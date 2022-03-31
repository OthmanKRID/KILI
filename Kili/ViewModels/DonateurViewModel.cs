using Kili.Models.Dons;
using Kili.Models.General;

namespace Kili.ViewModels
{
    public class DonateurViewModel
    {
        public Donateur Donateur { get; set; }
        public Adresse Adresse { get; set; }
        public bool Authentifie { get; set; }

    }
}
