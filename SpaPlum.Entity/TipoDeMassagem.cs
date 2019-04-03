using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Entity
{
    public class TipoDeMassagem
    {
        public int Id { get; set; }
        public string  Name { get; set; }

        public TipoDeMassagem()
        {
            this.Id = 0;
            this.Name = "";
        }
    }




}