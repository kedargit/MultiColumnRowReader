using System.Collections.Generic;
using MultiColumnRowReader.Models;

namespace MultiColumnRowReader
{
    public interface ISingleRowProcessor
    {
        List<PersonActivityReport> Process(ILineParser lineParser,
                                                  ILineAttributes lineAttributes,
                                                       string filePath,
                                                       string configPath);
    }
}