$(document).ready(function () {

    var Items = new Object();
    Items.ItensDeAgendamento = new Array();    

    $("#QuantidadePeriodos").val(1);
    $('#Valor').val('0,00');
    $('#ValorTaxaAdicional').val('0');

    var tipoMassagem = "0";
    var radios = document.querySelectorAll('input[type=radio][name=TipoDeMassagem]');
    for (var i = 0; i < radios.length; i++) {
        if (i === 0) {
            tipoMassagem = i.toString();
            radios[i].checked = true;
        }
    }
    $('#TipoDeMassagem').val(tipoMassagem);

    $('#btnLimparSacola').click(function () {
        Items.ItensDeAgendamento = [];
        
        $('#resultado').empty();
        $('#resultado').val('Nenhuma Terapia adicionado na sacola<br /><br />');
    });


    $('#btnAdicionar').click(function () {

        /* INICIALIZA VARIAVEIS */
        var var_idservico = $("#CodigoServico").val();
        var var_nome = "";
        var var_tipomassagem = "";
        var var_qtdeperiodos = 1;
        var var_precoservico = 0;
        var var_codigoterapeuta = 0;
        var var_nometerapeuta = "";

        /* CARREGA OS DADOS DO SERVICO */
        $.ajax({
            url: '/Agendamento/ObtemPrecoServico',
            type: 'POST',
            data: { codigoServico: $("#CodigoServico").val() },
            datatype: 'json',
            success: function (data) {
                if (data.EmPromocao === true) {
                    var_precoservico = data.ValorPromocao;
                } else {
                    var_precoservico = data.Valor;
                }

                var var_nome = data.Nome.toString();

                var tipoMassagem = "0";
                var radios = document.querySelectorAll('input[type=radio][name=TipoDeMassagem]');
                for (var i = 0; i < radios.length; i++) {
                    if (radios[i].checked) {
                        tipoMassagem = i.toString();
                    }
                }
                var nomeMassagem = "";

                if (tipoMassagem === "0") {
                     nomeMassagem= "Padrão";
                } else
                    if (tipoMassagem === "1") {
                        nomeMassagem = "4 Mãos";
                    }
                    else {
                        nomeMassagem = "Para Casais";
                    }

                var var_tipomassagem = nomeMassagem;
                var var_qtdeperiodos = $("#QuantidadePeriodos").val();
                var var_codigoterapeuta = $("#CodigoTerapeuta").val();

                var e = document.getElementById("CodigoTerapeuta");
                var strTerapeuta = e.options[e.selectedIndex].text;
                var var_nometerapeuta = strTerapeuta;

                Items.ItensDeAgendamento.push(new Object({ CodigoServico: var_idservico, Nome: var_nome, TipoDeMassagem: tipoMassagem, DescricaoTipoDeMassagem: var_tipomassagem, QtdePeriodos: var_qtdeperiodos, Valor: var_precoservico, CodigoTerapeuta: var_codigoterapeuta, NomeTerapeuta: var_nometerapeuta }));                                

                $("#resultado").empty();
                $("#resultado").append("<table class='table-condensed' width='100%' zoom:'80%'>")
                $("#resultado").append("<tr>");
                $("#resultado").append("<th style='text-align:left'>Terapia</th>");
                $("#resultado").append("<th style='text-align:left'>Terapeuta</th>");
                $("#resultado").append("<th style='text-align:left'>Tipo</th>");
                $("#resultado").append("<th style='text-align:left'>Periodos</th>");
                $("#resultado").append("<th style='text-align:right'>Valor</th>");
                $("#resultado").append("<th style='text-align:right'>Total</th>");
                $("#resultado").append("</tr>");
                var valorTotal = 0;
                $(Items.ItensDeAgendamento).each(function () {
                    $("#resultado").append("<tr>")
                    $("#resultado").append("<td style='text-align:left;width:40%;'>" + this.Nome + "</td>");
                    $("#resultado").append("<td style='text-align:left;width:20%;'>" + this.NomeTerapeuta + "</td>");
                    $("#resultado").append("<td style='text-align:left;width:10%;'>" + this.DescricaoTipoDeMassagem + "</td>");
                    $("#resultado").append("<td style='text-align:right;width:10%'>" + this.QtdePeriodos + "</td>");
                    $("#resultado").append("<td style='text-align:right;width:10%;'>" + this.Valor + "</td>");

                    var fator = 1;
                    if (this.TipoDeMassagem !== "0") {
                        fator = 2;
                    }
                    var totalCalculado = this.Valor * this.QtdePeriodos * fator;
                    $("#resultado").append("<td style='text-align:right;width:10%;'>" + (totalCalculado) + "</td>");
                    $("#resultado").append("</tr>");

                    valorTotal = valorTotal + this.QtdePeriodos * (this.Valor * fator);
                });
                $("#resultado").append("<tr>")
                $("#resultado").append("<td colspan='5'>&nbsp;</td>");
                $("#resultado").append("</tr>")
                $("#resultado").append("<tr>")
                $("#resultado").append("<td style='text-align:left;width:40%;'>&nbsp;</td>");
                $("#resultado").append("<td style='text-align:left;width:20%;'>&nbsp;</td>");
                $("#resultado").append("<td style='text-align:left;width:10%;'>&nbsp;</td>");
                $("#resultado").append("<td style='text-align:right;width:10%'>&nbsp;</td>");
                $("#resultado").append("<td style='text-align:right;width:10%'>&nbsp;</td>");
                $("#resultado").append("<td style='text-align:right;width:10%;'><b>" + valorTotal.toString() + "</b></td>");
                $("#resultado").append("</tr>");
                $("#resultado").append("</table>")
                $("#Valor").val(valorTotal);

                $('#CodigoTerapeuta').val("Selecione uma terapeuta...");
                $('#CodigoServico').val("Selecione uma terapia...");
                $('#TipoDeMassagem').val(0);
                $('#QuantidadePeriodos').val(1);
                //})
            }
        });
    });

    $('#CodigoServico').change(function () {
        $.ajax({
            url: '/Agendamento/TerapeutasPorServicoFilialHorario',
            type: 'POST',
            data: { codigoFilial: $('#CodigoFilial').val(), dataAgendamento: $('#DataInicial').val(), codigoServico: $(this).val() },
            datatype: 'json',
            success: function (data) {
                var options = '';
                if (data) {
                    $.each(data, function () {
                        options += '<option value="' + this.CodigoTerapeuta + '">' + this.Nome + '</option>';
                    });
                } else {
                    options += '<option value="-1">Terapeutas indisponíveis neste serviço/horário.</option>';
                }
                $('#CodigoTerapeuta').prop('disabled', false).html(options);
            }
        });
    });

    $.ajax({
        url: '/Agendamento/ServicosPorFilial',
        type: 'POST',
        data: { codigoFilial: $('#CodigoFilial').val() },
        datatype: 'json',
        success: function (data) {
            var options = '';
            $.each(data, function () {
                options += '<option value="' + this.CodigoServico + '">' + this.Nome + '</option>';
            });
            $('#CodigoServico').prop('disabled', false).html(options);

            $.ajax({
                url: '/Agendamento/TerapeutasPorServicoFilialHorario',
                type: 'POST',
                data: { codigoFilial: $('#CodigoFilial').val(), dataAgendamento: $('#DataInicial').val(), codigoServico: $('#CodigoServico').val() },
                datatype: 'json',
                success: function (data) {
                    var options = '';
                    if (data) {
                        $.each(data, function () {
                            options += '<option value="' + this.CodigoTerapeuta + '">' + this.Nome + '</option>';
                        });
                    } else {
                        options += '<option value="-1">Terapeutas indisponíveis neste serviço/horário.</option>';
                    }
                    $('#CodigoTerapeuta').prop('disabled', false).html(options);
                }
            });
        }
    });

    $.validator.methods.range = function (value, element, param) {
        var val = $.global.parseFloat(value);
        return this.optional(element) || (val >= param[0] && val <= param[1]);
    }

    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    }

    $.validator.methods.date = function (value, element) {
        return true; /*this.optional(element) || /^\d\d?\/\d\d?\/\d\d\d?\d?$/.test(value);*/
    };

});
