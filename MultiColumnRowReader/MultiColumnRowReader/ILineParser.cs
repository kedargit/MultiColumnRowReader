namespace MultiColumnRowReader
{
    public interface ILineParser
    {
        string[] Process(string filePath);
    }
}