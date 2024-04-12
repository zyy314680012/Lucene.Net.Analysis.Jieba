namespace Lucene.Net.Analysis.Jieba.Segmenter;

public class ConfigManager
{
    public static string ConfigFileBaseDir
    {
        get
        {
            var configFileDir = "Resources";
            return configFileDir;
        }
    }

    public static string MainDictFile
    {
        get { return System.IO.Path.Combine(ConfigFileBaseDir, "dict.txt"); }
    }

    public static string ProbTransFile
    {
        get { return System.IO.Path.Combine(ConfigFileBaseDir, "prob_trans.json"); }
    }

    public static string ProbEmitFile
    {
        get { return System.IO.Path.Combine(ConfigFileBaseDir, "prob_emit.json"); }
    }

    public static string PosProbStartFile
    {
        get { return System.IO.Path.Combine(ConfigFileBaseDir, "pos_prob_start.json"); }
    }

    public static string PosProbTransFile
    {
        get { return System.IO.Path.Combine(ConfigFileBaseDir, "pos_prob_trans.json"); }
    }

    public static string PosProbEmitFile
    {
        get { return System.IO.Path.Combine(ConfigFileBaseDir, "pos_prob_emit.json"); }
    }

    public static string CharStateTabFile
    {
        get { return System.IO.Path.Combine(ConfigFileBaseDir, "char_state_tab.json"); }
    }
}