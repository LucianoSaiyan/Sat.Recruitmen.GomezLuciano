using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.Implementations
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public string TimeElapsed { get; set; }
    }
}
