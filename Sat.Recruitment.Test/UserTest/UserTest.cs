using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Sat.Recruitment.Entities.DTO;
using Sat.Recruitment.Entities.Entity;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Business.Abtractions;
using Sat.Recruitment.Business.Implementations;
using Sat.Recruitment.Cross_Cutting.Helper;
using Sat.Recruitment.Data;
using Sat.Recruitment.Data.Abstractions;
using Sat.Recruitment.Data.Implementations;


namespace Sat.Recruitment.Test.UserTest
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTest
    {
        #region Interfaces

        private readonly IUserBusiness userBusiness;

        private readonly IValidations _validation;

        private readonly IEmail _email;

        private readonly IUserValidations _userValidations;

        private readonly IDataFromFile _dataFromFile;

        #endregion
        public UserTest()
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

        string RelativePath = @"Files\User\{0}.json";

        #region Create User Test

        [Fact]
        public async Task TestUserValid()
        {
            UsersController userController = new UsersController(userBusiness);

            string file = System.IO.File.ReadAllText(string.Format(RelativePath, "TestUserValid"));

            UserDTO userDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(file);

            Result result = await userController.CreateUser(userDTO);

            Assert.True(result?.IsSuccess);

            Assert.Equal(Helpers.User_Created, result.Message.TrimEnd());

        }

        [Fact]
        public async Task TestUserDuplicated()
        {
            UsersController userController = new UsersController(userBusiness);

            string file = System.IO.File.ReadAllText(string.Format(RelativePath, "TestUserDuplicated"));

            UserDTO userDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(file);

            Result result = await userController.CreateUser(userDTO);

            Assert.Equal(Helpers.User_is_duplicated, result.Message.TrimEnd());

        }

        [Fact]
        public async Task TestUserWithErrors()
        {
            UsersController userController = new UsersController(userBusiness);

            string file = System.IO.File.ReadAllText(string.Format(RelativePath, "TestUserWithErrors"));

            UserDTO userDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(file);

            Result result = await userController.CreateUser(userDTO);

            Assert.Contains(Helpers.required, result.Message);

        }

        #endregion

        #region User Mapper

        [Fact]
        public void UserDTOMapperToUser()
        {
            string file = System.IO.File.ReadAllText(@"Files\User\TestUserValid.json");
            UserDTO userDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(file);
            User user = Entities.Mapper.UserMapper.Map(userDTO);

            Assert.NotNull(user);
        }

        [Fact]
        public void UserMapperToUserDTO()
        {
            UserDTO user = Entities.Mapper.UserMapper.Map(new User("Franco", null, "", "", "Premium", 124));

            Assert.NotNull(user);
        }
        

        #endregion

    }
}
