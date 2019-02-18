using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// AgeEnum
    /// </summary>
    public enum CtfrecAgeEnum
    {
        /// <summary>
        /// 'Cur' <= Cur
        /// </summary>
        [Description("Cur")]
        Cur = 0,

        /// <summary>
        /// 'Value2' <= *
        /// </summary>
        [Description("*")]
        Value2 = 1,

        /// <summary>
        /// 'Value3' <= **
        /// </summary>
        [Description("**")]
        Value3 = 2,

        /// <summary>
        /// 'Value4' <= ***
        /// </summary>
        [Description("***")]
        Value4 = 3,

    }
}
