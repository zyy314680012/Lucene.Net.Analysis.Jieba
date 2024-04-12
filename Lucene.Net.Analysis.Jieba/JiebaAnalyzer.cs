using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Jieba.Segmenter;
using Lucene.Net.Analysis.TokenAttributes;

namespace Lucene.Net.Analysis.Jieba;

public class JiebaAnalyzer : Analyzer
{
    public TokenizerMode mode;

    public JiebaAnalyzer(TokenizerMode Mode)
        : base()
    {
        this.mode = Mode;
    }

    protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
    {
        var tokenizer = new JiebaTokenizer(reader, mode);

        var tokenstream = (TokenStream)new LowerCaseFilter(Lucene.Net.Util.LuceneVersion.LUCENE_48, tokenizer);

        tokenstream.AddAttribute<ICharTermAttribute>();
        tokenstream.AddAttribute<IOffsetAttribute>();

        return new TokenStreamComponents(tokenizer, tokenstream);
    }
}