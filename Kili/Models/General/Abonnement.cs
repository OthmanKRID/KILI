using Kili.Models;

namespace Kili.Models.General
{
    public class Abonnement
    {
        public int Id { get; set; }
        public int? AssociationId { get; set; }
        public virtual Association Association { get; set; }
    }
}
