using Sat.Recruitment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Business.Abtractions
{
    public interface IUserBusiness 
    {
        public Task<Tuple<User, string, bool>> CreateValidUser(User user);
    }
}
