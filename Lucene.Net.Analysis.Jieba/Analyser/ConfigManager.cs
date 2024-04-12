namespace Lucene.Net.Analysis.Jieba.Analyser;

public class ConfigManager
{
    // TODO: duplicate codes.
    public static string ConfigFileBaseDir
    {
        get
        {
            return "Resources";
        }
    }

    public static string IdfFile
    {
        get { return System.IO.Path.Combine(ConfigFileBaseDir, "idf.txt"); }
    }

    public static string StopWordsFile
    {
        get { return System.IO.Path.Combine(ConfigFileBaseDir, "stopwords.txt"); }
    }
}