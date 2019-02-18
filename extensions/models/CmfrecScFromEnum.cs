using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// ScFromEnum
    /// </summary>
    public enum CmfrecScFromEnum
    {
        /// <summary>
        /// 'InvoiceDate' <= Invoice date
        /// </summary>
        [Description("Invoice date")]
        InvoiceDate = 0,

        /// <summary>
        /// 'SalesMonthEndDate' <= Sales month end date
        /// </summary>
        [Description("Sales month end date")]
        SalesMonthEndDate = 1,

    }
}
