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
    public class JourneyController : Controller
    {
        // GET: Journey
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static JourneyController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44362/api/");
        }

        // GET: Journeys/List
        public ActionResult List()
        {
            //objective: communicate with our Journeys data api to retrieve a list of Journeys
            //curl https://localhost:44362/api/Journeysdata/listJourneys


            string url = "journeysdata/listjourneys";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<JourneysDto> Journeys = response.Content.ReadAsAsync<IEnumerable<JourneysDto>>().Result;
            //Debug.WriteLine("Number of Journeys received : ");
            //Debug.WriteLine(Journeys.Count());


            return View(Journeys);
        }

        // GET: Journeys/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our Journeys data api to retrieve one Journeys
            //curl https://localhost:44362/api/journeysdata/findjourneys/{id}

            DetailsJourney ViewModel = new DetailsJourney();

            string url = "journeysdata/findjourneys/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            JourneysDto SelectedJourneys = response.Content.ReadAsAsync<JourneysDto>().Result;
            Debug.WriteLine("Journeys received : ");
            Debug.WriteLine(SelectedJourneys.JourneyTitle);

            ViewModel.SelectedJourneys = SelectedJourneys;

            //showcase information about animals related to this journeys
            //send a request to gather information about animals related to a particular species ID
            url = "destinationdata/listdestinationsforjourneys/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DestinationDto> RelatedDestinations = response.Content.ReadAsAsync<IEnumerable<DestinationDto>>().Result;

            ViewModel.RelatedDestinations = RelatedDestinations;


            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Journeys/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Journeys/Create
        [HttpPost]
        public ActionResult Create(Journey journeys)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Journeys.JourneyTitle);
            //objective: add a new journeys into our system using the API
            //curl -H "Content-Type:application/json" -d @journeys.json https://localhost:44362/api/journeysdata/addJourneys
            string url = "journeysdata/addjourneys";


            string jsonpayload = jss.Serialize(journeys);
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
        // GET: Journeys/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "journeysdata/findjourneys/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            JourneysDto selectedJourneys = response.Content.ReadAsAsync<JourneysDto>().Result;
            return View(selectedJourneys);
        }

        // POST: Journeys/Update/5
        [HttpPost]
        public ActionResult Update(int id, Journey journeys)
        {

            string url = "journeysdata/updatejourneys/" + id;
            string jsonpayload = jss.Serialize(journeys);
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

        // GET: Journeys/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "journeysdata/findjourneys/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            JourneysDto selectedJourneys = response.Content.ReadAsAsync<JourneysDto>().Result;
            return View(selectedJourneys);
        }

        // POST: Journeys/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "journeysdata/deletejourneys/" + id;
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