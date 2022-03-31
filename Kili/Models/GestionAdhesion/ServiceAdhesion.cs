using Kili.Models;
using Kili.Models.GestionAdhesion;
using System;
using System.Collections.Generic;

namespace Kili.Models.General
{
    public class ServiceAdhesion
    {
        public int? Id { get; set; }

        public bool IsActive { get; set; }
        public DateTime dateAbonnement { get; set; }
        public DateTime dateFinAbonnement { get; set; }
        public int duree { get; set; }

        public virtual ICollection<Adherent> adherents { get; set; }

        public virtual ICollection<Adhesion> adhesions { get; set; }
    }

}

