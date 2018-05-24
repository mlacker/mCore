using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mCore.Services.IdentityServer.Models.ManageViewModels
{
    public class ShowRecoveryCodesViewModel
    {
        public string[] RecoveryCodes { get; set; }
    }
}
