using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sat.Recruitment.Business.Abtractions
{
    public interface IValidations
    {
        public void ValidateErrors<T>(T user,Dictionary<string, string> keyValuePairs, ref string errors);
    }
}
