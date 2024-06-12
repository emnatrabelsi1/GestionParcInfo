using BlazorApp.Shared.Services.IServices;
using Microsoft.AspNetCore.Components;
using platapp.Domain;

namespace BlazorApp.Client.Pages
{
    public class ParcBase
    {




        public class ParcsBase : ComponentBase
        {
            [Inject]
            public IParcService ParcService { get; set; }
          //  public IEnumerable<AddParcRequest> Parcs { get; set; }

            [Inject]
            public NavigationManager NavigationManager { get; set; }

            public string ErrorMessage { get; set; }
        }
    }
}
