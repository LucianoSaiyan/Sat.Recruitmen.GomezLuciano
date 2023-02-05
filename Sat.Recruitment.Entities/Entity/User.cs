using Sat.Recruitment.Entities.DTO;
using Sat.Recruitment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Entities.Entity
{
    public class User //: IListErrors
    {
        #region Constructores
        public User()
        {

        }
        public User(string _name, string _email, string _address, string _phone, string _typeUser, decimal _money)
        {
            Name = _name;
            Email = _email;
            Address = _address;
            Phone = _phone;
            TypeUser = _typeUser;
            Money = GetDecimal(_money).Value;
            TypeUserEnum = CheckUserTypeEnum(_typeUser);
        }

        public User(UserDTO _user)
        {
            Name = _user.Name;
            Email = _user.Email;
            Address = _user.Address;
            Phone = _user.Phone;
            TypeUser = _user.TypeUser;
            Money = GetDecimal(_user.Money).Value;
            TypeUserEnum = CheckUserTypeEnum(_user.TypeUser);
        }


        #endregion
        #region Properties

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string TypeUser { get; set; }       
        public decimal? Money { get; set; }
        public TypeUsernum? TypeUserEnum { get; set; }        

        #endregion        

        public enum TypeUsernum
        {
            Normal, SuperUser, Premium, Doesntexist
        }

        /// <summary>
        /// Retorna el tipo de usuario del enum a traves del string que recibe
        /// </summary>
        /// <param name="_typeuser"></param>
        /// <returns>Retorna el tipo de usuario del enum a traves del string que recibe</returns>
        static TypeUsernum CheckUserTypeEnum(string _typeuser)
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

            return typeuser;
        }

        /// <summary>
        /// Convierte el decimal en decimal nulleable
        /// </summary>
        /// <param name="convertvalue"></param>
        /// <returns>decimal nulleable</returns>
        decimal? GetDecimal(decimal convertvalue)
        {
            decimal? returnvalue = convertvalue;
            return returnvalue;
        }

        #region Errors for Users

        Dictionary<string, string> _errors;
        public Dictionary<string, string> Errors
        {
            get
            {
                _errors = new Dictionary<string, string>();
                _errors.Add(nameof(Name), "The name is required");
                _errors.Add(nameof(Email), "The email is required");
                _errors.Add(nameof(Address), "The address is required");
                _errors.Add(nameof(Phone), "The phone is required");
                _errors.Add(nameof(TypeUser), "The TypeUser is required");
                _errors.Add(nameof(Money), "The must be greater than to 0");
                return _errors;
            }
        }

        #endregion
    }
}
