using System;
using java.io;
using io.konik;
using Console = System.Console;

namespace ZUGFeRD{
    public class Invoice{
        public Invoice(){
            var test = new java.io.File("/Users/stevelamb/Development/ibcos/investigations/ZUGFeRD/zugferd_invoice.pdf");
            PdfHandler pdf = new PdfHandler();
            var invoice = pdf.extractInvoice(test);
            var header = invoice.getHeader();
            var trade = invoice.getTrade();
            var agreement = trade.getAgreement();
            var settlement = trade.getSettlement();
            var buyerOrder = agreement.getBuyerOrder();
            var orderNo = buyerOrder.getId();
            var reference = settlement.getPaymentReference();

            Console.WriteLine("done");
            //pdf.
        }
    }
}