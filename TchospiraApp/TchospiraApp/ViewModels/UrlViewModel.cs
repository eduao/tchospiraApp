using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TchospiraApp.Helpers;
using TchospiraApp.Models;
using TchospiraApp.Services;

namespace TchospiraApp.ViewModels
{
    class UrlViewModel : BaseViewModel
    {
        private readonly ITchospiraService _tchospiraService;

        public News News { get; set; }

        public UrlViewModel(ITchospiraService service, News news)
        {
            _tchospiraService = service;
            News = news;
        }

        public UrlViewModel(ITchospiraService service)
        {
            _tchospiraService = service;
            News = new News();
            News.Url = Settings.UrlSite;
        }
    }
}
