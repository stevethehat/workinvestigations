using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// CurAltEnum
    /// </summary>
    public enum CmfrecCurAltEnum
    {
        /// <summary>
        /// 'No' <= No
        /// </summary>
        [Description("No")]
        No = 0,

        /// <summary>
        /// 'BaseSummary' <= Base summary
        /// </summary>
        [Description("Base summary")]
        BaseSummary = 1,

        /// <summary>
        /// 'BaseDetail' <= Base detail
        /// </summary>
        [Description("Base detail")]
        BaseDetail = 2,

        /// <summary>
        /// 'EuroSummary' <= Euro summary
        /// </summary>
        [Description("Euro summary")]
        EuroSummary = 3,

        /// <summary>
        /// 'EuroDetail' <= Euro detail
        /// </summary>
        [Description("Euro detail")]
        EuroDetail = 4,

    }
}
