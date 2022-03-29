using Kili.Models;
using Kili.Models.Dons;
using System.Collections.Generic;

namespace Kili.Models.General
{
    public class Abonnement
    {
        public int Id { get; set; }

        public int? ServiceAdhesionId { get; set; }
        public virtual ServiceAdhesion serviceAdhesion { get; set; }
        public int? ServiceDonId { get; set; }
        public virtual ServiceDon ServiceDon { get; set; }
        //public virtual ServiceAdhesion serviceBoutique { get; set; }

        //public virtual List<Service> ServicesSouscrit { get; set; }
        public virtual Association Association { get; set; }    

        
        public Abonnement()
        {
            serviceAdhesion = new ServiceAdhesion();
            ServiceDon = new ServiceDon();
        }
        


    }
}
