using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// StopEnum
    /// </summary>
    public enum CmfrecStopEnum
    {
        /// <summary>
        /// 'No' <= No
        /// </summary>
        [Description("No")]
        No = 0,

        /// <summary>
        /// 'Warning' <= Warning
        /// </summary>
        [Description("Warning")]
        Warning = 1,

        /// <summary>
        /// 'TotalStop' <= Total Stop
        /// </summary>
        [Description("Total Stop")]
        TotalStop = 2,

    }
}
