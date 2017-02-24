using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using WebApi.Models.SummonerByName;

namespace WebApi.Controllers
{
    public class TestController : ApiController
    {
        // GET api/<controller>
//        public int Get()
//        {
//            var parser = new JsonParser();
//
//            //return parser.GetMatchById("na", "lose is improve");
//            //return parser.GetStats("na", "lose is improve");
//            return parser.GetChampionId("na", "lose is improve");
//        }

        // GET api/<controller>/5
        [HttpGet]
        public string Get(int token1)
        {
            //var auth = new AuthenticateToken();
            //string token = "tn7zr48xbz4k3xseuq8qpf09wdqg51";
            //string dupa = auth.Authenticate(token);
            return "dupa";
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