using System.Collections.Generic;
using Kili.Models.Services;

namespace Kili.Models.General
{
    public class PanierService
    {
        public int Id { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}
