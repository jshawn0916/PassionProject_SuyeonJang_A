using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject_SuyeonJang_A.Models
{
    public class Journey
    {
        //what discribes a journeys?
        //journey title, explain, destination
        [Key]
        public int JourneyId { get; set; }
        public string JourneyTitle { get; set; }
        public string JourneyExplain { get; set; }

        // Foriegn key & Navigation property
        [ForeignKey("Traveler")]
        public int TravelerId { get; set; }
        public virtual Traveler Traveler { get; set; }

        // Journey can have many Destinations (One-to-Many)
        public ICollection<Destination> Destinations { get; set; }

       
    }
    public class JourneysDto
    {
        public int JourneyId { get; set; }
        public string JourneyTitle { get; set; }

    }
}