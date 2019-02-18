using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// SetCredEnum
    /// </summary>
    public enum CmfrecSetCredEnum
    {
        /// <summary>
        /// 'AsPerInvoiceType' <= As Per Invoice Type
        /// </summary>
        [Description("As Per Invoice Type")]
        AsPerInvoiceType = 0,

        /// <summary>
        /// 'SettDiscountFixed' <= Sett Discount FIXED
        /// </summary>
        [Description("Sett Discount FIXED")]
        SettDiscountFixed = 1,

        /// <summary>
        /// 'SettDiscountAmend' <= Sett Discount AMEND
        /// </summary>
        [Description("Sett Discount AMEND")]
        SettDiscountAmend = 2,

        /// <summary>
        /// 'CredChargeFixed' <= Cred Charge FIXED
        /// </summary>
        [Description("Cred Charge FIXED")]
        CredChargeFixed = 3,

        /// <summary>
        /// 'CredChargeAmend' <= Cred Charge AMEND
        /// </summary>
        [Description("Cred Charge AMEND")]
        CredChargeAmend = 4,

    }
}
