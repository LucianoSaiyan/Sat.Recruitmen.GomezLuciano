using Sat.Recruitment.Entities.DTO;
using Sat.Recruitment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Entities.Mapper
{
    public static class UserMapper
    {

        public static UserDTO Map(User user)
        {
            return new UserDTO(user);
        }

        public static User Map(UserDTO user)
        {
            return new User(user);
        }

    }
}
