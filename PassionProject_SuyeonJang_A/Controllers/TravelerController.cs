using PassionProject_SuyeonJang_A.Models;
using PassionProject_SuyeonJang_A.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProject_SuyeonJang_A.Controllers
{
    public class TravelerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static TravelerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44362/api/");
        }
        // GET: Traveler/List
        public ActionResult List()
        {
            //objective: communicate with our Traveler data api to retrieve a list of Travelers
            //curl https://localhost:44362/api/travelerdata/listtravelers


            string url = "travelerdata/listtravelers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<TravelerDto> travelers = response.Content.ReadAsAsync<IEnumerable<TravelerDto>>().Result;
            //Debug.WriteLine("Number of travelers received : ");
            //Debug.WriteLine(travelers.Count());


            return View(travelers);
        }
        // GET: Traveler/Details/5
        public ActionResult Details(int id)
        {
            DetailsTraveler ViewModel = new DetailsTraveler();

            //objective: communicate with our Traveler data api to retrieve one Traveler
            //curl https://localhost:44324/api/travelerdata/findTraveler/{id}

            string url = "travelerdata/findTraveler/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            TravelerDto SelectedTraveler = response.Content.ReadAsAsync<TravelerDto>().Result;
            Debug.WriteLine("Traveler received : ");
            Debug.WriteLine(SelectedTraveler.TravelerFirstName);

            ViewModel.SelectedTraveler = SelectedTraveler;

            //show all destinations under the care of this Traveler
            url = "travelerdata/listdestinationsfortraveler/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DestinationDto> KeptDestinations = response.Content.ReadAsAsync<IEnumerable<DestinationDto>>().Result;

            ViewModel.KeptDestinations = KeptDestinations;


            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Traveler/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Traveler/Create
        [HttpPost]
        public ActionResult Create(Traveler Traveler)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Traveler.TravelerName);
            //objective: add a new Traveler into our system using the API
            //curl -H "Content-Type:application/json" -d @Traveler.json https://localhost:44324/api/travelerdata/addTraveler
            string url = "travelerdata/addtraveler";


            string jsonpayload = jss.Serialize(Traveler);
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
                return RedirectToAction("Error");
            }

        }
        // GET: Traveler/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "travelerdata/findtraveler/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TravelerDto selectedTraveler = response.Content.ReadAsAsync<TravelerDto>().Result;
            return View(selectedTraveler);
        }

        // POST: Traveler/Update/5
        [HttpPost]
        public ActionResult Update(int id, Traveler Traveler)
        {

            string url = "travelerdata/updatetraveler/" + id;
            string jsonpayload = jss.Serialize(Traveler);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        // GET: Traveler/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "travelerdata/findtraveler/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TravelerDto selectedTraveler = response.Content.ReadAsAsync<TravelerDto>().Result;
            return View(selectedTraveler);
        }

        // POST: Traveler/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "travelerdata/deletetraveler/" + id;
            HttpContent content = new StringContent("");
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
    }
}