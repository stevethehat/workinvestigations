using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// NoStatementEnum
    /// </summary>
    public enum CmfrecNoStatementEnum
    {
        /// <summary>
        /// 'Yes' <= Yes
        /// </summary>
        [Description("Yes")]
        Yes = 0,

        /// <summary>
        /// 'No' <= No
        /// </summary>
        [Description("No")]
        No = 1,

        /// <summary>
        /// 'YesButBfd' <= Yes but B/Fd
        /// </summary>
        [Description("Yes but B/Fd")]
        YesButBfd = 2,

    }
}
