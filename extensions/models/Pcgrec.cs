using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Ibcos.GoldAPIServer.DataLayer;

// produced ModelBuilder.exe - 18/02/2019 11:13:00
namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("pcgrec", Schema ="companyccc")]
    [Isam("DV1:PCGccc.ISM", Compressed=true, StaticRFA=true, Length=150)]
    [IsamKey("PCG_PFX_DSC_CUS", Start="1:3:5:11", Length="2:2:6:2", IsAscending=true, Index=0)]
    [IsamKey("PCG_CUS_PFX_DSC", Start="5:1:3:11", Length="6:2:2:2", IsAscending=true, Index=1)]
    public partial class Pcgrec : GoldModel
    {
        /// <summary>
        /// Product Prefix
        /// </summary>
        [Key]
        [IsamField(1, 2)]
        [Column("pcg_prefix",Order = 1)]
        public String Prefix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [IsamField(3, 2)]
        [Column("pcg_disc_lev",Order = 2)]
        public String DiscLev { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [IsamField(5, 6)]
        [Column("pcg_cusacc",Order = 3)]
        public String Cusacc { get; set; }

        /// <summary>
        /// Invoice type
        /// </summary>
        [Key]
        [IsamField(11, 2)]
        [Column("pcg_inv_typ",Order = 4)]
        public String InvTyp { get; set; }

        /// <summary>
        /// Price Base
        /// </summary>
        [IsamField(13, 1)]
        [Column("pcg_base",Order = 6)]
        [Enum(typeof(PcgrecBaseEnum), Start = 0, Step = 1)]
        public string Base { get; set; }

        /// <summary>
        /// Discount/Uplift Percentage 1
        /// </summary>
        [IsamField(14, 5)]
        [Column("pcg_perc1",Order = 7)]
        public Decimal? Perc1 { get; set; }

        /// <summary>
        /// Discount/Uplift Percentage 2
        /// </summary>
        [IsamField(19, 5)]
        [Column("pcg_perc2",Order = 8)]
        public Decimal? Perc2 { get; set; }

        /// <summary>
        /// Discount/Uplift Percentage 3
        /// </summary>
        [IsamField(24, 5)]
        [Column("pcg_perc3",Order = 9)]
        public Decimal? Perc3 { get; set; }

        /// <summary>
        /// Discount/Uplift Percentage 4
        /// </summary>
        [IsamField(29, 5)]
        [Column("pcg_perc4",Order = 10)]
        public Decimal? Perc4 { get; set; }

        /// <summary>
        /// Discount/Uplift Percentage 5
        /// </summary>
        [IsamField(34, 5)]
        [Column("pcg_perc5",Order = 11)]
        public Decimal? Perc5 { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(39, 9)]
        [Column("pcg_q_qty1",Order = 17)]
        [Precision(3)]
        public Decimal? QQty1 { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(48, 9)]
        [Column("pcg_q_qty2",Order = 18)]
        [Precision(3)]
        public Decimal? QQty2 { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(57, 9)]
        [Column("pcg_q_qty3",Order = 19)]
        [Precision(3)]
        public Decimal? QQty3 { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(66, 9)]
        [Column("pcg_q_qty4",Order = 20)]
        [Precision(3)]
        public Decimal? QQty4 { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(75, 9)]
        [Column("pcg_q_qty5",Order = 21)]
        [Precision(3)]
        public Decimal? QQty5 { get; set; }

        /// <summary>
        /// Overwrite order discount
        /// </summary>
        [IsamField(84, 1)]
        [Column("pcg_overwrite_disc",Order = 27)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string OverwriteDisc { get; set; }

        /// <summary>
        /// Order discount %
        /// </summary>
        [IsamField(85, 4)]
        [Column("pcg_order_disc",Order = 28)]
        public Decimal? OrderDisc { get; set; }

        /// <summary>
        /// Spare
        /// </summary>
        [NotMapped]
        [IsamField(89, 62)]
        [Column("pcg_spare",Order = 29)]
        public String Spare { get; set; }

    }
}
