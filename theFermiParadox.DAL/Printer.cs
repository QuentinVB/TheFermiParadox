using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace theFermiParadox.DAL
{
    public class Printer<T>
    {
        public static void PrintTable(string filePath, List<T> records)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AutoMap<T>();

                csv.WriteRecords(records);     
                
            }
        }
        public static void PrintHeader(string filePath, T record)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteHeader(record.GetType());
            }
        }
        public static void PrintRecord(string filePath,T record)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.NextRecord();
                csv.WriteRecord(record);
            }
        }
    }

}
