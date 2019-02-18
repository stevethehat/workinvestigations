using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// PopHedEnum
    /// </summary>
    public enum DeprecPopHedEnum
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
        /// 'Invstationery' <= Inv.Stationery
        /// </summary>
        [Description("Inv.Stationery")]
        Invstationery = 2,

        /// <summary>
        /// 'InvstatnoDepadd' <= Inv.Stat.No Dep.Add
        /// </summary>
        [Description("Inv.Stat.No Dep.Add")]
        InvstatnoDepadd = 3,

    }
}
