using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpaPlum.Entity
{
    [Serializable]
    [Table("TB_CARRINHO_ITEM")]
    public class CarrinhoItem
    {
        [Key]
        [Display(Name = "CodigoItemCarrinho")]
        [Column("COD_CARRINHO_ITEM",Order = 1)]
        public virtual Int64 CodigoCarrinhoItem { get; set; }

        [Display(Name = "CodigoCarrinho")]
        [Column("COD_CARRINHO", Order = 2)]
        public virtual Int64 CodigoCarrinho { get; set; }

        //

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

        // IDENTIFICADOR DE TIPO DE MASSAGEM

        [Display(Name = "Tipo de Massagem")]
        [Column("COD_TIPO_DE_MASSAGEM", Order = 5)]
        public int TipoDeMassagem { get; set; }

        // IDENTIFICADOR DA QUANTIDADE DE PERIODOS

        [Display(Name = "Qtde Periodos")]
        [Column("NUM_QTDE_PERIODOS", Order = 6)]
        public int QtdePeriodos { get; set; }

        [Display(Name = "Valor Unitário")]
        [Column("VAL_PRECO", Order = 7)]
        public decimal Preco { get; set; }

    }
}