﻿using System.Text;
using Lucene.Net.Analysis.Jieba.Segmenter.Common;

namespace Lucene.Net.Analysis.Jieba.Analyser;

public class IdfLoader
{
    internal string IdfFilePath { get; set; }
    internal IDictionary<string, double> IdfFreq { get; set; }
    internal double MedianIdf { get; set; }

    public IdfLoader(string idfPath = null)
    {
        IdfFilePath = string.Empty;
        IdfFreq = new Dictionary<string, double>();
        MedianIdf = 0.0;
        if (!string.IsNullOrWhiteSpace(idfPath))
        {
            SetNewPath(idfPath);
        }
    }

    public void SetNewPath(string newIdfPath)
    {
        var idfPath = newIdfPath;
        if (IdfFilePath != idfPath)
        {
            IdfFilePath = idfPath;
            var lines = FileExtension.ReadAllLines(idfPath, Encoding.UTF8);
            IdfFreq = new Dictionary<string, double>();
            foreach (var line in lines)
            {
                var parts = line.Trim().Split(' ');
                var word = parts[0];
                var freq = double.Parse(parts[1]);
                IdfFreq[word] = freq;
            }

            MedianIdf = IdfFreq.Values.OrderBy(v => v).ToList()[IdfFreq.Count / 2];
        }
    }
}