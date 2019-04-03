using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaPlum.Entity
{
    [Serializable]
    [Table("TB_SERVICO")]
    public class Servico
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_SERVICO", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoServico { get; set; }

        [ForeignKey("Filial")]
        [Display(Name = "Filial")]
        [Column("COD_FILIAL", Order = 2)]        
        public int CodigoFilial { get; set; }

        public virtual Filial Filial { get; set; }

        [Display(Name = "Descrição")]
        [Column("TXT_NOME", Order = 3)]
        [Required(ErrorMessage = "Informe o nome do serviço")]
        public string Nome { get; set; }

        [Display(Name = "Valor")]
        [Column("VAL_VALOR", Order = 4)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe o valor do serviço")]
        public decimal Valor { get; set; }

        [Display(Name = "Valor Empresa")]
        [Column("VAL_COMISSAO_EMPRESA", Order = 5)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe quanto do valor do serviço irá para a empresa")]
        public decimal ValorComissaoEmpresa { get; set; }

        [Display(Name = "Valor Terapeuta")]
        [Column("VAL_COMISSAO_TERAPEUTA", Order = 6)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe quanto do valor do serviço irá para a terapeuta")]
        public decimal ValorComissaoTerapeuta { get; set; }

        [Display(Name = "Em Promoção")]
        [Column("IND_PROMOCAO", Order = 7)]
        public bool EmPromocao { get; set; }

        [Display(Name = "Valor Promocional")]
        [Column("VAL_PROMOCAO", Order = 8)]
        [DataType(DataType.Currency)]
        public decimal ValorPromocao { get; set; }

        [Display(Name = "Valor Promoção Empresa")]
        [Column("VAL_PROMOCAO_EMPRESA", Order = 9)]
        [DataType(DataType.Currency)]
        public decimal ValorPromocaoComissaoEmpresa { get; set; }

        [Display(Name = "Valor Promoção Terapeuta")]
        [Column("VAL_PROMOCAO_TERAPEUTA", Order = 10)]
        [DataType(DataType.Currency)]
        public decimal ValorPromocaoComissaoTerapeuta { get; set; }

        [Display(Name = "Tempo Aproximado do Serviço")]
        [Column("NUM_TEMPO_SERVICO", Order = 11)]        
        public int TempoAproximadoDoServico { get; set; }

        [Display(Name = "Ativo")]
        [Column("IND_ATIVO", Order = 12)]
        public bool Ativo { get; set; }

        [Display(Name ="Pontos VIP")]
        [Column("NUM_PONTOS_VIP",Order =13)]
        public int PontosVip { get; set; }

        [Display(Name = "Câmbio dos Pontos VIP")]
        [Column("NUM_PONTOS_VIP_CAMBIO", Order = 14)]
        public int PontosVipCambio { get; set; }

        public Servico()
        {
            this.CodigoServico = 0;
            this.CodigoFilial = 0;
            this.Nome = "";
            this.Valor = 0;
            this.ValorComissaoEmpresa = 0;
            this.ValorComissaoTerapeuta = 0;
            this.EmPromocao = false;
            this.ValorPromocao = 0;
            this.ValorPromocaoComissaoEmpresa = 0;
            this.ValorPromocaoComissaoTerapeuta = 0;
            this.TempoAproximadoDoServico = 0;
            this.Ativo = true;
            this.PontosVip = 0;
            this.PontosVipCambio = 0;

            //this.Filial = new Filial();
        }

    }
}
