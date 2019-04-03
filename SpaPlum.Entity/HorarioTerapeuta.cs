using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpaPlum.Entity
{
    [Serializable]
    [Table("TB_HORARIO_TERAPEUTA")]
    public class HorarioTerapeuta
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_HORARIO_TERAPEUTA", Order = 1)]
        public int CodigoHorarioTerapeuta { get; set; }

        [ForeignKey("Terapeuta")]
        [Display(Name = "Terapeuta")]
        [Column("COD_TERAPEUTA", Order = 2)]
        public int CodigoTerapeuta { get; set; }

        public virtual Terapeuta Terapeuta { get; set; }

        [Display(Name = "Dia da Semana")]
        [Range(0, 6)]
        [Column("COD_DIA_DA_SEMANA", Order = 3)]
        public int CodigoDiaDaSemana { get; set; }

        [Display(Name = "Horário Inicial")]
        [Column("HOR_HORA_INICIO", Order = 4)]
        public string HorarioInicio { get; set; }

        [Display(Name = "Horário Final")]
        [Column("HOR_HORA_FIM", Order = 5)]
        public string HorarioFim { get; set; }


        [NotMapped]
        public bool chkDomingo { get; set; }

        [NotMapped]
        public string txtDomingoHoraEntrada { get; set; }

        [NotMapped]
        public string txtDomingoHoraSaida { get; set; }

        [NotMapped]
        public bool chkSegunda { get; set; }

        [NotMapped]
        public string txtSegundaHoraEntrada { get; set; }

        [NotMapped]
        public string txtSegundaHoraSaida { get; set; }

        [NotMapped]
        public bool chkTerca { get; set; }

        [NotMapped]
        public string txtTercaHoraEntrada { get; set; }

        [NotMapped]
        public string txtTercaHoraSaida { get; set; }

        [NotMapped]
        public bool chkQuarta { get; set; }

        [NotMapped]
        public string txtQuartaHoraEntrada { get; set; }

        [NotMapped]
        public string txtQuartaHoraSaida { get; set; }

        [NotMapped]
        public bool chkQuinta { get; set; }

        [NotMapped]
        public string txtQuintaHoraEntrada { get; set; }

        [NotMapped]
        public string txtQuintaHoraSaida { get; set; }

        [NotMapped]
        public bool chkSexta { get; set; }

        [NotMapped]
        public string txtSextaHoraEntrada { get; set; }

        [NotMapped]
        public string txtSextaHoraSaida { get; set; }

        [NotMapped]
        public bool chkSabado { get; set; }

        [NotMapped]
        public string txtSabadoHoraEntrada { get; set; }

        [NotMapped]
        public string txtSabadoHoraSaida { get; set; }
    }
}