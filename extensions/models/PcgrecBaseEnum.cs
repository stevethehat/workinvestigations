using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// BaseEnum
    /// </summary>
    public enum PcgrecBaseEnum
    {
        /// <summary>
        /// 'RetailFromPartsDiscount' <= Retail from Parts - Discount
        /// </summary>
        [Description("Retail from Parts - Discount")]
        RetailFromPartsDiscount = 0,

        /// <summary>
        /// 'CostUpliftFromParts' <= Cost + Uplift From Parts
        /// </summary>
        [Description("Cost + Uplift From Parts")]
        CostUpliftFromParts = 1,

        /// <summary>
        /// 'NotAllowed' <= Not Allowed
        /// </summary>
        [Description("Not Allowed")]
        NotAllowed = 2,

    }
}
