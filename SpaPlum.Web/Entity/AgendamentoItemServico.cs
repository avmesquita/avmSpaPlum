using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace SpaPlum.Web.Entity
{
    [Serializable]
    [Table("TB_AGENDAMENTO_ITEM_SERVICO")]
    public class AgendamentoItemServico
    {
        // CHAVE PRIMARIA INDEPENDENTE

        [Key]
        [Display(Name = "Código")]
        [Column("COD_AGENDAMENTO_ITEM_SERVICO", Order = 1)]
        public int CodigoAgendamentoItemServico { get; set; }

        // IDENTIFICADOR FK DO AGENDAMENTO PAI

        [Display(Name = "Código Agendamento")]
        [Column("COD_AGENDAMENTO", Order = 2)]
        public int CodigoAgendamento { get; set; }

        [ForeignKey("CodigoAgendamento")]
        public virtual Agendamento Agendamento { get; set; }

        // IDENTIFICADOR FK DO SERVICO DO ITEM

        [Display(Name = "Serviço")]
        [Column("COD_SERVICO", Order = 3)]
        public int CodigoServico { get; set; }

        [ForeignKey("CodigoServico")]
        public virtual Servico Servico { get; set; }
        
        // IDENTIFICADOR FK DA TERAPEUTA DO ITEM

        [Display(Name = "Terapeuta")]
        [Column("COD_TERAPEUTA", Order = 4)]
        public int CodigoTerapeuta { get; set; }


        [ForeignKey("CodigoTerapeuta")]
        public virtual Terapeuta Terapeuta { get; set; }

        // IDENTIFICADOR FK DA TERAPEUTA ADICIONAL PARA "4 MAOS"

        [Display(Name = "Terapeuta Adicional")]
        [Column("COD_TERAPEUTA_ADICIONAL", Order = 12)]
        public int? CodigoTerapeutaAdicional { get; set; }


        [ForeignKey("CodigoTerapeutaAdicional")]
        public virtual Terapeuta TerapeutaAdicional { get; set; }

        // IDENTIFICADOR DE TIPO DE MASSAGEM

        [Display(Name = "Tipo de Massagem")]
        [Column("COD_TIPO_DE_MASSAGEM", Order = 5)]
        public int TipoDeMassagem { get; set; }

        // IDENTIFICADOR DA QUANTIDADE DE PERIODOS

        [Display(Name = "Qtde Periodos")]
        [Column("NUM_QTDE_PERIODOS", Order = 6)]
        public int QtdePeriodos { get; set; }

        // PRECO COBRADO PELO SERVICO NA DATA DO AGENDAMENTO

        [Display(Name = "Valor")]
        [Column("VAL_VALOR", Order = 7)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe o valor do serviço")]
        public decimal Valor { get; set; }

        // VALOR A RECEBER PARA A EMPRESA PELO SERVICO

        [Display(Name = "Valor Empresa")]
        [Column("VAL_COMISSAO_EMPRESA", Order = 8)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe quanto do valor do serviço irá para a empresa")]
        public decimal ValorComissaoEmpresa { get; set; }

        // VALOR A RECEBER PARA A TERAPEUTA PELO SERVICO

        [Display(Name = "Valor Terapeuta")]
        [Column("VAL_COMISSAO_TERAPEUTA", Order = 9)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe quanto do valor do serviço irá para a terapeuta")]
        public decimal ValorComissaoTerapeuta { get; set; }

        [Display(Name = "Taxa Pro-rata")]
        [Column("VAL_TAXA_PRO_RATA", Order = 10)]
        [DataType(DataType.Currency)]        
        public decimal? ValorTaxaProRata { get; set; }

        [Display(Name = "Mora Pro-rata")]
        [Column("VAL_MORA_PRO_RATA", Order = 11)]
        [DataType(DataType.Currency)]
        public decimal? ValorMoraProRata { get; set; }


        public AgendamentoItemServico()
        {
            this.CodigoAgendamento = 0;
            this.CodigoAgendamentoItemServico = 0;
            this.CodigoServico = 0;
            this.CodigoTerapeuta = 0;            
            this.QtdePeriodos = 1;
            this.TipoDeMassagem = -1;
            this.Valor = 0;
            this.ValorComissaoEmpresa = 0;
            this.ValorComissaoTerapeuta = 0;
            this.ValorTaxaProRata = 0;
            this.ValorMoraProRata = 0;
        }


    }
}
