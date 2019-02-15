using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Models
{
	public class IntegradorModel
	{
		public string Mensagem { get; set; }
		public bool Sucesso { get; set; }

		public override string ToString()
		{
			return "{ Sucesso = '" + Sucesso + "', Mensagem = '" + Mensagem + "'}";			
		}
	}
}