using Kili.Models;
using Kili.Models.GestionAdhesion;
using System;
using System.Collections.Generic;

namespace Kili.Models.GestionAdhesion
{
    public class ServiceAdhesion
    {
        public int? Id { get; set; }

        public bool EstActif { get; set; }
        public DateTime dateAbonnement { get; set; }
        public DateTime dateFinAbonnement { get; set; }
        public int duree { get; set; }
        public double montant_paye { get; set; }

        public bool IsActif()
        {
            if (dateFinAbonnement < DateTime.Now)
            {
                EstActif = true;
                return true; 
            }
            else 
            {
                EstActif = false;
                return false;
            }
        }

        public virtual ICollection<Adherent> adherents { get; set; }

        public virtual ICollection<Adhesion> adhesions { get; set; }
    }
}

