using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workiom_test_project.Models;

namespace workiom_test_project.Data.Interfaces
{
    public interface IRepository<T> where T : IDocument
    {
        Task<List<T>> GetListAsync();
        Task<T> GetByIdAsync(string id);
        Task<List<T>> GetByIdAsync(List<string> ids);
        Task<T> CreateAsync(T model);
        Task<bool> UpdateAsync(string id, T model);
        Task<bool> DeleteAsync(T model);
        Task<bool> DeleteAsync(string id);
        Task<bool> AddColumnAsync(NewColumn item);
        Task<List<T>> SearchAsync(Dictionary<string, object> queries);
    }
}
