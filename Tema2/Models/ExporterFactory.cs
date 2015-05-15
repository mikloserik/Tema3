using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tema2.Models
{
    public class ExporterFactory
    {
        public Exporter CreateExporter(String t)
        {
            Exporter e;
            if (t == "Csv")
            {
                e = new CSVExporter();
                return e;
            }
            if (t == "Json")
            {
                e = new JsonExporter();
                return e;
            }
            return null;
        }
    }
}