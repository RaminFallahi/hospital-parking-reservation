using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using hospital_project4.Models;
using System.Web.Script.Serialization;

namespace hospital_project4.Controllers
{
    public class LotController : Controller
    {
        // GET: Lot/List
        public ActionResult List()
        {
            //objective: communicate with out Lot data api to retrieve a list of Lots.
            //curl https://localhost:44374/api/lotdata/listlots

            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44374/api/lotdata/listlots";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<Lot> Lots = response.Content.ReadAsAsync<IEnumerable<Lot>>().Result;

            return View(Lots);
        }

        // GET: Lot/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our Lot data api to retrieve one Lot.
            //curl https://localhost:44374/api/lotdata/findlot/{id}

            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44374/api/lotdata/findlot/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Lot selectedLot = response.Content.ReadAsAsync<Lot>().Result;
            
            return View(selectedLot);
        }

        // GET: Lot/New
        public ActionResult New()
        {
            //this is the GET request to just access the create page 
            return View();
        }

        // POST: Lot/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lot/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lot/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lot/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lot/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
