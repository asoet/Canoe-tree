using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantHunter.Mobile.Web.Data.Models
{
    public class Plant
    {
        public Plant()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string PlantFileUrl { get; set; }
    }
}
