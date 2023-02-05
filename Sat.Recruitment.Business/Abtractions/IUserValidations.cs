using Sat.Recruitment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static Sat.Recruitment.Entities.Entity.User;

namespace Sat.Recruitment.Business.Abtractions
{
    public interface IUserValidations
    {
        public TypeUsernum CheckTypeUserEnum(string _typeuser);
        public Tuple<string, bool> CheckUserType(string typeuser);
        public void SetPropertiesUser(ref User user, string typeUsernum, string email, decimal money);
        public decimal GetPercentagefromUser(TypeUsernum userType, decimal money);
        public decimal GetTotalMoneyFromUser(TypeUsernum userType, decimal money);
        public decimal CalculateGift(decimal money, decimal percentage, TypeUsernum User);
        public decimal GetMoneyfromUser(decimal Usermoney, decimal gift);
        public bool CheckisUserIsDuplicated(List<User> ListOfUsars, User user);
    }
}
