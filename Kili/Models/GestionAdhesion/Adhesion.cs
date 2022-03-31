using Kili.Models.General;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.GestionAdhesion
{
    public class Adhesion
    {
        public int Id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public double prix { get; set; }

        public int duree { get; set; }

        public string description { get; set; }

        public TypeDureeAdhesion typeAdhesion { get; set; }
        
        public int? ServiceAdhesionId { get; set; }
        public virtual ServiceAdhesion ServiceAdhesion { get; set; }    
        
        public virtual ICollection<Cotisation> Cotisations { get; set; }

        

        public enum TypeDureeAdhesion
        {
            Annuelle,
            Mensuelle
        }


    }
}
