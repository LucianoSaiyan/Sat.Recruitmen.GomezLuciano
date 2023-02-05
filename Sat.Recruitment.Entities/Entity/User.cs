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
        public User(string _name, string _email, string _address, string _phone, string _userType, decimal _money)
        {
            Name = _name;
            Email = _email;
            Address = _address;
            Phone = _phone;
            TypeUser = _userType;
            Money = GetDecimal(_money).Value;
            TypeUserEnum = CheckUserTypeEnum(_userType);
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
                _errors.Add(nameof(User.Name), "The name is required");
                _errors.Add(nameof(User.Email), " The email is required");
                _errors.Add(nameof(User.Address), " The address is required");
                _errors.Add(nameof(User.Phone), " The phone is required");
                return _errors;
            }
        }

        #endregion
    }
}
