using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// PostTypeEnum
    /// </summary>
    public enum CtfrecPostTypeEnum
    {
        /// <summary>
        /// 'Inv' <= Inv
        /// </summary>
        [Description("Inv")]
        Inv = 1,

        /// <summary>
        /// 'Cnt' <= C/nt
        /// </summary>
        [Description("C/nt")]
        Cnt = 2,

        /// <summary>
        /// 'Jrnl' <= Jrnl
        /// </summary>
        [Description("Jrnl")]
        Jrnl = 3,

        /// <summary>
        /// 'Disc' <= Disc
        /// </summary>
        [Description("Disc")]
        Disc = 4,

        /// <summary>
        /// 'Bfd' <= B/Fd
        /// </summary>
        [Description("B/Fd")]
        Bfd = 5,

        /// <summary>
        /// 'Cash' <= Cash
        /// </summary>
        [Description("Cash")]
        Cash = 6,

        /// <summary>
        /// 'Chq' <= Chq
        /// </summary>
        [Description("Chq")]
        Chq = 7,

        /// <summary>
        /// 'Ccd' <= C/Cd
        /// </summary>
        [Description("C/Cd")]
        Ccd = 8,

        /// <summary>
        /// 'Bacs' <= Bacs
        /// </summary>
        [Description("Bacs")]
        Bacs = 9,

        /// <summary>
        /// 'Opy' <= O/Py
        /// </summary>
        [Description("O/Py")]
        Opy = 10,

    }
}
