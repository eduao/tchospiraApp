using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TchospiraApp.Models;

namespace TchospiraApp.ViewModels
{
    class ListPollsViewModel
    {

        private IMobileServiceClient _client;
        private IMobileServiceTable<Survey> _surveyTable;
        private const string Url = "http://tchospira.azurewebsites.net"; // aqui ate onde sei seria o endereço da tabela/
        
        public async Task<IEnumerable<Survey>> GetDados()
        {
            try
            {
                return await _surveyTable.ToEnumerableAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ListPollsViewModel()
        {

            _client = new MobileServiceClient(Url);
            _surveyTable = _client.GetTable<Survey>();

        }
    }
}
