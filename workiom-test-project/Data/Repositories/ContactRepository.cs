using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workiom_test_project.Data.Interfaces;
using workiom_test_project.Models;

namespace workiom_test_project.Data.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(IDbSettings s) : base(s.ConnectionString, s.DatabaseName, s.ContactsCollectionName)
        {

        }
    }
}
