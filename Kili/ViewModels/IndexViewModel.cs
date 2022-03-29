using Kili.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kili.ViewModels
{
    public class IndexViewModel
    {
        public UserAccount UserAccount { get; set; }
        public bool Authentifie { get; set; }
        public List<Association> Associations { get; set; }
        public Association association { get; set; }
    }
}
