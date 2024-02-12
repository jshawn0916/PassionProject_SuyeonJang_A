using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_SuyeonJang_A.Models.ViewModels
{
    public class DetailsDestination
    {
        public DestinationDto SelectedDestination { get; set; }
        public IEnumerable<TravelerDto> ResponsibleTravelers { get; set; }

        public IEnumerable<TravelerDto> AvailableTravelers { get; set; }
    }
}