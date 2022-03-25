using Kili.Models;
using Kili.Models.Dons;
using System;
using System.Collections.Generic;

namespace Kili.Models.Dons
{
    public class ServiceDon
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }
        public DateTime dateAbonnement { get; set; }
        public DateTime dateFinAbonnement { get; set; }
        public int duree { get; set; }

    }

    
}

