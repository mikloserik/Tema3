using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web;

namespace Tema2.Models
{
    public class CSVExporter : Exporter
    {
        public void export(ProductDBContext db)
        {
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"ID\",\"Title\",\"Stock\",\"Price\"");

            TextWriter tw = new StreamWriter("Products.csv");

            IEnumerable<Product> e = db.Products.ToList();

            foreach (var line in e)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                                           line.ID,
                                           line.Title,
                                           line.Stock,
                                           line.Price));
            }

            tw.Write(sw.ToString());
            tw.Close();
        }
    }
}