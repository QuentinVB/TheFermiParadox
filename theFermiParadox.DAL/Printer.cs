using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace theFermiParadox.DAL
{
    public class Printer<T>
    {
        private static CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true
        };
       

        public static void PrintTable(string filePath, List<T> records)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer,config))
            {
                csv.WriteRecords(records);     
            }
        }
        public static void PrintHeader(string filePath, T record)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteHeader(record.GetType());
            }
        }
        public static void PrintRecord(string filePath,T record)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer,config))
            {
                csv.NextRecord();
                csv.WriteRecord(record);
            }
        }
    }

}
