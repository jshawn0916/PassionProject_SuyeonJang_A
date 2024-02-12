using PassionProject_SuyeonJang_A.Models.ViewModels;
using PassionProject_SuyeonJang_A.Models;
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
    public class DestinationController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DestinationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44362/api/");
        }

        // GET: Destination/List
        // Objective :  a webpage that lists destinations in our system
        public ActionResult List()
        {
            //use HTTP client to access information

            HttpClient client = new HttpClient();
            //set the url
            string url = "destinationdata/listdestinations";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DestinationDto> Destinations = response.Content.ReadAsAsync<IEnumerable<DestinationDto>>().Result;

            return View(Destinations);
        }

        // GET: Destination/Details/5
        public ActionResult Details(int id)
        {
            DetailsDestination ViewModel = new DetailsDestination();

            //objective: communicate with destination data api to retrieve one destination
            //curl https://localhost:44362/api/destinationdata/finddestination/{id}

            string url = "destinationdata/finddestination/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DestinationDto SelectedDestination = response.Content.ReadAsAsync<DestinationDto>().Result;
            Debug.WriteLine("destination received : ");
            Debug.WriteLine(SelectedDestination.DestinationName);

            ViewModel.SelectedDestination = SelectedDestination;




            return View(ViewModel);
        }



        // POST: Destination/Create
        [HttpPost]
        public ActionResult Create(Destination destinations)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(destination.DestinationName);
            //objective: add a new destination into our system using the API
            //curl -H "Content-Type:application/json" -d @destination.json https://localhost:44362/api/destinationdata/adddestination
            string url = "destinationdata/adddestination";


            string jsonpayload = jss.Serialize(destinations);
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

        // GET: Destination/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDestination ViewModel = new UpdateDestination();

            //the existing destination information
            string url = "destinationdata/finddestination/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DestinationDto SelectedDestination = response.Content.ReadAsAsync<DestinationDto>().Result;
            ViewModel.SelectedDestination = SelectedDestination;

            // all journeys to choose from when updating this destination
            //the existing destination information
            url = "journeysdata/listjourneys/";
            response = client.GetAsync(url).Result;
            IEnumerable<JourneysDto> JourneysOptions = response.Content.ReadAsAsync<IEnumerable<JourneysDto>>().Result;

            ViewModel.JourneysOptions = JourneysOptions;

            return View(ViewModel);

        }

        // POST: Destination/Update/5
        [HttpPost]
        public ActionResult Update(int id, Destination destinations)
        {

            string url = "destinationdata/updatedestination/" + id;
            string jsonpayload = jss.Serialize(destinations);
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

        // GET: Destination/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "destinationdata/finddestination/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DestinationDto selecteddestination = response.Content.ReadAsAsync<DestinationDto>().Result;
            return View(selecteddestination);
        }

        // POST: Destination/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "destinationdata/deletedestination/" + id;
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