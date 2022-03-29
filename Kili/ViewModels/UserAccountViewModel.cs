using Kili.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kili.ViewModels
{
    public class UserAccountViewModel
    {
        public UserAccount UserAccount { get; set; }
        public bool Authentifie { get; set; }

        public string OldPassword   { get; set; }
        public string ConfirmationPassword { get; set; }
        public string NewPassword { get; set; }
        public string Message { get; set; }

    }
}
