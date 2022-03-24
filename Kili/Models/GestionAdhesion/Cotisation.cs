using System;

namespace Kili.Models.GestionAdhesion
{
    public class Cotisation
    {
        public int Id { get; set; }
        public int? AdhesionId { get; set; }
        public virtual Adhesion Adhesion { get; set; }
        public int? AdherentId { get; set; }
        public virtual Adherent Adherent { get; set; }
        public DateTime dateAdhesion { get; set; }
        public DateTime dateFin { get; set; }
    }
}
