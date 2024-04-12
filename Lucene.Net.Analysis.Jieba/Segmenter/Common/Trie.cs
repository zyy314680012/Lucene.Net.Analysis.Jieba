namespace Lucene.Net.Analysis.Jieba.Segmenter.Common;

// Refer to: https://github.com/brianfromoregon/trie
public class Trie : ITrie
{
    private static readonly char RootChar = '\0';

    internal TrieNode Root;

    public int Count { get; private set; }
    public int TotalFrequency { get; private set; }

    public Trie()
    {
        Root = new TrieNode(RootChar);
        Count = 0;
    }

    public bool Contains(string word)
    {
        CheckWord(word);

        var node = Root.Search(word.Trim(), 0);
        return node.IsNotNull() && node.Frequency > 0;
    }

    public bool ContainsPrefix(string word)
    {
        CheckWord(word);

        var node = Root.Search(word.Trim(), 0);
        return node.IsNotNull();
    }

    public int Frequency(string word)
    {
        CheckWord(word);

        var node = Root.Search(word.Trim(), 0);
        return node.IsNull() ? 0 : node.Frequency;
    }

    public int Insert(string word, int freq = 1)
    {
        CheckWord(word);

        var i = Root.Insert(word.Trim(), 0, freq);
        if (i > 0)
        {
            TotalFrequency += freq;
            Count++;
        }

        return i;
    }

    public IEnumerable<char> ChildChars(string prefix)
    {
        var node = Root.Search(prefix.Trim(), 0);
        return node.IsNull() || node.Children.IsNull() ? null : node.Children.Select(p => p.Key);
    }

    private void CheckWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            throw new ArgumentException("word must not be null or whitespace");
        }
    }
}