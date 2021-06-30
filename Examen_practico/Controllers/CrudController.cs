using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Examen_practico.Models;
using PagedList;

namespace Examen_practico.Controllers
{
    public class CrudController : Controller
    {
        // GET: Crud
        private int CantidadRegistros = 10;
        private const int NumeroItemsPorPagina = 10;
        public ActionResult Index(int? pagesize,int? page)
        {
           
             IList<Inventario> invObj = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:59588/");
            var request = hc.GetAsync("api/Inventarios");
            request.Wait();
            var response = request.Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<IList<Inventario>>();
                data.Wait();

                invObj = data.Result;

                pagesize = (pagesize ?? 10);
                page = (page ?? 1);

                ViewBag.pages = pagesize;
            }

            
            return View(invObj.ToPagedList(page.Value,pagesize.Value));
        }
        public ActionResult Insertar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insertar(Inventario inventario)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:59588/");

            var insertarinv = hc.PostAsJsonAsync<Inventario>("api/Inventarios",inventario);
            insertarinv.Wait();

            var sdata = insertarinv.Result;
            if (sdata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Insertar");
        }
        public ActionResult Detalles(int id)
        {
            Inventario iobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:59588/");
            var apiid = hc.GetAsync("api/Inventarios?id=" + id.ToString());
            apiid.Wait();
            var rdata = apiid.Result;
            if (rdata.IsSuccessStatusCode)
            {
                var ddata = rdata.Content.ReadAsAsync<Inventario>();
                ddata.Wait();
                iobj = ddata.Result;
            }
            return View(iobj);
        }
        public ActionResult Editar(int id)
        {
            Inventario iobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:59588/");
            var apiid = hc.GetAsync("api/Inventarios?id=" + id.ToString());
            apiid.Wait();
            var rdata = apiid.Result;
            if (rdata.IsSuccessStatusCode)
            {
                var ddata = rdata.Content.ReadAsAsync<Inventario>();
                ddata.Wait();
                iobj = ddata.Result;
            }
            return View(iobj);
        }
        [HttpPost]
        public ActionResult Editar(Inventario inven, int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:59588/");

            var insertarinv = hc.PutAsJsonAsync("api/Inventarios?id="+id.ToString(), inven);
            insertarinv.Wait();

            var sdata = insertarinv.Result;
            if (sdata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(inven);
        }
        public ActionResult Eliminar(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:59588/");
            var elinv = hc.DeleteAsync("api/Inventarios?id=" + id.ToString());
            elinv.Wait();

            var edata = elinv.Result;
            if (edata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}