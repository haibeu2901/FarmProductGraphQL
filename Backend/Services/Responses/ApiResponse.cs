using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(T data)
        {
            Data = data;
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
        }
    }
}
