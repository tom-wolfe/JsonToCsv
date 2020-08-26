using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace JsonToCsv
{
    public static class CsvExporter
    {
        public static void ExportCsv(string file, IEnumerable<Dictionary<string, object>> data, IEnumerable<string> columns)
        {
            using (var stream = new FileStream(file, FileMode.Create))
            using (var streamWriter = new StreamWriter(stream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                // Write the headers.
                foreach (var column in columns)
                {
                    csvWriter.WriteField(column);
                }
                csvWriter.NextRecord();

                // Write the data.
                foreach (var row in data)
                {
                    foreach (var column in columns)
                    {
                        csvWriter.WriteField(row[column]);
                    }
                    csvWriter.NextRecord();
                }
                csvWriter.Flush();
            }
        }
    }
}
