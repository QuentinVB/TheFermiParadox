using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace theFermiParadox.DAL
{
    public class Loader<T>
    {
        //TODO : should be elsewhere ?
        private const string DATAROOTPATH = "data";
        private static string solutionPath = Environment.CurrentDirectory;
        private static string pathOffset = "../../..";
        private static CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true
        };

        public static List<T> LoadTable(string fileName)
        {
            using (var reader = new StreamReader(Path.Combine(solutionPath, pathOffset, DATAROOTPATH, fileName)))
            using (var csv = new CsvReader(reader, config))
            {
                return csv.GetRecords<T>().ToList();
                /*
                foreach (T foo in records)
                {
                    table.AddEntry(foo.Name, Convert.ToDouble(foo.Frequency.Replace("%", string.Empty), CultureInfo.InvariantCulture));
                }*/
            }
        }
    }

}
