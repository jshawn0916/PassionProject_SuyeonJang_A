using PassionProject_SuyeonJang_A.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PassionProject_SuyeonJang_A.Controllers
{
    public class DestinationDataController : ApiController
    {
        //utlizing the database connection
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all destinations in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all destinations in the database, including their associated travelers.
        /// </returns>
        /// <example>
        /// GET: api/DestinationData/ListDestinations
        /// </example>
        [HttpGet]
        [ResponseType(typeof(DestinationDto))]
        public IHttpActionResult ListDestinations()
        {
            //sending a query to the database
            //select * from destinations...

            List<Destination> Destinations = db.Destinations.ToList();
            List<DestinationDto> DestinationDtos = new List<DestinationDto>();

            Destinations.ForEach(d => DestinationDtos.Add(new DestinationDto()
            {
                DestinationId = d.DestinationId,
                DestinationName = d.DestinationName,
            }
            ));

            //read through the results...
            //push the results to the list of destinations to return
            return Ok(DestinationDtos);
        }

        /// <summary>
        /// Gathers information about all destinations related to a particular journeys ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all destinations in the database, including their associated journeys matched with a particular journey ID
        /// </returns>
        /// <param name="id">Journey ID.</param>
        /// <example>
        /// GET: api/DestinationData/ListDestinationsForJourneys/3
        /// </example>
        [HttpGet]
        [ResponseType(typeof(DestinationDto))]
        public IHttpActionResult ListDestinationsForJourneys(int id)
        {
            //SQL Equivalent:
            //Select * from destinations where destinations.journeyid = {id}
            List<Destination> Destinations = db.Destinations.Where(a => a.JourneyId == id).ToList();
            List<DestinationDto> DestinationDtos = new List<DestinationDto>();

            Destinations.ForEach(d => DestinationDtos.Add(new DestinationDto()
            {
                DestinationId = d.DestinationId,
                DestinationName = d.DestinationName,
                JourneyId = d.Journey.JourneyId,
                JourneyTitle = d.Journey.JourneyTitle
            }));

            return Ok(DestinationDtos);
        }
        //UpdateDestination
        /// <summary>
        /// Updates a particular destination in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Destination ID primary key</param>
        /// <param name="destination">JSON FORM DATA of an destination</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/DestinationData/UpdateDestination/5
        /// FORM DATA: Destination JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDestination(int id, Destination destinations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != destinations.DestinationId)
            {

                return BadRequest();
            }

            db.Entry(destinations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JourneyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JourneyExists(int id)
        {
            return db.Destinations.Count(e => e.DestinationId == id) > 0;
        }

        //AddDestination
        /// <summary>
        /// Adds a destination to the system
        /// </summary>
        /// <param name="destination">JSON FORM DATA of an destination</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Destination ID, Destination Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/DestinationData/AddDestination
        /// FORM DATA: Destination JSON Object
        /// </example>
        [ResponseType(typeof(Destination))]
        [HttpPost]
        public IHttpActionResult AddDestination(Destination destinations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Destinations.Add(destinations);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = destinations.DestinationId }, destinations);
        }

        //DeleteDestination
        /// <summary>
        /// Deletes a destination from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the destination</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/DestinationData/DeleteDestination/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Destination))]
        [HttpPost]
        public IHttpActionResult DeleteDestination(int id)
        {
            Destination destinations = db.Destinations.Find(id);
            if (destinations == null)
            {
                return NotFound();
            }

            db.Destinations.Remove(destinations);
            db.SaveChanges();

            return Ok();
        }


        private bool DestinationExists(int id)
        {
            return db.Destinations.Count(e => e.DestinationId == id) > 0;
        }


    }
}
