namespace Lucene.Net.Analysis.Jieba.Segmenter;

public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
    public new TValue this[TKey key]
    {
        get
        {
            if (!ContainsKey(key))
            {
                Add(key, default(TValue));
            }
            return base[key];
        }
        set { base[key] = value; }
    }
}