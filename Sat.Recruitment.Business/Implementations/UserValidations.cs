using Sat.Recruitment.Business.Abtractions;
using Sat.Recruitment.Cross_Cutting.Methods;
using Sat.Recruitment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using static Sat.Recruitment.Entities.Entity.User;

namespace Sat.Recruitment.Business.Implementations
{
    public class UserValidations : IUserValidations
    {
        #region Check the Type of User

        /// <summary>
        /// Chequea que el tipo de usuario que se ingresa sea valido
        /// </summary>
        /// <param name="typeuser"></param>
        /// <returns></returns>
        public Tuple<string, bool> CheckUserType(string typeuser)
        {
            object user = new object();

            bool checkuser = Enum.TryParse(typeof(TypeUsernum), typeuser, out user);

            return new Tuple<string, bool>(user == null ? UserMethods.El_tipo_de_Usuario_no_existe : user.ToString(), checkuser);
            
        }


        /// <summary>
        /// Chequea que el tipo de usuario que se ingresa sea valido
        /// validando con Enum
        /// </summary>
        /// <param name="_typeuser"></param>
        /// <returns></returns>
        public User.TypeUsernum CheckTypeUserEnum(string _typeuser)
        {
            TypeUsernum typeuser = TypeUsernum.Doesntexist;
            try
            {
                typeuser = (TypeUsernum)Enum.Parse(typeof(TypeUsernum), _typeuser);
            }
            catch
            {
                typeuser = TypeUsernum.Doesntexist;
            }

            return typeuser; ;
        }

        #endregion

        #region Calculate Money
        public decimal GetTotalMoneyFromUser(TypeUsernum userType, decimal money)
           => GetMoneyfromUser(money, CalculateGift(money, GetPercentagefromUser(userType, money), userType));

        // <summary>
        /// Metodo para obtener el decimal que se aplicara a la cuenta a realizar dependiendo el tipo de usuario
        /// y el dinero ingresado
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public decimal GetPercentagefromUser(TypeUsernum userType, decimal money)
        {
            decimal percentage = 0;

            switch (userType)
            {
                case TypeUsernum.Normal:
                    if (money >= 100)
                        percentage = Convert.ToDecimal(0.12);
                    else if (money < 100 && money > 10)
                        percentage = Convert.ToDecimal(0.8);
                    break;
                case TypeUsernum.SuperUser:
                    if (money > 100)
                        percentage = Convert.ToDecimal(0.20);
                    break;
                default:
                    break;
            }

            return percentage;
        }

        public decimal CalculateGift(decimal money, decimal percentage, TypeUsernum User)
            => money * (User.Equals(TypeUsernum.Premium) ? 2 : percentage);

        /// <summary>
        /// Metodo que obtiene el regalo que se le brindara al usuario 
        /// ingresando su saldo mas el Saldo del gift 
        /// </summary>
        /// <param name="Usermoney"></param>
        /// <param name="gift"></param>
        /// <returns></returns>
        public decimal GetMoneyfromUser(decimal Usermoney, decimal gift)
            => Usermoney + gift;

        #endregion        

        public void SetPropertiesUser(ref User user, string typeUsernum,string email,decimal money)
        {
            #region Set Values from Atributes

            user.TypeUser = typeUsernum;
            user.Email = email;
            user.Money = GetTotalMoneyFromUser(CheckTypeUserEnum(user.TypeUser),money);

            #endregion
        }

        public bool CheckisUserIsDuplicated(List<User> ListOfUsars, User user)
            => ListOfUsars.Any(
                            i => i.Email == user.Email || i.Phone == user.Phone
                            || i.Name == user.Name || i.Address == user.Address);

    }
}
