namespace Lucene.Net.Analysis.Jieba.Segmenter.Common;

// Refer to: https://github.com/brianfromoregon/trie
public class TrieNode
{
    public char Char { get; set; }
    public int Frequency { get; set; }
    public Dictionary<char, TrieNode> Children { get; set; }

    public TrieNode(char ch)
    {
        Char = ch;
        Frequency = 0;
        
        // TODO: or an empty dict?
        //Children = null;
    }

    public int Insert(string s, int pos, int freq = 1)
    {
        if (string.IsNullOrEmpty(s) || pos >= s.Length)
        {
            return 0;
        }

        if (Children == null)
        {
            Children = new Dictionary<char, TrieNode>();
        }

        var c = s[pos];
        if (!Children.ContainsKey(c))
        {
            Children[c] = new TrieNode(c);
        }

        var curNode = Children[c];
        if (pos == s.Length - 1)
        {
            curNode.Frequency += freq;
            return curNode.Frequency;
        }

        return curNode.Insert(s, pos + 1, freq);
    }

    public TrieNode Search(string s, int pos)
    {
        if (string.IsNullOrEmpty(s))
        {
            return null;
        }

        // if out of range or without any child nodes
        if (pos >= s.Length || Children == null)
        {
            return null;
        }
        // if reaches the last char of s, it's time to make the decision.
        if (pos == s.Length - 1)
        {
            return Children.ContainsKey(s[pos]) ? Children[s[pos]] : null;
        }
        // continue if necessary.
        return Children.ContainsKey(s[pos]) ? Children[s[pos]].Search(s, pos + 1) : null;
    }
}