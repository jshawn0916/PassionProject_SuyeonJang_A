using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_SuyeonJang_A.Models.ViewModels
{
    public class DetailsJourney
    {
        //the journey itself that we want to display
        public JourneysDto SelectedJourneys { get; set; }

        //all of the related destination to that particular journey
        public IEnumerable<DestinationDto> RelatedDestinations { get; set; }
    }
}