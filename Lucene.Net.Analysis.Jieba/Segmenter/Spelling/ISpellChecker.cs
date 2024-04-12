namespace Lucene.Net.Analysis.Jieba.Segmenter.Spelling;

public interface ISpellChecker
{
    IEnumerable<string> Suggests(string word);
}