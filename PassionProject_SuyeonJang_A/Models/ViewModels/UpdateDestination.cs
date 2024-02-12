using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_SuyeonJang_A.Models.ViewModels
{
    public class UpdateDestination
    {
        //This viewmodel is a class which stores information that we need to present to /Destination/Update/{}

        //the existing destination information

        public DestinationDto SelectedDestination { get; set; }

        // all Journeys to choose from when updating this destination

        public IEnumerable<JourneysDto> JourneysOptions { get; set; }
    }
}