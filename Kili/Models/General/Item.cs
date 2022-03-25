using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kili.Models.General;

namespace Kili.Models.General
{
    public class Item
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public int PanierServiceId { get; set; }
        public virtual PanierService PanierService { get; set; }
    }
}
