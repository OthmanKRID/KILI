using Kili.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kili.ViewModels
{
    public class AssociationViewModel
    {
        public List<Association> Associations { get; set; }
        public Association association { get; set; }

        public bool Authentifie { get; set; }

        public AssociationViewModel()
        {
            association = new Association();
        }
    }
}
