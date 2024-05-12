﻿using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Uber.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("api/login")]
        public HttpResponseMessage Logins()
        {
            try
            {
                var data = LoginService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Massage = ex.Message });
            }
        }
        // POST api/login by create service
        /*
        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage Login([FromBody] LoginDTO request)
        {
            try
            {
                LoginService.Create(request);
                return Request.CreateResponse(HttpStatusCode.Created, new { Message = "Login created successfully." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Massage = ex.Message });
            }
        }
        */

        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage Login([FromBody] LoginDTO login)
        {
            try
            {
                var data = LoginService.Get();
                var user = data.FirstOrDefault(u => u.username == login.username && u.password == login.password);

                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Message = "Invalid username or password" });
                }

                // Set the user's role in a cookie
                var cookie = new HttpCookie("UserRole");
                cookie.Value = user.roll;
                cookie.Expires = DateTime.Now.AddMinutes(5); // Cookie expires in 5 minutes
                HttpContext.Current.Response.Cookies.Add(cookie);

                // Login successful message
                var response = Request.CreateResponse(HttpStatusCode.OK, new { Message = "Login successful" });

                // Retrieve role from cookie and append it to the response message
                var userRole = HttpContext.Current.Request.Cookies["UserRole"]?.Value;
                if (!string.IsNullOrEmpty(userRole))
                {
                    response.Content = new StringContent(response.Content.ReadAsStringAsync().Result + " - User Role: " + userRole);
                }

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }
}