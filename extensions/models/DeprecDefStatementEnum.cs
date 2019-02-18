using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// DefStatementEnum
    /// </summary>
    public enum DeprecDefStatementEnum
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
        /// 'YesButBfd' <= Yes but B/Fd
        /// </summary>
        [Description("Yes but B/Fd")]
        YesButBfd = 2,

    }
}
