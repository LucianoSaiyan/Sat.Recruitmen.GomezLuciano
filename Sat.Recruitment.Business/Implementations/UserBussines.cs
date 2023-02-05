
using Sat.Recruitment.Business.Abtractions;
using Sat.Recruitment.Business.Implementations;
using Sat.Recruitment.Cross_Cutting.Helper;
using Sat.Recruitment.Cross_Cutting.Methods;
using Sat.Recruitment.Data;
using Sat.Recruitment.Data.Abstractions;
using Sat.Recruitment.Data.Implementations;
using Sat.Recruitment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Sat.Recruitment.Entities.Entity.User;

namespace Sat.Recruitment.Business.Implementations
{
    public class UserBusiness : IUserBusiness
    {

        private readonly IValidations validations;
        private readonly IEmail email;
        private readonly IUserValidations userValidations;
        private readonly IDataFromFile dataFromFile;
        StreamReader _getdatafromfile = null;
        #region Constructors

        public UserBusiness(IValidations _validations, IEmail _email, IUserValidations userValidations, IDataFromFile dataFromFile)
        {
            this.validations = _validations;
            this.email = _email;
            this.userValidations = userValidations;
            this.dataFromFile = dataFromFile;
        }

        public UserBusiness()
        {
            if (validations == null)
                validations = new Validations();
            if (email == null)
                email = new Email();
            if (userValidations == null)
                userValidations = new UserValidations();
            if (dataFromFile == null)
                dataFromFile = new DataFromFile();

        }

        #endregion

        public async Task<Tuple<User, string, bool>> CreateValidUser(User user)
        {
            string errors = "";
            bool isDuplicated = false;

            try
            {

                #region Validate if all the properties of business it's ok
                validations?.ValidateErrors(user, user.Errors, ref errors);
                #endregion                

                #region Check the user type
                Tuple<string, bool> validateusertype = userValidations.CheckUserType(user.TypeUser);
                if (!validateusertype.Item2)
                    errors += $"{(errors.Length > 0 ? " ," : " ")} {UserMethods.El_tipo_de_Usuario_no_existe}";
                #endregion

                if (String.IsNullOrEmpty(errors))
                    #region Check if the user Exists
                    try
                    {
                        _getdatafromfile = await dataFromFile.ReadUsersFromFile("Users");
                        List<User> ListOfUsars = await dataFromFile.GetListUsersfromFile(_getdatafromfile);

                        if (ListOfUsars.Count > 0)
                        {
                            isDuplicated = userValidations.CheckisUserIsDuplicated(ListOfUsars, user);
                            if (isDuplicated)
                                errors += Helpers.User_is_duplicated;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().Contains(Helpers.The_file_doesnt_exists))
                            errors += Helpers.The_file_doesnt_exists;
                        else
                            throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        _getdatafromfile = null;
                    }
                #endregion

                #region Set Values from Atributes

                if (String.IsNullOrEmpty(errors))
                    userValidations.SetPropertiesUser(ref user, validateusertype.Item1, email.NormalizeMail(user.Email), user.Money.Value);

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<User, string, bool>(user, errors, isDuplicated);

        }
    }
}
