namespace MultiColumnRowReader
{
    public interface ILineAttributes
    {
        bool Get(string key, int lineNumber, string configPath);
    }
}