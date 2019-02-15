$(document).ready(function () {
    $('#btnAgendar').click(function () {
        window.location.href = '/Agendamento/WizardPasso1';
    });

    $('#btnPasso1').click(function () {
        window.location.href = '/Agendamento/WizardPasso1';
    });

    $('#btnPasso2').click(function () {
        window.location.href = '/Agendamento/WizardPasso2';
    });

    $('#btnPasso3').click(function () {
        window.location.href = '/Agendamento/WizardPasso3';
    });
    $('#btnPasso4').click(function () {
        window.location.href = '/Agendamento/WizardPasso4';
    });

    $('#btnPasso5').click(function () {
        window.location.href = '/Agendamento/WizardPasso5';
    });

    $('#btnWizardRevisao').click(function () {
        window.location.href = '/Agendamento/WizardRevisao';
    });

    $('#btnGravarAgendamento').click(function () {
        if (Items.ItensDeAgendamento.length > 0) {
            var data = {
                DataInicial: $('#DataInicial').val(),
                CodigoFilial: $('#CodigoFilial').val(),
                Valor: $('#Valor').val(),
                CodigoTipoPagamento: $('#CodigoTipoPagamento').val(),
                NomeCliente: $('#NomeCliente').val(),
                EmailDoCliente: $('#EmailDoCliente').val(),
                Observacao: $('#Observacao').val(),
                CodigoStatusAgendamento: $('#CodigoStatusAgendamento').val(),
                ItensDoAgendamento: Items.ItensDeAgendamento
            }

            $(this).val('Aguarde...');

            $.ajax({
                url: '/Agendamento/GravarAgendamento/',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'JSON',
                contentType: 'application/json',
                success: function (d) {
                    if (d.status === true) {
                        var sucessoURL = '/Agendamento/Agendado?auth=' + d.auth;
                        window.location.href = sucessoURL;
                    } else {
                        $(this).val('Agendar');
                    }
                },
                error: function (d) {
                    $(this).val('Agendar');
                }
            });
            $(this).val('Agendar');

        }
    });
});
