using DompifyAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DompifyAPI.Application.Helpers
{
    public static class ResponseFormatter
    {
        public static IActionResult Success(int statusCode, string message, object? data = null)
        {
            var response = new
            {
                Meta = new
                {
                    Code = statusCode,
                    Status = "success",
                    Message = message
                },
                Data = data
            };

            return new ObjectResult(response) { StatusCode = statusCode };
        }

        public static IActionResult Failure(int statusCode, string message)
        {
            var response = new
            {
                Meta = new
                {
                    Code = statusCode,
                    Status = "error",
                    Message = message
                }
            };

            return new ObjectResult(response) { StatusCode = statusCode };
        }
    }
}
