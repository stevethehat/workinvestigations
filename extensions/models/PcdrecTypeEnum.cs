using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// TypeEnum
    /// </summary>
    public enum PcdrecTypeEnum
    {
        /// <summary>
        /// 'PartNumber' <= Part Number
        /// </summary>
        [Description("Part Number")]
        PartNumber = 0,

        /// <summary>
        /// 'ProductGroup' <= Product Group
        /// </summary>
        [Description("Product Group")]
        ProductGroup = 1,

    }
}
