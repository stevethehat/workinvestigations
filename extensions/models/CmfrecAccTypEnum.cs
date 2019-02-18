using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// AccTypEnum
    /// </summary>
    public enum CmfrecAccTypEnum
    {
        /// <summary>
        /// 'OpenItem' <= Open item
        /// </summary>
        [Description("Open item")]
        OpenItem = 1,

        /// <summary>
        /// 'BroughtForward' <= Brought forward
        /// </summary>
        [Description("Brought forward")]
        BroughtForward = 2,

        /// <summary>
        /// 'Prospect' <= Prospect
        /// </summary>
        [Description("Prospect")]
        Prospect = 3,

    }
}
