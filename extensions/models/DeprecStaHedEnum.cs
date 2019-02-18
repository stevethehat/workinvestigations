using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// StaHedEnum
    /// </summary>
    public enum DeprecStaHedEnum
    {
        /// <summary>
        /// 'No' <= No
        /// </summary>
        [Description("No")]
        No = 0,

        /// <summary>
        /// 'Yes' <= Yes
        /// </summary>
        [Description("Yes")]
        Yes = 1,

        /// <summary>
        /// 'Top' <= Top
        /// </summary>
        [Description("Top")]
        Top = 2,

    }
}
