using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using hospital_project4.Models;

namespace hospital_project4.Controllers
{
    public class FeeController : Controller
    {
        // GET: Fee/List
        public ActionResult List()
        {
            //objective: communicate with out Fee data api to retrieve a list of fees.
            //curl https://localhost:44374/api/feedata/listfees

            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44374/api/feedata/listfees";
            HttpResponseMessage response = client.GetAsync(url).Result;
            
            Debug.WriteLine("The request is code is ");
            Debug.WriteLine(response.StatusCode);
            
            IEnumerable<FeeDto> Fees = response.Content.ReadAsAsync<IEnumerable<FeeDto>>().Result;
            Debug.WriteLine("Number of fees recieved : ");

            return View(Fees);
        }

        // GET: Fee/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our Fee data api to retrieve a fee.
            //curl https://localhost:44374/api/feedata/findfee/{id}

            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44374/api/feedata/findfee/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The request is code is ");
            Debug.WriteLine(response.StatusCode);

            FeeDto selectedFee = response.Content.ReadAsAsync<FeeDto>().Result;
            Debug.WriteLine("fee recieved : ");

            return View(selectedFee);
        }

        // GET: Fee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fee/Create
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

        // GET: Fee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Fee/Edit/5
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

        // GET: Fee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Fee/Delete/5
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
