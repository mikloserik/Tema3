using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Tema2.Models
{
    public class JsonExporter : Exporter
    {
        public void export(ProductDBContext db)
        {
            var json = new JavaScriptSerializer().Serialize(db);
            Console.WriteLine(json);
        }
    }
}