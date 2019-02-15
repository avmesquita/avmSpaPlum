using SpaPlum.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Results;
using SpaPlum.Web.Entity;

namespace SpaPlum.Web.Controllers
{
    public class IntegradorController : ApiController
    {

		[HttpPost]
		public string ProcessarCriacaoEmpresa(string user,string pass,string token,string hash)
		{






			var resposta = new IntegradorModel()
			{
				Mensagem = "",
				Sucesso = true
			};

			return resposta.ToString();
		}


        // GET: api/Integrador
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Integrador/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Integrador
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Integrador/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Integrador/5
        public void Delete(int id)
        {
        }
    }
}
