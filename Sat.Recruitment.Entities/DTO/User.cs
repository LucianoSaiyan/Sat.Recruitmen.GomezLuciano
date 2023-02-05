using Sat.Recruitment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Entities.DTO
{
    public class UserDTO //: IListErrors
    {
        #region Constructores
        public UserDTO()
        {

        }
        public UserDTO(string _name, string _email, string _address, string _phone, string _typeUser, decimal _money)
        {
            Name = _name;
            Email = _email;
            Address = _address;
            Phone = _phone;
            TypeUser = _typeUser;
            Money = _money;
        }

        public UserDTO(User _user)
        {
            Name = _user.Name;
            Email = _user.Email;
            Address = _user.Address;
            Phone = _user.Phone;
            TypeUser = _user.TypeUser;
            Money = _user.Money.Value;
        }

        #endregion
        #region Properties

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string TypeUser { get; set; }       
        public decimal Money { get; set; }     

        #endregion        

      
    }
}
