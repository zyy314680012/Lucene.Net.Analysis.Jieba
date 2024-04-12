using System.Text;
using Lucene.Net.Analysis.Jieba.Segmenter;
using Lucene.Net.Analysis.TokenAttributes;

namespace Lucene.Net.Analysis.Jieba;

public class JiebaTokenizer : Tokenizer
{
    private static bool _initial = false;
    private string _inputText;
    private bool _originalResult = false;
    private int _start = 0;

    private readonly string _stropWordsPath = "Resources/stopwords.txt";

    private readonly JiebaSegmenter _segmenter;
    private TokenizerMode _mode;
    private ICharTermAttribute _termAtt;
    private IOffsetAttribute _offsetAtt;
    private IPositionIncrementAttribute _posIncrAtt;
    private ITypeAttribute _typeAtt;

    private Dictionary<string, int> _stopWords = new Dictionary<string, int>();
    private List<Segmenter.Token> _wordList = new List<Segmenter.Token>();

    private IEnumerator<Segmenter.Token> _iter;

    public JiebaTokenizer(TextReader input, TokenizerMode Mode)
        : base(AttributeFactory.DEFAULT_ATTRIBUTE_FACTORY, input)
    {
        _segmenter = new JiebaSegmenter();
        _mode = Mode;
        LoadStopWords();
        Init();
    }

    public Dictionary<string, int> StopWords
    {
        get => _stopWords;
    }

    private void LoadStopWords()
    {
        using (var reader = new StreamReader(_stropWordsPath, Encoding.UTF8))
        {
            var s = "";
            while ((s = reader.ReadLine()) != null)
            {
                if (String.IsNullOrEmpty(s))
                    continue;
                if (_stopWords.ContainsKey(s))
                    continue;
                _stopWords.Add(s, 1);
            }
        }
    }

    private void Init()
    {
        _termAtt = AddAttribute<ICharTermAttribute>();
        _offsetAtt = AddAttribute<IOffsetAttribute>();
        _posIncrAtt = AddAttribute<IPositionIncrementAttribute>();
        _typeAtt = AddAttribute<ITypeAttribute>();
    }

    private string ReadToEnd(TextReader input)
    {
        return input.ReadToEnd();
    }

    public sealed override Boolean IncrementToken()
    {
        ClearAttributes();

        var word = Next();
        if (word != null)
        {
            var buffer = word.ToString();
            _termAtt.SetEmpty().Append(buffer);
            _offsetAtt.SetOffset(CorrectOffset(word.StartOffset), CorrectOffset(word.EndOffset));
            _typeAtt.Type = word.Type;
            return true;
        }

        End();
        Dispose();
        return false;
    }

    private Lucene.Net.Analysis.Token Next()
    {
        var length = 0;
        var res = _iter.MoveNext();
        if (res)
        {
            var word = _iter.Current;
            var token = new Lucene.Net.Analysis.Token(word.Word, word.StartIndex, word.EndIndex);
            _start += length;
            return token;
        }

        return null;
    }

    public override void Reset()
    {
        base.Reset();

        _inputText = ReadToEnd(base.m_input);
        RemoveStopWords(_segmenter.Tokenize(_inputText, _mode));

        _start = 0;
        _iter = _wordList.GetEnumerator();
    }

    private void RemoveStopWords(IEnumerable<Segmenter.Token> words)
    {
        _wordList.Clear();

        foreach (var x in words)
        {
            if (!_stopWords.ContainsKey(x.Word))
            {
                _wordList.Add(x);
            }
        }
    }
}