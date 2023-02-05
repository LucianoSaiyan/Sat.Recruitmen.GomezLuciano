namespace Sat.Recruitment.Entity.Entities
{
    public class UUUser
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
            UserType = _userType;
            Money = _money;
        }

        public User(string _name, string _email, string _address, string _phone, string _userType)
        {
            Name = _name;
            Email = _email;
            Address = _address;
            Phone = _phone;
            UserType = _userType;
        }


        #endregion
        #region Properties

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }

        decimal? _money;
        public decimal? Money
        {
            get
            {                   
                return _money;
            }
            set
            {
                value = _money;
            }
        }

        #endregion

        public TypeUser TypeUserEnum { get; set; }
        public enum TypeUser
        {
            Normal, SuperUser, Premium, Doesntexist
        }


    }
}
