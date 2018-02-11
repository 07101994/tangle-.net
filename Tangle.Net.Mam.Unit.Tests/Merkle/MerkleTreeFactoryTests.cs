﻿namespace Tangle.Net.Mam.Unit.Tests.Merkle
{
  using System.Runtime.InteropServices;

  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using Tangle.Net.Cryptography;
  using Tangle.Net.Entity;
  using Tangle.Net.Mam.Merkle;
  using Tangle.Net.Unit.Tests.Cryptography;

  /// <summary>
  /// The merkle tree factory tests.
  /// </summary>
  [TestClass]
  public class MerkleTreeFactoryTests
  {
    private const string ExpectedKey =
      "TLYWKHBGCEJFIQUMPBOFZJVQI9SCMYIY9JO9UDOHDTP9GIHMOJANDONKXCNGUDTEUSDMPOULHFFK9VYOYBNGORLQLAMTFFQCLTYSXNWAO9FXHANCDWDTUESDSAV9YGXOXBTHOXZMVQKACIEGDAZRYMUXWQVWLMWUZYW9UMFJTTNBEGOHOUXHMANQ9UJHJKOBXPFILRFGJ9YUNORP9DKCCKWNQKPGRJVGYX9PHOQJDXXCAGHUBBDYYUZOUYJJQT9PVEEW9UOLZBOLPOUJVRFBDPAL9QHOQSPNRIXAJSDMWSJRXRANBIZOP9SRWIGIEZXRAPQACWEFIHHHTPB9HQKZUZXQJRJDTJMKCXUG9QGNXD9VOZOUXZGTBHXOTXRNVLYOBBR9XFFFHCWMBQLKT9LXAA9VTNAFMDZQCQUUDKMJGMJYFWKOUSWRUUSFNRAKEUCNTNLBQHTSHXMJCHPMNREIAMUCGMUYWTFBWDLPCXBWHUNCBDRKHBRXDSWDUITELWYISXNSVBBTJEWAHPMFPCABLNGCEVIWSE9QENQXVAPIROXDLUYRXUVKGTAIRZKGBGGVBFZTYMMRSX9YOTAXE9QOGWCBVZISWDPNLOPXYOWKRHPWVMUJDPDFAZEPQBGXHNJFKXQUMGVDP9PBIOIMQGEGDQWUZTAOYPLIROXTMWAIJQZHXEGATLSSPFGMVXFB9BWFUIFMAEOO9WKPBO9MMPFZSUKQWQQFGMJFPEGNHJLLEYVSRNHUM9RMDGCRHPDZWB99AFTOHJKONZFTUXYIZZETTIMOPHRUW9XY9XHISQAWVDFUMFKSJIM9DGPESMPCATVEXDDCAZCYZAZMGFAMQHSHJUWBBXNL9FQKJXV9UOHRVZILUFOXTHMQOSHFSEBDRDKSX9TRXUEJRZSOCWDDVHXNPNQFMHFSCLFND9WXQVYFMKAZTQDYUYAYAHGDKLWSJGWPBKOI9NBAQRVWYXLMEWSSVHBLXGLYICXSWBPBFCGKJWHCZSHMFSWXG9JHLTPZCCWSRWAUDLHCGFYIMYUUDSQRRVQZKBZEWZITFQLSXLXLCYLMHMSPANNXBBHNPUOWUEDDSL9APFBIVMYRETJNLOASGADUVDPCT9VXXQSIFAU9JUEGMWJAFJJTIAYCWUDGLHYHDZJWDEDSIPTIHPACZZIWZCPSJZHKOLDEZHIXQF9LBDOKUBWZU9DRGJLOYZXGNZYFXSCN9IPOLNNQCQLPSOTBUGQZFXEIY9VBJRMGZZIATYOPYNUGR9JQNSLMAZINZIBVUOKZIMK9FLZASFWCYKLJLJ99XJIDAZLYDMTONLNCKPKU9HWBVZGQGHENJWGQMUXQASXFCBJWPWQXPTBHSISKBVOSGCDOBQWRWUAI9CQPIJNEKNARVJSWMBNWTXJE9BT9XWDWHZZGRSZEQRNAVKBWQNTNGDMZVZOFXJYDIQOVUDS9XAZSIDEJCLMBMYUECVFUENDGINQTZMWPZREOCAROUXJBCINIHMVISHKIWGJVW9BGRFHNUYRPRNWNDCPTPLTJKLWHZCCW9NKSGOO9PUCQILLSGBAZVKXBIVCPVIQK9JNKLIOVVKQTNTMYSCWUQGADEMKWZDGLWDMIIIICLSEXPNVTILPBOGHPXKCNHWDAUHLRSOCDTST9XNV9RDQJTXVKIGHBQJKCI9HJNVQQTVMTZGJP9JBZOHVCIAQJBCAFBQODZVU9JBOCIDJYGVQTXPQKSOPLZUKUSNHHGNSXYXEJHPGJVFNMWQZYNSNBMJMCOSOUWIABRSXFRXU9DUXDVNOP9VVQKLEEACTEZZ9VJCLSLHGDWNIVRVKUOKWTNPFYFZQGQZKJOQYCFGHYQFGMMKJAANKNYM9ZNOVNUFU9MQGSLLLLDRJRQIMBFDFCTGOUZVKLBPDIWRXUU9LUCNUCLUVEEVKVUONAGFEJKGZGAKTTXWDYOCCDUZLUCSLNHGMBBXPGBNHULJZULWGJWRZFIFVLRAUJPNTGX9JS9TRUUASZUNYIFOSCVRYFPITD9IQVXJM9FGXLDSOQMPZP9MEVJXAOWRLWQWLGXDJIIHT9YEIJHQ9AMHFLCVVDSVLZTUOARRXERGLWPNTWDP9HIBDOCGZBVSVNWQP9VFJJZRQHHMPXKSMI9SZRHXQGSCSAXDQH9OOPJHREZQXGIVBZTJGAMOJZNFEXLIOGVUXEACETRNMCOXEPMRIYYVFV9WDYEOPAGRRRXIQLIFLLAIJUWFFAMCHYDGGZDWGWQKSIOGRTSPMAVREZNG9X9PQSSDCCFIJQIKIO9TFYMNWHQETEKUNOYEEERSJKVIPOLE9ELJMF9KYSEFEYOXHBDOOTTSBQUYGWAJDC9DMAMEWMVWGTVYTNUOBTLHIA9KTDHXLDBHLDLHI9HPDIVAZBVIXQEHIMJJIPDPCXKLCURLDLRQWZDBGJW9CAQJONJIPAZUIMSHV9HFYV9KWSKCQIQPOGLQSLDCZUUGHV9Q9PWPSUBSUZOED9LQUPECOMDPRYO9TDM9CQWRODIQFIFMWUKYVFNOYWOOOOSTIIGKHEHGOROOYLDGKAEUZVYYEUWO9ZJGDSU9JOGVGT99FGUOOKACWACLLMVXPNCBNNKSADFZVSYVNGFAAVKMXPV9NGZTESWQQGWHTGUNXHFETCAAFCNGUYBJHODLLILYTRQEAQWWOHFAUSYXPOOFKWAXXTHROYBUJECOSXDQDUQXDFEMWBUGFHURTXT9ZZRLNNEHBQXSAGYJVWHPNORYBIA9CJNWWBDVRVVWNQZQXOWZHOGOPKKNDGJVEFJXKJGTGJCBDXORUYHWKXLQCYFIEMIDJ9MKTQPJMEJVTJPPWRPOKHOHFAYTCPIMJCGPHHAISK9GDPQKLOOCLJCCTVPYNZJLFJPNTGLAORQIABUTTZKHZKWPBEWGQFOVFXFEJHFDQFCPTHZBAYJDZDOFXJANMVJZKAMCFJIWVFIB9JOBJNNBIVMNYDRGUYGQQQVEFGTNTXFMXRWUYHBE9ZVNLDUV9YMNVIINKIGZCPBSFBGFB9VLITQDUDTQYSPOH9DNAN9JNJBNVWXVNVJYVE9JWOMSNTGDBXNBMYOHLXMPVNIGUCIYBRPXZWZTVFNMENZKRWPBVISJRPWZFLDMBKHQDVTJEAJW9FMS9GHBXYZYRPYEBLRM9IULG9DASXHUAGAJTXLALHCMZCEAKXVKEETTCCKP9BJDH9EGFVZJUFEYFNJXLNBC99BZZIRYMMROKG9QDAW9EFSIPTKZVBNXTIYEFS9FCKNADMBWODMDZFUUJHFNYYKV99YILANPNPZBLQYWZBY9GFQGZTJLQINCSMDURBEUVRSAHA9YFNYEXGYLJGFIKZNJUMSCGTAEUVLJJTQNLGIFBXHLFBEOZSIGEWJSDPND9WTQTSEOPEJZEECVXVFHGXQPUDDYNKCKWRNC9O9KLMSIBDPICXSWDSBT9NVQJXDNFPDYQDEAOCNZYLSBJGZLXG9JAQZJXIXYVHEC9UZDHICCJQMLUGXBADNQYMBHQSFWEQA9VLUMOWVVDCOQMFYDSQRSKDNVWMBJOIORJEQOX9WGEYOCDEWGHZIHFIEUYXGSPSN9JW9AFKWJRVFKYKDKZUVXJWJGSJWE9RBTSBMR9AFFVNFDCSK9BTIDQSTGMWYEBUOTWPDW99YWCKAZS9CCZKYDWHGKMGAWDHFHT9XSVOOJDLCRCRUZUDOYLXSJSUPSFGW9XKPAYAZHM9SDKCJAEMUF9JTSGJFAVGEZWOQULEQZSKQ9FUPBFOLBVKAGAOMQG9PCLYCXNAYLPS9CBJZFGQWXCRYRYCTE9OJDJSHTSMVZXDGIMTRXNXYVTBIXQGITNDMFRDOPWXBORCMUSSWNWUSOESJCHIJEFZKGMDVO9BAZBDQWCXJUVDPUXOIGXWSSAWHWSNTLDBIQJXMATIPFROWXHZNQAFGWFYQWFQIHAQCFJBNRMJRFGTFJLLKLWQBPNMXYEPQ9RQKNYOJOG9V9DSGJMS9JVFSKSIEKBIZFZ9NTACSOSCNZGEZDBBNYZNZRDXFQSZWAPYHUWKYXFTPXRGXJ9DLMLDIXXVHYVBCJNWASMC9SWB9CSLWTILRRX9QS9LFCCMCQ9YJRSBRLZVUQXCHGFHRTARZRBERHDCEW9Z9YYGEDLBZDHSZLNNGIMC9OZXFXXPKGEGAWEWBZYFNKIZ9GNOGXRLHUFPCBWKCKOZAUWXNBNTWQVDWONPDDRAKCOVDKKDNOVEEOJSRPAMUFEDOZTMHZG9NDZ9EVFUKTUWBIYOUQOJZJXDRJLMPDPMFTXOPEAACURZUV9ZJMKPBUOSZZIFZOEEEEP9AZHMRSEIDT9OPMIGFFEYRECMLKR9XUNDFWXPQOIHQFACTSXKWMDOXB";
    #region Public Methods and Operators

    /// <summary>
    /// The test tree has only more than one leaf should create nodes.
    /// </summary>
    [TestMethod]
    public void TestTreeHasOnlyMoreThanOneLeafShouldCreateNodes()
    {
      var seed = new Seed("L9DRGFPYDMGVLH9ZCEWHXNEPC9TQQSA9W9FZVYXLBMJTHJC9HZDONEJMMVJVEMHWCIBLAUYBAUFQOMYSN");
      var factory = new CurlMerkleTreeFactory(new CurlMerkleNodeFactory(new Curl()), new CurlMerkleLeafFactory(new AddressGenerator(seed)));
      var tree = factory.Create(seed, 0, 10, SecurityLevel.Medium);

      Assert.IsNotNull(tree.Root);
      Assert.AreEqual("PPTEDVFWYZFLADQINJDWOKSKWPJHKOGLEKLGNKSKHHTMHLTIOUHDGDRPCJCWQDJMNYDAQUIQRXIGLKVSC", tree.Root.Hash.Value);
      Assert.AreEqual("HUCUF9KHGAFXZPVWRMEHJEPLEKCVDQNPYRALE9IEFELWDUJUDOCQSZWPBLXANM9JWFWYMOZWFXRWQWEPI", tree.Root.LeftNode.Hash.Value);
      Assert.AreEqual("NVPRSBAZVMYYOLVKWPSZIAECBRINZ9CXWBGYVPVCVYLXOBUIJWXPUSOCTHQOVYFHKUFHCCKHWAEQPIRKD", tree.Root.LeftNode.LeftNode.Hash.Value);
      Assert.AreEqual(ExpectedKey, tree.Root.LeftNode.LeftNode.LeftNode.LeftNode.Key.Value);
      Assert.AreEqual("VTGYAQMLCYKTXBYNDKHCHJFWDFCPHO99GRMJVEVPOEWQPHJWDUJ9YBOBDNAQBSFKFHWFDDMCECHITZAKK", tree.Root.RightNode.Hash.Value);
      Assert.AreEqual(10, tree.Size);
    }

    /// <summary>
    /// The test tree has only one leaf should set leaf as root node.
    /// </summary>
    [TestMethod]
    public void TestTreeHasOnlyOneLeafShouldSetLeafAsRootNode()
    {
      var factory = new CurlMerkleTreeFactory(new CurlMerkleNodeFactory(new Curl()), new CurlMerkleLeafFactory(new AddressGeneratorStub()));
      var tree = factory.Create(Seed.Random(), 0, 1, SecurityLevel.Medium);

      Assert.IsNotNull(tree.Root);
    }

    #endregion
  }
}