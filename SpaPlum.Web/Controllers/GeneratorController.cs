using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaPlum.Web.Controllers
{
    public class GeneratorController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: Generator
        [Authorize]
        public ActionResult Index()
        {
            return View("~/Views/Erro/Index");
        }

        [Authorize]
        public ActionResult GeracaoDeDadosParaFacilitar()
        {
            ViewBag.FoiGerado = GenerateAllData();

            return View();
        }

        internal bool GenerateAllData()
        {
            bool retorno = false;
            try
            {
                if (User.Identity.Name.Equals("andre@avmsistemas.net"))
                {
                    try
                    {
                        #region Inclusão de Filiais
                        if (db.FilialModels.Count() == 0)
                        {
                            db.FilialModels.Add(new Entity.Filial
                            {
                                CodigoFilial = 0,
                                Nome = "Corpus SPA Niterói",
                                Ativo = true
                            });

                            db.FilialModels.Add(new Entity.Filial
                            {
                                CodigoFilial = 0,
                                Nome = "Corpus SPA Copacabana",
                                Ativo = true

                            });
                            db.SaveChanges();
                        }
                        #endregion
                    }
                    catch { }

                    try
                    {
                        #region Inclusão de Forma de Pagto
                        if (db.TipoPagamentoModels.Count() == 0)
                        {
                            db.TipoPagamentoModels.Add(new Entity.TipoPagamento
                            {
                                CodigoTipoPagamento = 0,
                                Descricao = "Dinheiro",
                                Ativo = true,
                                Mora = 0,
                                Taxa = 0,
                                TipoMora = "R",
                                TipoTaxa = "R"
                            });
                            db.TipoPagamentoModels.Add(new Entity.TipoPagamento
                            {
                                CodigoTipoPagamento = 0,
                                Descricao = "Cartão de Crédito",
                                Ativo = true,
                                Mora = 0,
                                Taxa = 0,
                                TipoMora = "R",
                                TipoTaxa = "R"
                            });
                            db.TipoPagamentoModels.Add(new Entity.TipoPagamento
                            {
                                CodigoTipoPagamento = 0,
                                Descricao = "Cartão de Débito",
                                Ativo = false,
                                Mora = 0,
                                Taxa = 0,
                                TipoMora = "R",
                                TipoTaxa = "R"
                            });
                            db.TipoPagamentoModels.Add(new Entity.TipoPagamento
                            {
                                CodigoTipoPagamento = 0,
                                Descricao = "PayPal",
                                Ativo = false,
                                Mora = 0,
                                Taxa = 0,
                                TipoMora = "R",
                                TipoTaxa = "R"
                            });
                            db.SaveChanges();
                        }

                        #endregion
                    }
                    catch { }

                    try
                    {
                        #region Inclusão de Serviços
                        if (db.ServicoModels.Count() == 0)
                        {
                            // Filial 1
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                // MASSAGEM DESPORTIVA

                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Desportiva - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 120,
                                ValorComissaoEmpresa = 0,
                                ValorComissaoTerapeuta = 0,
                                EmPromocao = false,
                                ValorPromocao = 0,
                                ValorPromocaoComissaoEmpresa = 0,
                                ValorPromocaoComissaoTerapeuta = 0,
                                Filial = db.FilialModels.Find(1)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Desportiva - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 180,
                                ValorComissaoEmpresa = 90,
                                ValorComissaoTerapeuta = 90,
                                EmPromocao = true,
                                ValorPromocao = 160,
                                ValorPromocaoComissaoEmpresa = 80,
                                ValorPromocaoComissaoTerapeuta = 80,
                                Filial = db.FilialModels.Find(1)
                            });

                            // MASSAGEM RELAXANTE

                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Relaxante - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 110,
                                ValorComissaoEmpresa = 55,
                                ValorComissaoTerapeuta = 55,
                                EmPromocao = false,
                                ValorPromocao = 110,
                                ValorPromocaoComissaoEmpresa = 55,
                                ValorPromocaoComissaoTerapeuta = 55,
                                Filial = db.FilialModels.Find(1)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Relaxante - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 170,
                                ValorComissaoEmpresa = 85,
                                ValorComissaoTerapeuta = 85,
                                EmPromocao = true,
                                ValorPromocao = 150,
                                ValorPromocaoComissaoEmpresa = 75,
                                ValorPromocaoComissaoTerapeuta = 75,
                                Filial = db.FilialModels.Find(1)
                            });

                            // MASSAGEM TÂNTRICA

                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Tântrica - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 140,
                                ValorComissaoEmpresa = 70,
                                ValorComissaoTerapeuta = 70,
                                EmPromocao = false,
                                ValorPromocao = 140,
                                ValorPromocaoComissaoEmpresa = 70,
                                ValorPromocaoComissaoTerapeuta = 70,
                                Filial = db.FilialModels.Find(1)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Tântrica - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 200,
                                ValorComissaoEmpresa = 100,
                                ValorComissaoTerapeuta = 100,
                                EmPromocao = true,
                                ValorPromocao = 180,
                                ValorPromocaoComissaoEmpresa = 90,
                                ValorPromocaoComissaoTerapeuta = 90,
                                Filial = db.FilialModels.Find(1)
                            });

                            // MASSAGEM THAILANDESA

                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Thailandesa - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 200,
                                ValorComissaoEmpresa = 100,
                                ValorComissaoTerapeuta = 100,
                                EmPromocao = false,
                                ValorPromocao = 200,
                                ValorPromocaoComissaoEmpresa = 100,
                                ValorPromocaoComissaoTerapeuta = 100,
                                Filial = db.FilialModels.Find(1)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Thailandesa - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 280,
                                ValorComissaoEmpresa = 140,
                                ValorComissaoTerapeuta = 140,
                                EmPromocao = true,
                                ValorPromocao = 250,
                                ValorPromocaoComissaoEmpresa = 125,
                                ValorPromocaoComissaoTerapeuta = 125,
                                Filial = db.FilialModels.Find(1)
                            });

                            // MASSAGEM RHAMA

                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Rhama - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 120,
                                ValorComissaoEmpresa = 60,
                                ValorComissaoTerapeuta = 60,
                                EmPromocao = false,
                                ValorPromocao = 120,
                                ValorPromocaoComissaoEmpresa = 60,
                                ValorPromocaoComissaoTerapeuta = 60,
                                Filial = db.FilialModels.Find(1)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 1,
                                Nome = "Massagem Rhama - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 180,
                                ValorComissaoEmpresa = 90,
                                ValorComissaoTerapeuta = 90,
                                EmPromocao = true,
                                ValorPromocao = 160,
                                ValorPromocaoComissaoEmpresa = 80,
                                ValorPromocaoComissaoTerapeuta = 80,
                                Filial = db.FilialModels.Find(1)
                            });

                            // Filial 2

                            db.ServicoModels.Add(new Entity.Servico
                            {
                                // MASSAGEM DESPORTIVA

                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Desportiva - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 120,
                                ValorComissaoEmpresa = 0,
                                ValorComissaoTerapeuta = 0,
                                EmPromocao = false,
                                ValorPromocao = 0,
                                ValorPromocaoComissaoEmpresa = 0,
                                ValorPromocaoComissaoTerapeuta = 0,
                                Filial = db.FilialModels.Find(2)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Desportiva - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 180,
                                ValorComissaoEmpresa = 90,
                                ValorComissaoTerapeuta = 90,
                                EmPromocao = true,
                                ValorPromocao = 160,
                                ValorPromocaoComissaoEmpresa = 80,
                                ValorPromocaoComissaoTerapeuta = 80,
                                Filial = db.FilialModels.Find(2)
                            });

                            // MASSAGEM RELAXANTE

                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Relaxante - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 110,
                                ValorComissaoEmpresa = 55,
                                ValorComissaoTerapeuta = 55,
                                EmPromocao = false,
                                ValorPromocao = 110,
                                ValorPromocaoComissaoEmpresa = 55,
                                ValorPromocaoComissaoTerapeuta = 55,
                                Filial = db.FilialModels.Find(2)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Relaxante - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 170,
                                ValorComissaoEmpresa = 85,
                                ValorComissaoTerapeuta = 85,
                                EmPromocao = true,
                                ValorPromocao = 150,
                                ValorPromocaoComissaoEmpresa = 75,
                                ValorPromocaoComissaoTerapeuta = 75,
                                Filial = db.FilialModels.Find(2)
                            });

                            // MASSAGEM TÂNTRICA

                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Tântrica - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 140,
                                ValorComissaoEmpresa = 70,
                                ValorComissaoTerapeuta = 70,
                                EmPromocao = false,
                                ValorPromocao = 140,
                                ValorPromocaoComissaoEmpresa = 70,
                                ValorPromocaoComissaoTerapeuta = 70,
                                Filial = db.FilialModels.Find(2)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Tântrica - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 200,
                                ValorComissaoEmpresa = 100,
                                ValorComissaoTerapeuta = 100,
                                EmPromocao = true,
                                ValorPromocao = 180,
                                ValorPromocaoComissaoEmpresa = 90,
                                ValorPromocaoComissaoTerapeuta = 90,
                                Filial = db.FilialModels.Find(2)
                            });

                            // MASSAGEM THAILANDESA

                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Thailandesa - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 200,
                                ValorComissaoEmpresa = 100,
                                ValorComissaoTerapeuta = 100,
                                EmPromocao = false,
                                ValorPromocao = 200,
                                ValorPromocaoComissaoEmpresa = 100,
                                ValorPromocaoComissaoTerapeuta = 100,
                                Filial = db.FilialModels.Find(2)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Thailandesa - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 280,
                                ValorComissaoEmpresa = 140,
                                ValorComissaoTerapeuta = 140,
                                EmPromocao = true,
                                ValorPromocao = 250,
                                ValorPromocaoComissaoEmpresa = 125,
                                ValorPromocaoComissaoTerapeuta = 125,
                                Filial = db.FilialModels.Find(2)
                            });

                            // MASSAGEM RHAMA

                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Rhama - 30 minutos",
                                TempoAproximadoDoServico = 30,
                                Ativo = true,
                                Valor = 120,
                                ValorComissaoEmpresa = 60,
                                ValorComissaoTerapeuta = 60,
                                EmPromocao = false,
                                ValorPromocao = 120,
                                ValorPromocaoComissaoEmpresa = 60,
                                ValorPromocaoComissaoTerapeuta = 60,
                                Filial = db.FilialModels.Find(2)
                            });
                            db.ServicoModels.Add(new Entity.Servico
                            {
                                CodigoServico = 0,
                                CodigoFilial = 2,
                                Nome = "Massagem Rhama - 1 hora",
                                TempoAproximadoDoServico = 60,
                                Ativo = true,
                                Valor = 180,
                                ValorComissaoEmpresa = 90,
                                ValorComissaoTerapeuta = 90,
                                EmPromocao = true,
                                ValorPromocao = 160,
                                ValorPromocaoComissaoEmpresa = 80,
                                ValorPromocaoComissaoTerapeuta = 80,
                                Filial = db.FilialModels.Find(2)
                            });

                            db.SaveChanges();
                        }
                        #endregion
                    }
                    catch { }

                    try
                    {
                        #region Inclusão de Terapeutas
                        if (db.TerapeutaModels.Count() == 0)
                        {
                            // Filial 1
                            db.TerapeutaModels.Add(new Entity.Terapeuta
                            {
                                CodigoFilial = 1,
                                Nome = "Lohane",
                                Ativa = true,
                                CodigoTerapeuta = 0,
                                DataCadastro = DateTime.Now,
                                Filial = db.FilialModels.Find(1)
                            });
                            db.TerapeutaModels.Add(new Entity.Terapeuta
                            {
                                CodigoFilial = 1,
                                Nome = "Denise",
                                Ativa = true,
                                CodigoTerapeuta = 0,
                                DataCadastro = DateTime.Now,
                                Filial = db.FilialModels.Find(1)
                            });
                            db.TerapeutaModels.Add(new Entity.Terapeuta
                            {
                                CodigoFilial = 1,
                                Nome = "Ana",
                                Ativa = true,
                                CodigoTerapeuta = 0,
                                DataCadastro = DateTime.Now,
                                Filial = db.FilialModels.Find(1)
                            });
                            db.TerapeutaModels.Add(new Entity.Terapeuta
                            {
                                CodigoFilial = 1,
                                Nome = "Dayse",
                                Ativa = true,
                                CodigoTerapeuta = 0,
                                DataCadastro = DateTime.Now,
                                Filial = db.FilialModels.Find(1)
                            });
                            db.TerapeutaModels.Add(new Entity.Terapeuta
                            {
                                CodigoFilial = 1,
                                Nome = "Daniela",
                                Ativa = true,
                                CodigoTerapeuta = 0,
                                DataCadastro = DateTime.Now,
                                Filial = db.FilialModels.Find(1)
                            });

                            //Filial 2
                            db.TerapeutaModels.Add(new Entity.Terapeuta
                            {
                                CodigoFilial = 2,
                                Nome = "Carol",
                                Ativa = true,
                                CodigoTerapeuta = 0,
                                DataCadastro = DateTime.Now,
                                Filial = db.FilialModels.Find(2)
                            });
                            db.TerapeutaModels.Add(new Entity.Terapeuta
                            {
                                CodigoFilial = 2,
                                Nome = "Grasi",
                                Ativa = true,
                                CodigoTerapeuta = 0,
                                DataCadastro = DateTime.Now,
                                Filial = db.FilialModels.Find(2)
                            });
                            db.SaveChanges();
                        }
                        #endregion
                    }
                    catch { }

                    try
                    {
                        #region Inclusão de Status de Agendamento
                        /*
                        if (db.StatusAgendamentoModels.Count() == 0)
                        {
                            db.StatusAgendamentoModels.Add(new Entity.StatusAgendamento
                            {
                                CodigoStatusAgendamento = 0,
                                Descricao = "Agendado pelo Site",
                                Ativo = true
                            });
                            // SALVA PARA GARANTIR O CODIGO 1
                            db.SaveChanges();

                            db.StatusAgendamentoModels.Add(new Entity.StatusAgendamento
                            {
                                CodigoStatusAgendamento = 0,
                                Descricao = "Agendado pelo Telefone",
                                Ativo = true
                            });
                            db.StatusAgendamentoModels.Add(new Entity.StatusAgendamento
                            {
                                CodigoStatusAgendamento = 0,
                                Descricao = "Agendado pelo Pessoalmente",
                                Ativo = true
                            });
                            db.StatusAgendamentoModels.Add(new Entity.StatusAgendamento
                            {
                                CodigoStatusAgendamento = 0,
                                Descricao = "Confirmado",
                                Ativo = true
                            });
                            db.StatusAgendamentoModels.Add(new Entity.StatusAgendamento
                            {
                                CodigoStatusAgendamento = 0,
                                Descricao = "Cancelado",
                                Ativo = true
                            });
                            db.StatusAgendamentoModels.Add(new Entity.StatusAgendamento
                            {
                                CodigoStatusAgendamento = 0,
                                Descricao = "Concluído",
                                Ativo = true
                            });
                            db.SaveChanges();
                        }
                        */
                        #endregion
                    }
                    catch { }

                }
            }
            catch
            {
                retorno = false;
            }
            return retorno;
        }


    }
}