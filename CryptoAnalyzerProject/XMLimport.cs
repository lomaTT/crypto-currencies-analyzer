using System.Xml.Linq;

namespace CryptoAnalyzerProject;

public class XMLimport
{
    public void exportFromCSV(string path)
    {
        
        DirectoryInfo di = new DirectoryInfo(path);
        FileInfo[] files = di.GetFiles("*.csv");
        for (int i = 0; i < files.Length; i++)
        {
            string[] source = File.ReadAllLines("" + files[i]);
            XElement crypto = new XElement("Root",
                from str in source
                let fields = str.Split(',')
                select new XElement("Cryptocurrency",
                    new XAttribute("CurrencyName", fields[0]),
                    new XElement("Date", fields[1]),
                    new XElement("Open", fields[2]),
                    new XElement("High", fields[3]),
                    new XElement("Low", fields[4]),
                    new XElement("Close", fields[5])
                )
            );
            // Console.WriteLine(crypto);
            crypto.Save(files[i] + ".xml"); // save into bin/Debug/net7.0/SOL.xml
        }
        
        
    }
}