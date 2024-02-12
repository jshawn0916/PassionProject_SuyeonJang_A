using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_SuyeonJang_A.Models
{
    public class Traveler
    {
        //what discribes a traveler?
        //first name, last name, email
        [Key]
        public int TravelerId { get; set; }

        public string TravelerFirstName { get; set; }
        public string TravelerLastName { get; set; }
        public string TravelerEmail { get; set; }
        // Traveler can have many Journeys (One-to-Many)
        public ICollection<Journey> Journeys { get; set; }
    }
    public class TravelerDto
    {
        public int TravelerId { get; set; }
        public string TravelerFirstName { get; set; }
        public string TravelerLastName { get; set; }
        public string TravelerEmail { get; set; }


    }
}