﻿namespace Tangle.Net.Mam.Mam
{
  using System.Collections.Generic;
  using System.Linq;

  using Tangle.Net.Cryptography;
  using Tangle.Net.Entity;
  using Tangle.Net.Mam.Merkle;
  using Tangle.Net.Utils;

  /// <summary>
  /// The curl mam parser.
  /// </summary>
  public class CurlMamParser : AbstractMam, IMamParser
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CurlMamParser"/> class.
    /// </summary>
    /// <param name="mask">
    /// The mask.
    /// </param>
    /// <param name="treeFactory">
    /// The tree Factory.
    /// </param>
    public CurlMamParser(IMask mask, IMerkleTreeFactory treeFactory)
    {
      this.TreeFactory = treeFactory;
      this.Mask = mask;
    }

    /// <summary>
    /// Gets the tree factory.
    /// </summary>
    private IMerkleTreeFactory TreeFactory { get; }

    /// <inheritdoc />
    public UnmaskedAuthenticatedMessage Unmask(Bundle payload, TryteString channelKey, int securityLevel)
    {
      var maskedMessage = payload.Transactions.Select(t => t.Fragment).ToList().Merge();
      var unmaskedMessage = this.Mask.Unmask(maskedMessage, channelKey);

      var signatureLength = securityLevel * Fragment.Length;
      var signature = unmaskedMessage.GetChunk(0, signatureLength);
      var unmaskedMessageWithoutSignature = unmaskedMessage.GetChunk(signatureLength, unmaskedMessage.TrytesLength - signatureLength);

      var index = Converter.TritsToInt(unmaskedMessageWithoutSignature.GetChunk(0, 27).ToTrits());
      var messageHashes = unmaskedMessageWithoutSignature.GetChunk(27, unmaskedMessageWithoutSignature.TrytesLength - 27).GetChunks(Hash.Length);
      var nextRoot = Hash.Empty;
      var treeHashes = new List<Hash>();
      var messageTrytes = new List<TryteString>();

      for (var i = 0; i < messageHashes.Count; i++)
      {
        if (messageHashes[i].Value != Hash.Empty.Value)
        {
          continue;
        }

        treeHashes = messageHashes.Take(i).Select(h => new Hash(h.Value)).ToList();
        nextRoot = new Hash(messageHashes[i + 1].Value);
        messageTrytes.AddRange(messageHashes.Skip(i + 2).Take(messageHashes.Count - i + 2));

        break;
      }

      var chainedMessageTrytes = messageTrytes.Merge();

      var recalculatedTree = this.TreeFactory.FromBranch(treeHashes.Select(t => new MerkleNode { Hash = t }).ToList());

      return new UnmaskedAuthenticatedMessage
               {
                 NextRoot = nextRoot,
                 Message = chainedMessageTrytes,
                 Root = recalculatedTree.Root.Hash
               };
    }
  }
}