using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_SuyeonJang_A.Models.ViewModels
{
    public class DetailsTraveler
    {
        public TravelerDto SelectedTraveler { get; set; }
        public IEnumerable<DestinationDto> KeptDestinations { get; set; }
    }
}