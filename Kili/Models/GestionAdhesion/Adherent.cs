using Kili.Models.General;
using System.Collections.Generic;

namespace Kili.Models.GestionAdhesion
{
    public class Adherent
    {
        public int Id { get; set; }

        public uint NumeroAdherent { get; set; }

        public Adresse Adresse { get; set; }
        public string Telephone { get; set; }

        public bool Actif { get; set; }

        public virtual ICollection<Cotisation> Cotisation { get; set; }
    }
}
