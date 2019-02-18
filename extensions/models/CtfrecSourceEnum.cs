using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// SourceEnum
    /// </summary>
    public enum CtfrecSourceEnum
    {
        /// <summary>
        /// 'General' <= General
        /// </summary>
        [Description("General")]
        General = 0,

        /// <summary>
        /// 'Pos' <= POS
        /// </summary>
        [Description("POS")]
        Pos = 1,

        /// <summary>
        /// 'Wsj' <= WSJ
        /// </summary>
        [Description("WSJ")]
        Wsj = 2,

        /// <summary>
        /// 'Wit' <= WIT
        /// </summary>
        [Description("WIT")]
        Wit = 3,

        /// <summary>
        /// 'Vehic' <= {VEHIC}
        /// </summary>
        [Description("{VEHIC}")]
        Vehic = 4,

        /// <summary>
        /// 'Plant' <= Plant
        /// </summary>
        [Description("Plant")]
        Plant = 5,

        /// <summary>
        /// 'PaymentRequest' <= Payment Request
        /// </summary>
        [Description("Payment Request")]
        PaymentRequest = 6,

    }
}
