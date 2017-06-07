using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TchospiraApp.Models;

namespace TchospiraApp.Services
{
    public interface ITchospiraService
    {

        Task<List<New>> GetNewsByTagIdAsync(string tagId);
        Task<List<Tag>> GetTagsAsync();
        Task<List<New>> GetNewsByFilterAsync(string filter);
    }
}
