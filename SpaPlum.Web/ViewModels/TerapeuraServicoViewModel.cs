using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaPlum.Web.ViewModels
{
    public class TerapeuraServicoViewModel
    {
        public TerapeutaServico terapeutaServico { get; set; }

        public List<Terapeuta> listaTerapeutas { get; set; }

        public List<Servico> listaServico { get; set; }

        public MultiSelectList servicos { get; set; }

    }
}