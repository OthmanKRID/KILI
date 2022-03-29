using Kili.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;


namespace Kili.ViewModels
{
    public class AssociationViewModel
    {
        public List<Association> Associations { get; set; }
        public Association association { get; set; }

       // public HttpPostedFileBase file;

        public bool Authentifie { get; set; }

        public RechercheTheme RechercheTheme { get; set; }

        public AssociationViewModel()
        {
            association = new Association();
            Associations = new List<Association>();
        }
    }
}
