using Kili.Models;
using Kili.Models.GestionAdhesion;
using System;
using System.Collections.Generic;

namespace Kili.Models.General
{
    public class Service
    {
        public int Id { get; set; }
        public double prix  { get; set; }
        public int duree_mois { get; set; }
        public TypeService TypeService { get; set; }

    }

    public enum TypeService
    {
        Adhesion,
        Don,
        Boutique
    }

}

