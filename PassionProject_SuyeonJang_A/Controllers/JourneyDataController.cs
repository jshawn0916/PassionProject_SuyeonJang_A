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
    public class JourneyDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Journey in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Journey in the database, including their associated journeys.
        /// </returns>
        /// <example>
        /// GET: api/JourneysData/ListJourneys
        /// </example>
        [HttpGet]
        [ResponseType(typeof(JourneysDto))]
        public IHttpActionResult ListJourneys()
        {
            List<Journey> journeys = db.Journeys.ToList();
            List<JourneysDto> JourneysDtos = new List<JourneysDto>();

            journeys.ForEach(j => JourneysDtos.Add(new JourneysDto()
            {
                JourneyId = j.JourneyId,
                JourneyTitle = j.JourneyTitle,
            }));

            return Ok(JourneysDtos);
        }

        /// <summary>
        /// Returns all Journeys in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A Journey in the system matching up to the Journey ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Journey</param>
        /// <example>
        /// GET: api/JourneysData/FindJourneys/5
        /// </example>
        [ResponseType(typeof(JourneysDto))]
        [HttpGet]
        public IHttpActionResult FindJourneys(int id)
        {
            Journey Journeys = db.Journeys.Find(id);
            JourneysDto JourneysDto = new JourneysDto()
            {
                JourneyId = Journeys.JourneyId,
                JourneyTitle = Journeys.JourneyTitle,
            };
            if (Journeys == null)
            {
                return NotFound();
            }

            return Ok(JourneysDto);
        }

        /// <summary>
        /// Updates a particular Journeys in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Journeys ID primary key</param>
        /// <param name="Journeys">JSON FORM DATA of an Journeys</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/JourneysData/UpdateJourneys/5
        /// FORM DATA: Journeys JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateJourneys(int id, Journey Journeys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Journeys.JourneyId)
            {

                return BadRequest();
            }

            db.Entry(Journeys).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JourneysExists(id))
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
        /// <summary>
        /// Adds an Journeys to the system
        /// </summary>
        /// <param name="Journeys">JSON FORM DATA of an Journeys</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Journeys ID, Journeys Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/JourneysData/AddJourneys
        /// FORM DATA: Journeys JSON Object
        /// </example>
        [ResponseType(typeof(Journey))]
        [HttpPost]
        public IHttpActionResult AddJourneys(Journey Journeys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Journeys.Add(Journeys);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Journeys.JourneyId }, Journeys);
        }

        /// <summary>
        /// Deletes an Journeys from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Journeys</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/JourneysData/DeleteJourneys/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Journey))]
        [HttpPost]
        public IHttpActionResult DeleteJourneys(int id)
        {
            Journey Journeys = db.Journeys.Find(id);
            if (Journeys == null)
            {
                return NotFound();
            }

            db.Journeys.Remove(Journeys);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JourneysExists(int id)
        {
            return db.Journeys.Count(e => e.JourneyId == id) > 0;
        }
    }
}
