namespace mCore.Services.Identity.API.Models.AccountViewModels
{
    using System.Collections.Generic;

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; private set; }

        public bool RememberMe { get; private set; }
    }
}
