using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// DefScFromEnum
    /// </summary>
    public enum DeprecDefScFromEnum
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
