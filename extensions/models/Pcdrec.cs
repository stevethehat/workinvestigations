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
    [Table("pcdrec", Schema ="companyccc")]
    [Isam("DV1:PCDccc.ISM", Compressed=true, StaticRFA=true, Length=200)]
    [IsamKey("PCD_PARTNO_CUST", Start="1:23", Length="22:6", IsAscending=true, Index=0)]
    [IsamKey("PCD_CUST_PARTNO", Start="23:1", Length="6:22", IsAscending=true, Index=1)]
    public partial class Pcdrec : GoldModel
    {
        /// <summary>
        /// Part Number
        /// </summary>
        [Key]
        [IsamField(1, 22)]
        [Column("pcd_partno",Order = 1)]
        public String Partno { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [IsamField(23, 6)]
        [Column("pcd_cusacc",Order = 3)]
        public String Cusacc { get; set; }

        /// <summary>
        /// Price Base
        /// </summary>
        [IsamField(29, 1)]
        [Column("pcd_base",Order = 5)]
        [Enum(typeof(PcdrecBaseEnum), Start = 0, Step = 1)]
        public string Base { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(30, 9)]
        [Column("pcd_q_qty1",Order = 6)]
        [Precision(3)]
        public Decimal? QQty1 { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(39, 9)]
        [Column("pcd_q_qty2",Order = 7)]
        [Precision(3)]
        public Decimal? QQty2 { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(48, 9)]
        [Column("pcd_q_qty3",Order = 8)]
        [Precision(3)]
        public Decimal? QQty3 { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(57, 9)]
        [Column("pcd_q_qty4",Order = 9)]
        [Precision(3)]
        public Decimal? QQty4 { get; set; }

        /// <summary>
        /// Quantity Break For Relevant Discount
        /// </summary>
        [IsamField(66, 9)]
        [Column("pcd_q_qty5",Order = 10)]
        [Precision(3)]
        public Decimal? QQty5 { get; set; }

        /// <summary>
        /// Specific Prices or Discount %ages
        /// </summary>
        [IsamField(75, 10)]
        [Column("pcd_pric_perc1",Order = 16)]
        [Precision(2)]
        public Decimal? PricPerc1 { get; set; }

        /// <summary>
        /// Specific Prices or Discount %ages
        /// </summary>
        [IsamField(85, 10)]
        [Column("pcd_pric_perc2",Order = 17)]
        [Precision(2)]
        public Decimal? PricPerc2 { get; set; }

        /// <summary>
        /// Specific Prices or Discount %ages
        /// </summary>
        [IsamField(95, 10)]
        [Column("pcd_pric_perc3",Order = 18)]
        [Precision(2)]
        public Decimal? PricPerc3 { get; set; }

        /// <summary>
        /// Specific Prices or Discount %ages
        /// </summary>
        [IsamField(105, 10)]
        [Column("pcd_pric_perc4",Order = 19)]
        [Precision(2)]
        public Decimal? PricPerc4 { get; set; }

        /// <summary>
        /// Specific Prices or Discount %ages
        /// </summary>
        [IsamField(115, 10)]
        [Column("pcd_pric_perc5",Order = 20)]
        [Precision(2)]
        public Decimal? PricPerc5 { get; set; }

        /// <summary>
        /// Part or Prod grp type
        /// </summary>
        [IsamField(125, 1)]
        [Column("pcd_type",Order = 26)]
        [Enum(typeof(PcdrecTypeEnum), Start = 0, Step = 1)]
        public string Type { get; set; }

        /// <summary>
        /// Spare
        /// </summary>
        [NotMapped]
        [IsamField(126, 75)]
        [Column("pcd_spare",Order = 27)]
        public String Spare { get; set; }

    }
}
