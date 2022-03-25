using Kili.Models;
using System.Collections.Generic;

namespace Kili.Models.General
{
    public class AbonnementService
    {
        public int Id { get; set; }

        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }
//
        public virtual List<Service> ServicesSouscrit { get; set; }
       /*
        public int? AssociationId { get; set; }
        public virtual Association Association { get; set; }*/

    }
}
