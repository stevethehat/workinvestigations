using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// InvMthdEnum
    /// </summary>
    public enum CmfrecInvMthdEnum
    {
        /// <summary>
        /// 'ImmediateInvoice' <= Immediate Invoice
        /// </summary>
        [Description("Immediate Invoice")]
        ImmediateInvoice = 0,

        /// <summary>
        /// 'BatchInvoice' <= Batch Invoice
        /// </summary>
        [Description("Batch Invoice")]
        BatchInvoice = 1,

        /// <summary>
        /// 'CashSaleInvoice' <= Cash Sale Invoice
        /// </summary>
        [Description("Cash Sale Invoice")]
        CashSaleInvoice = 2,

        /// <summary>
        /// 'BatchInvoiceNoDespatch' <= Batch Invoice No Despatch
        /// </summary>
        [Description("Batch Invoice No Despatch")]
        BatchInvoiceNoDespatch = 3,

        /// <summary>
        /// 'Reserved' <= * Reserved *
        /// </summary>
        [Description("* Reserved *")]
        Reserved = 4,

        /// <summary>
        /// 'SelectAtPos' <= Select At POS
        /// </summary>
        [Description("Select At POS")]
        SelectAtPos = 5,

        /// <summary>
        /// 'DespatchNoteInvoice' <= Despatch Note + Invoice
        /// </summary>
        [Description("Despatch Note + Invoice")]
        DespatchNoteInvoice = 6,

    }
}
