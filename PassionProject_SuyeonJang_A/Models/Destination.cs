using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject_SuyeonJang_A.Models
{
    public class Destination
    {
        //what discribes a destination?
        //name, category, location
        [Key]
        public int DestinationId { get; set; }

        public string DestinationName { get; set; }

        public string DestinationCategory { get; set; }

        public string DestinationLocation { get; set; }

        // Foriegn key Navigation property
        [ForeignKey("Journey")]
        public int JourneyId { get; set; }
        public virtual Journey Journey { get; set; }


    }
    public class DestinationDto
    {
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }
        public string DestinationCategory { get; set; }

        public string DestinationLocation { get; set; }

        public int JourneyId { get; set; }
        public string JourneyTitle { get; set; }



    }
}