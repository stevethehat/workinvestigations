using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// BaseEnum
    /// </summary>
    public enum PcdrecBaseEnum
    {
        /// <summary>
        /// 'PriceInCustomerCurrency' <= Price in Customer Currency
        /// </summary>
        [Description("Price in Customer Currency")]
        PriceInCustomerCurrency = 0,

        /// <summary>
        /// 'DiscountFromPartsRetail' <= Discount % from Parts Retail
        /// </summary>
        [Description("Discount % from Parts Retail")]
        DiscountFromPartsRetail = 1,

    }
}
