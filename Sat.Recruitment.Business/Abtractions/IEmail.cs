using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sat.Recruitment.Business.Abtractions
{
    public interface IEmail
    {
        public string NormalizeMail(string Email);

    }
}
