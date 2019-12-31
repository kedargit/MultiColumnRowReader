using CsvHelper;
using MultiColumnRowReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MultiColumnRowReader
{
    public class LineParser : ILineParser
    {
        public string[] Process(string filePath)
        {
            CsvParser parser;
            using (TextReader textReader = File.OpenText(filePath))
            {
                parser = new CsvParser(textReader);
                parser.Configuration.Delimiter = ",";
                return parser.Read();

            }
        }
    }

    public class SingleRowProcessor : ISingleRowProcessor
    {
        public List<PersonActivityReport> Process(ILineParser lineParser,
                                                   ILineAttributes lineAttributes,
                                                        string filePath,
                                                        string configPath)
        {
            int lineNumber = 1;
            string[] row = lineParser.Process(filePath);
            bool isExists = lineAttributes.Get("Weight(in Kgs)", lineNumber, configPath);


            return new List<PersonActivityReport>();
        }
    }

    public class LineAttributes : ILineAttributes
    {
        public bool Get(string key, int lineNumber, string configPath)
        {
            bool returnValue = false;
            XDocument doc = System.Xml.Linq.XDocument.Load(configPath);
            XElement data = doc.XPathSelectElements($".//{key}/Row[@LineNumber='{lineNumber}']").FirstOrDefault();

            if (data != null)
                returnValue = true;
            return returnValue;

        }
    }
}
