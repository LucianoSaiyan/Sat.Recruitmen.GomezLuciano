using Sat.Recruitment.Cross_Cutting.Helper;
using Sat.Recruitment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Data.Abstractions
{
    public interface IDataFromFile
    {
        public Task<StreamReader> ReadUsersFromFile(string File);
        public Task<List<User>> GetListUsersfromFile(StreamReader reader);
    }
}
