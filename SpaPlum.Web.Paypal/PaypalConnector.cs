using SpaPlum.Web.Paypal.paypalProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaPlum.Web.Paypal
{
    public class PaypalConnector
    {
        /// <summary>
        /// Carrega o cabeçalho com dados de acesso do Paypal
        /// </summary>
        /// <returns></returns>
        private paypalProxy.CustomSecurityHeaderType getCredentials()
        {
            paypalProxy.CustomSecurityHeaderType header = new paypalProxy.CustomSecurityHeaderType();
            header.Credentials = new paypalProxy.UserIdPasswordType();

            header.Credentials.Username = "";
            header.Credentials.Password = "";
            header.Credentials.Signature = "";
            header.Credentials.Subject = "";

            return header;
        }

        /// <summary>
        /// Gera o objeto de armazenamento de valor do Paypal
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        private paypalProxy.BasicAmountType getAmountValue(Decimal valor)
        {
            paypalProxy.BasicAmountType amt = new paypalProxy.BasicAmountType();
            amt.currencyID = paypalProxy.CurrencyCodeType.BRL;
            amt.Value = valor.ToString("N2").Replace(",", ".");

            return amt;
        }

        /// <summary>
        /// Inicia o pagamento através do Paypal
        /// </summary>
        /// <param name="venda"></param>
        /// <param name="emailComprador"></param>
        /// <param name="valor"></param>
        /// <param name="valorFrete"></param>
        /// <param name="shipToName"></param>
        /// <param name="shipToStreet"></param>
        /// <param name="shipToStreet2"></param>
        /// <param name="shipToCity"></param>
        /// <param name="shipToState"></param>
        /// <param name="shipToZip"></param>
        /// <param name="shipToCountryCode"></param>
        /// <param name="billingToName"></param>
        /// <param name="billingToStreet"></param>
        /// <param name="billingToStreet2"></param>
        /// <param name="billingToCity"></param>
        /// <param name="billingToState"></param>
        /// <param name="billingToZip"></param>
        /// <param name="billingToCountryCode"></param>
        /// <returns></returns>
        public paypalProxy.SetExpressCheckoutResponseType ExpressCheckout(Object agendamento,
                                                                          string emailComprador, Decimal valor, Decimal valorFrete,
                                                                          string shipToName, string shipToStreet, string shipToStreet2,
                                                                          string shipToCity, string shipToState, string shipToZip,
                                                                          string shipToCountryCode,
                                                                          string billingToName, string billingToStreet, string billingToStreet2,
                                                                          string billingToCity, string billingToState, string billingToZip,
                                                                          string billingToCountryCode)
        {
            #region Cabeçalho de Identificação
            paypalProxy.CustomSecurityHeaderType header = getCredentials();
            #endregion

            #region Corpo da Requisição
            paypalProxy.SetExpressCheckoutReq requisicao = new paypalProxy.SetExpressCheckoutReq();

            #region Configuração da Requisição
            requisicao.SetExpressCheckoutRequest = new paypalProxy.SetExpressCheckoutRequestType();

            requisicao.SetExpressCheckoutRequest.Version = "109.0";
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails = new paypalProxy.SetExpressCheckoutRequestDetailsType();
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.OrderTotal = getAmountValue(valor);
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BuyerEmail = emailComprador;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.ReturnURL = "PaypalReturnURL";
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.CancelURL = "PaypalCancelURL";
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.PaymentAction = paypalProxy.PaymentActionCodeType.Sale;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.ReqConfirmShipping = "0";
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.AddressOverride = "0";
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.AllowNote = "0";
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BrandName = "PaypalBrandName";
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.ShippingMethod = ShippingServiceCodeType.CustomCode;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.cppheaderimage = "PaypalLogo";
            #endregion

            #region Detalhe do Endereço de Entrega
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.Address = new paypalProxy.AddressType();
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.Address.Name = shipToName;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.Address.Street1 = shipToStreet;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.Address.Street2 = shipToStreet2;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.Address.CityName = shipToCity;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.Address.StateOrProvince = shipToState;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.Address.PostalCode = shipToZip;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.Address.Country = paypalProxy.CountryCodeType.BR;
            #endregion

            #region Detalhe do Endereço de Cobrança
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BillingAddress = new paypalProxy.AddressType();
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BillingAddress.Name = billingToName;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BillingAddress.Street1 = billingToStreet;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BillingAddress.Street2 = billingToStreet2;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BillingAddress.CityName = billingToCity;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BillingAddress.StateOrProvince = billingToState;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BillingAddress.PostalCode = billingToZip;
            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.BillingAddress.Country = paypalProxy.CountryCodeType.BR;
            #endregion

            #region Detalhe de Pagamento
            PaymentDetailsType paymentDetail = new PaymentDetailsType();

            #region ITENS
            /*
                        List<PaymentDetailsItemType> paymentItems = new List<PaymentDetailsItemType>();
                        foreach (var item in agendamento.ItensDoAgendamento)
                        {
                            PaymentDetailsItemType itemPaypal = new PaymentDetailsItemType();
                            itemPaypal.Name = item.Produto;
                            //itemPaypal.Description = item.Produto;
                            itemPaypal.Amount = getAmountValue(item.valorTotal);
                            itemPaypal.Quantity = item.qtd.ToString();
                            itemPaypal.ItemURL = item.UrlImagem;

                            paymentItems.Add(itemPaypal);
                        }
                        */

            #region DESCONTO
            /*
            // Segundo o Ariel, da Paypal, tanto o desconto no produto como o desconto do item sobem 
            // como uma linha de desconto no item do detalhe de pagamento com o valor negativo.
            if (venda.isDesconto)
            {
                PaymentDetailsItemType itemPaypal = new PaymentDetailsItemType();
                itemPaypal.Name = "Desconto";
                itemPaypal.Description = "Desconto";
                itemPaypal.Amount = getAmountValue(venda.valorDesconto * (-1));
                itemPaypal.Quantity = "1";

                paymentItems.Add(itemPaypal);
            }
            */
            #endregion

            paymentDetail.ItemTotal = getAmountValue(valor);
            #endregion

            #region FRETE
            /*
            if (valorFrete > 0)
            {
                paymentDetail.ShippingTotal = getAmountValue(valorFrete);
            }

            if (venda.valorFreteDesconto > 0)
            {
                paymentDetail.ShippingDiscount = getAmountValue(venda.valorFreteDesconto);
            }
            */
            #endregion

            //paymentDetail.OrderDescription = string.Format("Pedido {0} Nº {1}", "PaypalBrandName", agendamento.CodigoAgendamento);
            //paymentDetail.PaymentDetailsItem = paymentItems.ToArray();
            paymentDetail.AllowedPaymentMethod = AllowedPaymentMethodType.AnyFundingSource;
            //paymentDetail.InvoiceID = agendamento.CodigoAgendamento.ToString();

            List<PaymentDetailsType> paymentDetails = new List<PaymentDetailsType>();
            paymentDetails.Add(paymentDetail);

            requisicao.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails.PaymentDetails = paymentDetails.ToArray();
            #endregion
            #endregion

            #region Envio do envelope ao Paypal e preparando resposta
            paypalProxy.SetExpressCheckoutResponseType tipo = null;
            paypalProxy.PayPalAPIAAInterfaceClient cliente = new paypalProxy.PayPalAPIAAInterfaceClient();
            tipo = cliente.SetExpressCheckout(ref header, requisicao);
            #endregion

            return tipo;
        }

        public GetExpressCheckoutDetailsResponseType GetShippingDetails(string token, ref string PayerId, ref string ShippingAddress, ref string retMsg)
        {
            #region Cabeçalho de Identificação
            paypalProxy.CustomSecurityHeaderType header = getCredentials();
            #endregion

            #region Corpo da Requisição
            paypalProxy.GetExpressCheckoutDetailsReq requisicao = new paypalProxy.GetExpressCheckoutDetailsReq();

            #region Configuração da Requisição
            requisicao.GetExpressCheckoutDetailsRequest = new paypalProxy.GetExpressCheckoutDetailsRequestType();
            requisicao.GetExpressCheckoutDetailsRequest.Token = token;
            #endregion

            #endregion

            paypalProxy.GetExpressCheckoutDetailsResponseType tipo = null;
            paypalProxy.PayPalAPIAAInterfaceClient cliente = new paypalProxy.PayPalAPIAAInterfaceClient();
            tipo = cliente.GetExpressCheckoutDetails(ref header, requisicao);

            if (tipo.Ack == AckCodeType.Success || tipo.Ack == AckCodeType.SuccessWithWarning)
            {
                /*
                ShippingAddress = "<table><tr>";
                ShippingAddress += "<td> First Name </td><td>" + decoder["FIRSTNAME"] + "</td></tr>";
                ShippingAddress += "<td> Last Name </td><td>" + decoder["LASTNAME"] + "</td></tr>";
                ShippingAddress += "<td colspan='2'> Shipping Address</td></tr>";
                ShippingAddress += "<td> Name </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTONAME"] + "</td></tr>";
                ShippingAddress += "<td> Street1 </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOSTREET"] + "</td></tr>";
                ShippingAddress += "<td> Street2 </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOSTREET2"] + "</td></tr>";
                ShippingAddress += "<td> City </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOCITY"] + "</td></tr>";
                ShippingAddress += "<td> State </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOSTATE"] + "</td></tr>";
                ShippingAddress += "<td> Zip </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOZIP"] + "</td>";
                ShippingAddress += "</tr>";
                */
            }
            else
            {
                /*
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];
                */
            }
            return tipo;
        }

        /*
        public bool ConfirmPayment(string finalPaymentAmount, string token, string PayerId, ref NVPCodec decoder, ref string retMsg)
        { 
                encoder["METHOD"] = "DoExpressCheckoutPayment";
        encoder["TOKEN"] = token;
        encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
        encoder["PAYERID"] = PayerId;
        encoder["PAYMENTREQUEST_0_AMT"] = finalPaymentAmount;
		encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD";

        }
        */
    }
}
