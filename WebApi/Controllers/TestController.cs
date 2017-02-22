using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        public ResultDto Get()
        {
            var parser = new JsonParser();
            parser.Parser("na", "lose is improve");
            return new ResultDto
            {
                Data = "Success"
            };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public ResultDto Test()
        {
            return new ResultDto
            {
                Data = "Success"
            };
        }

        public class ResultDto
        {
            public string Data { get; set; }
        }
    }
}