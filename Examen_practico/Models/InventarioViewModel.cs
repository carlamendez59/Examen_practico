using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen_practico.Models
{
    public class InventarioViewModel
    {
        public IPagedList <Inventario> Inventario { get; set; }
    }
}