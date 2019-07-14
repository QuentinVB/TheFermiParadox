using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace theFermiParadox.DAL
{
    public class Loader<T>
    {
        public static List<T> LoadTable(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader))
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
