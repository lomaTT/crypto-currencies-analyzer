namespace CryptoAnalyzerCore;

public class Program
{
    public static void Main(string[] args)
    {
        XMLimport str = new XMLimport();
        str.exportFromCSV("D:/C#-projs/crypto-currencies-analyzer/archive");
    }
}