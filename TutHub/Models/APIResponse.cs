using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutHub.Models
{
    public class APIResponse<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public string RequestUrl { get; set; }

    }
}
