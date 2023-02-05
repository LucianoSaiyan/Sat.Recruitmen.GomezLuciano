using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Business.Abtractions;
using Sat.Recruitment.Business.Implementations;
using Sat.Recruitment.Cross_Cutting.Methods;
using Sat.Recruitment.Entities.Entity;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        public UsersController(IUserBusiness _userBusiness)
        {
            this.userBusiness = _userBusiness;
            //it's added that validation of instance for unit testings
            if (userBusiness == null)
                userBusiness = new UserBusiness();

        }
        public UsersController()
        {

        }

        [HttpGet]
        [Route("/Healthcheck")]
        public Result Healthcheck()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            
            stopwatch.Stop();
            return Result(true,$"{stopwatch.ElapsedMilliseconds}",DateTime.Now.ToLongDateString());
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(User user)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {                
                stopwatch.Start();

                Tuple<User, string, bool> resultfrombusiness = await userBusiness.CreateValidUser(user);

                stopwatch.Stop();
                return Result(!resultfrombusiness.Item3,
                    $"{(!string.IsNullOrEmpty(resultfrombusiness.Item2) ? resultfrombusiness.Item2 : UserMethods.User_Created)} ",
                    $" Tiempo Transcurrido en Ms {stopwatch.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                return Result(false, $"{ex}", $" Tiempo Transcurrido en Ms {stopwatch.ElapsedMilliseconds}");
            }
            
        }

        Result Result(bool issuccess,string message,string timeElapsed = null, string error = null)
        {
            Debug.WriteLine(message);
            return new Result()
            {
                IsSuccess = issuccess,
                Message = message,
                Errors = error == null ? String.Empty : error,
                TimeElapsed = timeElapsed
            };
        }

    }

}
