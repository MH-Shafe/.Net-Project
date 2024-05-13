﻿using BLL.DTOs.Admin;
using BLL.Services;
using BLL.Services.Admin;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Uber.Controllers.Admin
{
    public class SignUpController : ApiController
    {
        [HttpGet]
        [Route("api/signup/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = SignUpService.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("api/signup/create")]
        public HttpResponseMessage Create(SignUpDTO signUpDTO)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            SignUpService.Create(signUpDTO);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/signup/all")]
        public HttpResponseMessage GetAll()
        {
            var data = SignUpService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
