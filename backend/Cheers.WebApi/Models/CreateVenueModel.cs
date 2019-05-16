using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cheers.WebApi.Models
{
    public class CreateVenueModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public string Latitude { get; set; }
    }
}
