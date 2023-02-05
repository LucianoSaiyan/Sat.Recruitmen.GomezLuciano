using Sat.Recruitment.Cross_Cutting.Helper;
using Sat.Recruitment.Data.Abstractions;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Entities.Entity;

namespace Sat.Recruitment.Data.Implementations
{
    public class DataFromFile : IDataFromFile
    {
        public Task<StreamReader> ReadUsersFromFile(string File)
        {
            string path = Directory.GetCurrentDirectory() + $"/Files/{File}.txt";
            StreamReader reader = null;
            if (!FileExists(path))
                throw new Exception(Helpers.The_file_doesnt_exists);
            FileStream fileStream = new FileStream(path, FileMode.Open);
            reader = new StreamReader(fileStream);

            return Task.FromResult(reader);
        }


        static bool FileExists(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
        }
        public async Task<List<User>> GetListUsersfromFile(StreamReader reader)
        {
            List<User> _users = new List<User>();

            while (reader.Peek() >= 0)
            {
                string line = await reader.ReadLineAsync();
                string[] splited_line = Getsplitedline(line);

                User user = new User
                {
                    Name = splited_line[0]?.ToString(),
                    Email = splited_line[1]?.ToString(),
                    Phone = splited_line[2]?.ToString(),
                    Address = splited_line[3]?.ToString(),
                    TypeUser = splited_line[4]?.ToString(),
                    Money = GetDecimal(decimal.Parse(splited_line[5]?.ToString())),
                };
                _users.Add(user);
            }
            reader.Close();
            reader.Dispose();
            return _users;
        }

        static decimal? GetDecimal(decimal convertvalue)
        {
            decimal? returnvalue = convertvalue;
            return returnvalue;
        }
        static string[] Getsplitedline(string line)
        => line.Split(',');

    }
}
