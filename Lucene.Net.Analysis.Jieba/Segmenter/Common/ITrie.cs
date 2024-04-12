namespace Lucene.Net.Analysis.Jieba.Segmenter.Common;

// Refer to: https://github.com/brianfromoregon/trie
public interface ITrie
{
    //string BestMatch(string word, long maxTime);
    bool Contains(string word);
    int Frequency(string word);
    int Insert(string word, int freq = 1);
    //bool Remove(string word);
    int Count { get; }
    int TotalFrequency { get; }
}