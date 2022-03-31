using Kili.Models;
using Kili.Models.Dons;
using Kili.Models.General;
using System;
using System.Collections.Generic;

namespace Kili.Models.Dons
{
    public class ServiceDon
    {
        public int Id { get; set; }

        public bool EstActif { get; set; }
        public DateTime dateAbonnement { get; set; }
        public DateTime dateFinAbonnement { get; set; }
        public int duree { get; set; }
        public ICollection<Collecte> Collectes { get; set; }       
        public virtual Abonnement Abonnement { get; set; }
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

    }

    
}

