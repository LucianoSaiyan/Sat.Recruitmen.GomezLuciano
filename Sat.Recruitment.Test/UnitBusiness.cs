using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Business.Abtractions;
using Sat.Recruitment.Business.Implementations;
using Sat.Recruitment.Cross_Cutting.Helper;
using Sat.Recruitment.Data;
using Sat.Recruitment.Data.Abstractions;
using Sat.Recruitment.Data.Implementations;
using Sat.Recruitment.Entities.DTO;
using Sat.Recruitment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using static Sat.Recruitment.Entities.Entity.User;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitBusiness
    {
        #region Interfaces

        private readonly IUserBusiness userBusiness;

        private readonly IValidations _validation;

        private readonly IEmail _email;

        private readonly IUserValidations _userValidations;

        private readonly IDataFromFile _dataFromFile;

        #endregion

        string RelativePath = @"Files\User\{0}.json";
        public UnitBusiness()
        {
            #region Create Instances

            if (userBusiness == null)
                userBusiness = new UserBusiness();

            if (_validation == null)
                _validation = new Validations();

            if (_email == null)
                _email = new Email();

            if (_userValidations == null)
                _userValidations = new UserValidations();

            if (_dataFromFile == null)
                _dataFromFile = new DataFromFile();
            #endregion
        }

        #region Validations from Model

        [Fact]
        public void TestValidateErrorsfromModel()
        {
            string file = System.IO.File.ReadAllText(string.Format(RelativePath, "TestUserWithErrors"));
            UserDTO userDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(file);
            User user = Entities.Mapper.UserMapper.Map(userDTO); 
            string errors = string.Empty;

            //Validate Errors trough Reflection
            _validation.ValidateErrors(user, user.Errors, ref errors);

            if (!String.IsNullOrEmpty(errors))
                Assert.True(errors.Length > 0);
            else
                Assert.NotEqual(string.Empty, errors);
        }

        #endregion

        #region Tests Money User

        [Fact]
        public void TestCalculateMoneyUser()
        {

            decimal testmethod = _userValidations.GetPercentagefromUser(TypeUsernum.Normal, 101);

            Assert.True(testmethod > 0);
        }

        [Fact]
        public void TestGetGiftfromUser()
        {
            decimal testmethod = _userValidations.CalculateGift(101, (decimal)0.12, TypeUsernum.Normal);

            Assert.True(testmethod > 0);
        }

        [Fact]
        public void TestGetMoneyfromUser()
        {
            decimal testmethod = _userValidations.GetMoneyfromUser((decimal)0.12, 200);

            Assert.True(testmethod > 0);
        }

        [Fact]
        public void TestGetTotalMoneyFromUser()
        {
            decimal testmethod = _userValidations.GetTotalMoneyFromUser(TypeUsernum.Normal, 200);

            Assert.True(testmethod > 0);
        }

        #endregion

        #region Type User

        [Fact]
        public void TestCheckUserTypeEnum()
        {
            TypeUsernum CheckUserTypeEnum = _userValidations.CheckTypeUserEnum("SuperUser");

            Assert.NotEqual(TypeUsernum.Doesntexist, CheckUserTypeEnum);
        }

        [Fact]
        public void TestCheckUserType()
        {
            Tuple<string, bool> tuple = _userValidations.CheckUserType("Premium");

            if (tuple.Item2)
                Assert.True(tuple.Item2);
            else
                Assert.False(tuple.Item2);

        }

        #endregion

        #region Mail

        [Fact]
        public void TestNormalizeMail()
        {
            string mail = _email.NormalizeMail("mail@gmail.com");

            Assert.Contains("@", mail);

        }

        #endregion

    }
}
