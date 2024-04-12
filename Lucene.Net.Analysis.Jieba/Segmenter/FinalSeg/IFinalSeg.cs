namespace Lucene.Net.Analysis.Jieba.Segmenter.FinalSeg;

public interface IFinalSeg
{
    IEnumerable<string> Cut(string sentence);
}