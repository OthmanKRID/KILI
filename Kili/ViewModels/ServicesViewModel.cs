using Kili.Models.Dons;
using Kili.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kili.ViewModels
{
    public class ServicesViewModel
    {
        public Abonnement abonnement { get; set; }
        
        public ServiceAdhesion ServiceAdhesion { get; set; }

        public ServiceDon ServiceDon { get; set; }

        public List<Service> listesServicesProposes { get; set; }

    }
}
