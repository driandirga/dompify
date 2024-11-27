using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.DTOs
{
    public class Meta
    {
        public int? Code { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }

    public class ResponseSucces
    {
        public Meta? Meta { get; set; }
        public object? Data { get; set; }
    }

    public class ResponseFailure
    {
        public Meta? Meta { get; set; }
    }
}
