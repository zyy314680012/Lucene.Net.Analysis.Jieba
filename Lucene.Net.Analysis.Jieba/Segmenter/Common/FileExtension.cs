using System.Text;

namespace Lucene.Net.Analysis.Jieba.Segmenter.Common;

public static class FileExtension
{
    public static string ReadAllLine(string path)
    {
        return ReadAllLine(path, Encoding.UTF8);
    }

    public static string ReadAllLine(string path, Encoding encoding)
    {
        using (var sr = new StreamReader(path, encoding))
        {
            return sr.ReadToEnd();
        }
    }

    public static List<string> ReadAllLines(string path, Encoding encoding)
    {
        List<string> list = new List<string>();
        using (StreamReader streamReader = new StreamReader(path, encoding))
        {
            string item;
            while ((item = streamReader.ReadLine()) != null)
            {
                list.Add(item);
            }
        }

        return list;
    }

    public static List<string> ReadAllLines(string path)
    {
        return ReadAllLines(path, Encoding.UTF8);
    }
}