using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tema2.Models
{
    public interface Exporter
    {
        void export(ProductDBContext db);
    }
}