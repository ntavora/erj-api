using erj_api.Models;
using erj_api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace erj_api.Repositories
{
    public class UsersRepository : GenericRepository<Users>, IUserRepository
    {
        public UsersRepository() : base() { }
    }
}
