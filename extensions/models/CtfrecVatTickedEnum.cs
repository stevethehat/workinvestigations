using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// VatTickedEnum
    /// </summary>
    public enum CtfrecVatTickedEnum
    {
        /// <summary>
        /// 'NotTicked' <= Not Ticked
        /// </summary>
        [Description("Not Ticked")]
        NotTicked = 0,

        /// <summary>
        /// 'TickedWip' <= Ticked (WIP)
        /// </summary>
        [Description("Ticked (WIP)")]
        TickedWip = 1,

        /// <summary>
        /// 'History' <= History
        /// </summary>
        [Description("History")]
        History = 2,

    }
}
