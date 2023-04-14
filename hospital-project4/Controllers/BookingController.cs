using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using hospital_project4.Models;
using System.Web.Script.Serialization;
using System.Net;

namespace hospital_project4.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking/List
        public ActionResult List()
        {
            //objective: communicate with out Booking data api to retrieve a list of bookings.
            //curl https://localhost:44374/api/bookingdata/listbookings

            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44374/api/bookingdata/listbookings";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The request is code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<BookingDto> Bookings = response.Content.ReadAsAsync<IEnumerable<BookingDto>>().Result;
            Debug.WriteLine("Number of bookings recieved : ");

            return View(Bookings);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our Booking data api to retrieve one booking.
            //curl https://localhost:44374/api/bookingdata/findbooking/{id}

            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44374/api/bookingdata/findbooking/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The request is code is ");
            //Debug.WriteLine(response.StatusCode);

            BookingDto selectedBooking = response.Content.ReadAsAsync<BookingDto>().Result;
            //Debug.WriteLine("booking recieved : ");
            //Debug.WriteLine(selectedBooking.BookingName);

            return View(selectedBooking);
        }

        // GET: Booking/New
        public ActionResult New()
            //this is the GET request to jusr access the create page 
        {
            // Load available fees from the database
            HttpClient client = new HttpClient();
            string url = "https://localhost:44374/api/feedata/listfees";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<FeeDto> fees = response.Content.ReadAsAsync<IEnumerable<FeeDto>>().Result;

            // Pass the fees to the view
            ViewBag.Fees = fees;

            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(BookingDto Booking)
        {
            Debug.WriteLine("The json payload is : ");

            //Create an instance of the Booking class

            //objective: add a new booking into our system using the API
            //curl -H "Content-Type:application/json" -d @booking.json https://localhost:44374/api/bookingdata/addbooking
            HttpClient client = new HttpClient();
            string url = "https://localhost:44374/api/bookingdata/addbooking";
            
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(Booking);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                Debug.WriteLine("Response status code: " + response.StatusCode);
                Debug.WriteLine("Response content: " + response.Content.ReadAsStringAsync().Result);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "https://localhost:44374/api/bookingdata/findbooking/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            BookingDto selectedBooking = response.Content.ReadAsAsync<BookingDto>().Result;

            if (selectedBooking == null)
            {
                return HttpNotFound();
            }

            // Load available fees from the database
            string feesUrl = "https://localhost:44374/api/feedata/listfees";
            HttpResponseMessage feesResponse = client.GetAsync(feesUrl).Result;
            IEnumerable<FeeDto> fees = feesResponse.Content.ReadAsAsync<IEnumerable<FeeDto>>().Result;

            // Pass the fees to the view
            ViewBag.Fees = fees;

            return View(selectedBooking);
        }

        // POST: Booking/Update/5
        [HttpPost]
        public ActionResult Update(int id, BookingDto bookingDto)
        {
            string url = "https://localhost:44374/api/bookingdata/updatebooking/" + id;
            Debug.WriteLine("The updated booking data is : ");

            HttpClient client = new HttpClient();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string jsonpayload = jss.Serialize(bookingDto);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            string url = "https://localhost:44374/api/bookingdata/findbooking/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                BookingDto selectedBooking = response.Content.ReadAsAsync<BookingDto>().Result;
                return View(selectedBooking);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "https://localhost:44374/api/bookingdata/deletebooking/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
