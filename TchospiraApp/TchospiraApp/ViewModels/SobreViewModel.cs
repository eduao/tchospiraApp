using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TchospiraApp.Services;

namespace TchospiraApp.ViewModels
{
    class SobreViewModel : BaseViewModel
    {

        private readonly ITchospiraService _tchospiraService;

        public SobreViewModel(ITchospiraService tchospiraService)
        {
            _tchospiraService = tchospiraService;
           
        }
    }
}
