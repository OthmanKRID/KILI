using Kili.Models.General;
using System.Collections.Generic;

namespace Kili.Models.GestionAdhesion
{
    public class Adhesion
    {
        public int? Id { get; set; }
        public string nom { get; set; }

        public int duree { get; set; }

        public double prix { get; set; }

        public string description { get; set; }

        public int? ServiceAdhesionId { get; set; }
        public virtual ServiceAdhesion ServiceAdhesion { get; set; }    
        public virtual ICollection<Cotisation> Cotisations { get; set; }
    }
}
