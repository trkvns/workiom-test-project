using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workiom_test_project.Models;

namespace workiom_test_project.Data.Interfaces
{
    public interface IDb
    {
        IDbSettings Settings { get; }
        ICompanyRepository Companies { get; }
        IContactRepository Contacts { get; }
    }
}
