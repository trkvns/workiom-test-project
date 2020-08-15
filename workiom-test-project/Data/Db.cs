using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workiom_test_project.Data.Interfaces;
using workiom_test_project.Data.Repositories;
using workiom_test_project.Models;

namespace workiom_test_project.Data
{
    public class Db : IDb
    {
        public IDbSettings Settings { get; private set; }

        public ICompanyRepository Companies { get; private set; }

        public IContactRepository Contacts { get; private set; }

        public Db(IDbSettings settings)
        {
            if (Settings != null)
            {
                Settings = settings;
            }

            Companies = new CompanyRepository(Settings);
            Contacts = new ContactRepository(Settings);
        }
    }
}
