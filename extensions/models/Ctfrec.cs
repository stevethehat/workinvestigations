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
    [Table("ctfrec", Schema ="companyccc")]
    [Isam("DV1:CTFccc.ISM", Compressed=true, StaticRFA=true, Length=1024)]
    [IsamKey("CTF_CUST_STATAC", Start="1:7:13:21", Length="6:6:8:6", Duplicates=true, IsAscending=true, Index=0)]
    [IsamKey("CTF_REF_DATE", Start="21:13", Length="6:8", Duplicates=true, IsAscending=true, Index=1)]
    [IsamKey("CTF_CUST_ACCUM", Start="1:189:21:13", Length="6:2:6:8", Duplicates=true, IsAscending=true, Index=2)]
    [IsamKey("CTF_CBPAY_DATE", Start="191:13:21", Length="8:8:6", Duplicates=true, IsAscending=true, Modifiable=true, Index=3)]
    [IsamKey("CTF_DATE_REF", Start="13:21", Length="8:6", Duplicates=true, IsAscending=true, Index=4)]
    [IsamKey("CTF_CUST_DATE_REF", Start="1:13:21", Length="6:8:6", Duplicates=true, IsAscending=true, Index=5)]
    public partial class Ctfrec : GoldModel
    {
        /// <summary>
        /// Customer Account
        /// </summary>
        [Key]
        [IsamField(1, 6)]
        [Column("ctf_cust_acc",Order = 1)]
        public String CustAcc { get; set; }

        /// <summary>
        /// Statement account number
        /// </summary>
        [Key]
        [IsamField(7, 6)]
        [Column("ctf_orig_acc",Order = 2)]
        public String OrigAcc { get; set; }

        /// <summary>
        /// Transaction date
        /// </summary>
        [Key]
        [IsamField(13, 8)]
        [Column("ctf_tran_dat",Order = 3)]
        public DateTime? TranDat { get; set; }

        /// <summary>
        /// Document reference
        /// </summary>
        [Key]
        [IsamField(21, 6)]
        [Column("ctf_doc_ref",Order = 4)]
        public String DocRef { get; set; }

        /// <summary>
        /// Posting Type
        /// </summary>
        [IsamField(27, 2)]
        [Column("ctf_post_type",Order = 6)]
        [Enum(typeof(CtfrecPostTypeEnum), Start = 1, Step = 1)]
        public string PostType { get; set; }

        /// <summary>
        /// Ageing Flag
        /// </summary>
        [IsamField(29, 1)]
        [Column("ctf_age",Order = 7)]
        [Enum(typeof(CtfrecAgeEnum), Start = 0, Step = 1)]
        public string Age { get; set; }

        /// <summary>
        /// Transaction value
        /// </summary>
        [IsamField(30, 10)]
        [Column("ctf_tran_val_1",Order = 8)]
        [Precision(2)]
        public Decimal? TranVal_1 { get; set; }

        /// <summary>
        /// Transaction value
        /// </summary>
        [IsamField(40, 10)]
        [Column("ctf_tran_val_2",Order = 9)]
        [Precision(2)]
        public Decimal? TranVal_2 { get; set; }

        /// <summary>
        /// Balance Outstanding
        /// </summary>
        [IsamField(50, 10)]
        [Column("ctf_bal_out_1",Order = 11)]
        [Precision(2)]
        public Decimal? BalOut_1 { get; set; }

        /// <summary>
        /// Balance Outstanding
        /// </summary>
        [IsamField(60, 10)]
        [Column("ctf_bal_out_2",Order = 12)]
        [Precision(2)]
        public Decimal? BalOut_2 { get; set; }

        /// <summary>
        /// Vat value
        /// </summary>
        [IsamField(70, 10)]
        [Column("ctf_vat_val_1",Order = 14)]
        [Precision(2)]
        public Decimal? VatVal_1 { get; set; }

        /// <summary>
        /// Vat value
        /// </summary>
        [IsamField(80, 10)]
        [Column("ctf_vat_val_2",Order = 15)]
        [Precision(2)]
        public Decimal? VatVal_2 { get; set; }

        /// <summary>
        /// Settlement value
        /// </summary>
        [IsamField(90, 10)]
        [Column("ctf_sett_val_1",Order = 17)]
        [Precision(2)]
        public Decimal? SettVal_1 { get; set; }

        /// <summary>
        /// Settlement value
        /// </summary>
        [IsamField(100, 10)]
        [Column("ctf_sett_val_2",Order = 18)]
        [Precision(2)]
        public Decimal? SettVal_2 { get; set; }

        /// <summary>
        /// Cost value
        /// </summary>
        [IsamField(110, 10)]
        [Column("ctf_costs",Order = 20)]
        [Precision(2)]
        public Decimal? Costs { get; set; }

        /// <summary>
        /// Customer Ref. No.
        /// </summary>
        [IsamField(120, 10)]
        [Column("ctf_ref_no",Order = 21)]
        public String RefNo { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [IsamField(130, 20)]
        [Column("ctf_desc",Order = 22)]
        public String Desc { get; set; }

        /// <summary>
        /// Settlement Date
        /// </summary>
        [IsamField(150, 8)]
        [Column("ctf_set_dat",Order = 23)]
        public DateTime? SetDat { get; set; }

        /// <summary>
        /// Batch No.
        /// </summary>
        [IsamField(158, 4)]
        [Column("ctf_batch",Order = 24)]
        public Decimal? Batch { get; set; }

        /// <summary>
        /// Invoice Type
        /// </summary>
        [IsamField(162, 2)]
        [Column("ctf_inv_typ",Order = 25)]
        public String InvTyp { get; set; }

        /// <summary>
        /// Disputed Item Code
        /// </summary>
        [IsamField(164, 1)]
        [Column("ctf_dispute",Order = 26)]
        public String Dispute { get; set; }

        /// <summary>
        /// Used Car Flag
        /// </summary>
        [IsamField(165, 1)]
        [Column("ctf_uc_flg",Order = 27)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string UcFlg { get; set; }

        /// <summary>
        /// Clearing cheque number
        /// </summary>
        [IsamField(166, 6)]
        [Column("ctf_chq_num",Order = 28)]
        public String ChqNum { get; set; }

        /// <summary>
        /// Source of Transaction
        /// </summary>
        [IsamField(172, 1)]
        [Column("ctf_source",Order = 29)]
        [Enum(typeof(CtfrecSourceEnum), Start = 0, Step = 1)]
        public string Source { get; set; }

        /// <summary>
        /// Exported Transaction
        /// </summary>
        [IsamField(173, 1)]
        [Column("ctf_exported",Order = 30)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string Exported { get; set; }

        /// <summary>
        /// System Date at Time of Transaction
        /// </summary>
        [IsamField(174, 8)]
        [Column("ctf_sys_dat",Order = 31)]
        public DateTime? SysDat { get; set; }

        /// <summary>
        /// VAT Code
        /// </summary>
        [IsamField(182, 2)]
        [Column("ctf_vat_code",Order = 32)]
        public Decimal? VatCode { get; set; }

        /// <summary>
        /// Sales ledger reconciled
        /// </summary>
        [IsamField(184, 1)]
        [Column("ctf_ticked",Order = 33)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string Ticked { get; set; }

        /// <summary>
        /// Currency Code
        /// </summary>
        [IsamField(185, 3)]
        [Column("ctf_cur_cod",Order = 34)]
        public String CurCod { get; set; }

        /// <summary>
        /// Foreign Currency Transactions
        /// </summary>
        [IsamField(188, 1)]
        [Column("ctf_cur_tran",Order = 35)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string CurTran { get; set; }

        /// <summary>
        /// Transaction Accumulator - from ivt_accum
        /// </summary>
        [IsamField(189, 2)]
        [Column("ctf_accum_no",Order = 36)]
        public Decimal? AccumNo { get; set; }

        /// <summary>
        /// Cashbook Pay in Slip No
        /// </summary>
        [IsamField(191, 8)]
        [Column("ctf_cb_pay_no",Order = 37)]
        public String CbPayNo { get; set; }

        /// <summary>
        /// VAT Code
        /// </summary>
        [IsamField(199, 2)]
        [Column("ctf_vat_array__ctf_mvf_code_1",Order = 38)]
        public Decimal? VatArray__MvfCode_1 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(201, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_1_1",Order = 39)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_1_1 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(211, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_1_2",Order = 40)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_1_2 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(221, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_1_1",Order = 41)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_1_1 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(231, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_1_2",Order = 42)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_1_2 { get; set; }

        /// <summary>
        /// VAT Code
        /// </summary>
        [IsamField(241, 2)]
        [Column("ctf_vat_array__ctf_mvf_code_2",Order = 43)]
        public Decimal? VatArray__MvfCode_2 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(243, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_2_1",Order = 44)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_2_1 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(253, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_2_2",Order = 45)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_2_2 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(263, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_2_1",Order = 46)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_2_1 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(273, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_2_2",Order = 47)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_2_2 { get; set; }

        /// <summary>
        /// VAT Code
        /// </summary>
        [IsamField(283, 2)]
        [Column("ctf_vat_array__ctf_mvf_code_3",Order = 48)]
        public Decimal? VatArray__MvfCode_3 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(285, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_3_1",Order = 49)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_3_1 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(295, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_3_2",Order = 50)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_3_2 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(305, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_3_1",Order = 51)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_3_1 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(315, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_3_2",Order = 52)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_3_2 { get; set; }

        /// <summary>
        /// VAT Code
        /// </summary>
        [IsamField(325, 2)]
        [Column("ctf_vat_array__ctf_mvf_code_4",Order = 53)]
        public Decimal? VatArray__MvfCode_4 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(327, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_4_1",Order = 54)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_4_1 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(337, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_4_2",Order = 55)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_4_2 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(347, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_4_1",Order = 56)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_4_1 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(357, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_4_2",Order = 57)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_4_2 { get; set; }

        /// <summary>
        /// VAT Code
        /// </summary>
        [IsamField(367, 2)]
        [Column("ctf_vat_array__ctf_mvf_code_5",Order = 58)]
        public Decimal? VatArray__MvfCode_5 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(369, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_5_1",Order = 59)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_5_1 { get; set; }

        /// <summary>
        /// amount of vat (negative) - 0=FC,1=BC
        /// </summary>
        [IsamField(379, 10)]
        [Column("ctf_vat_array__ctf_mvf_vat_5_2",Order = 60)]
        [Precision(2)]
        public Decimal? VatArray__MvfVat_5_2 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(389, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_5_1",Order = 61)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_5_1 { get; set; }

        /// <summary>
        /// amount of goods + vat - 0=FC,1=BC
        /// </summary>
        [IsamField(399, 10)]
        [Column("ctf_vat_array__ctf_mvf_goods_5_2",Order = 62)]
        [Precision(2)]
        public Decimal? VatArray__MvfGoods_5_2 { get; set; }

        /// <summary>
        /// POS Payment Type
        /// </summary>
        [IsamField(409, 2)]
        [Column("ctf_pay_type",Order = 63)]
        [Enum(typeof(CtfrecPayTypeEnum), Start = 0, Step = 1)]
        public string PayType { get; set; }

        /// <summary>
        /// Period  YYYYPP
        /// </summary>
        [IsamField(411, 6)]
        [Column("ctf_period_no",Order = 64)]
        public DateTime? PeriodNo { get; set; }

        /// <summary>
        /// Base currency template
        /// </summary>
        [IsamField(417, 10)]
        [Column("ctf_released_val_1",Order = 65)]
        [Precision(2)]
        public Decimal? ReleasedVal_1 { get; set; }

        /// <summary>
        /// Base currency template
        /// </summary>
        [IsamField(427, 10)]
        [Column("ctf_released_val_2",Order = 66)]
        [Precision(2)]
        public Decimal? ReleasedVal_2 { get; set; }

        /// <summary>
        /// Base currency template
        /// </summary>
        [IsamField(437, 10)]
        [Column("ctf_released_vat_1",Order = 68)]
        [Precision(2)]
        public Decimal? ReleasedVat_1 { get; set; }

        /// <summary>
        /// Base currency template
        /// </summary>
        [IsamField(447, 10)]
        [Column("ctf_released_vat_2",Order = 69)]
        [Precision(2)]
        public Decimal? ReleasedVat_2 { get; set; }

        /// <summary>
        /// Base currency template
        /// </summary>
        [IsamField(457, 10)]
        [Column("ctf_retained_val_1",Order = 71)]
        [Precision(2)]
        public Decimal? RetainedVal_1 { get; set; }

        /// <summary>
        /// Base currency template
        /// </summary>
        [IsamField(467, 10)]
        [Column("ctf_retained_val_2",Order = 72)]
        [Precision(2)]
        public Decimal? RetainedVal_2 { get; set; }

        /// <summary>
        /// Base currency template
        /// </summary>
        [IsamField(477, 10)]
        [Column("ctf_disputed_val_1",Order = 74)]
        [Precision(2)]
        public Decimal? DisputedVal_1 { get; set; }

        /// <summary>
        /// Base currency template
        /// </summary>
        [IsamField(487, 10)]
        [Column("ctf_disputed_val_2",Order = 75)]
        [Precision(2)]
        public Decimal? DisputedVal_2 { get; set; }

        /// <summary>
        /// On VAT Return 0=no,1=ticked(wip),2=hist
        /// </summary>
        [IsamField(497, 1)]
        [Column("ctf_vat_ticked",Order = 77)]
        [Enum(typeof(CtfrecVatTickedEnum), Start = 0, Step = 1)]
        public string VatTicked { get; set; }

        /// <summary>
        /// PPD Sales Net Amount
        /// </summary>
        [IsamField(498, 10)]
        [Column("ctf_ppdsnamt_1",Order = 78)]
        [Precision(2)]
        public Decimal? Ppdsnamt_1 { get; set; }

        /// <summary>
        /// PPD Sales Net Amount
        /// </summary>
        [IsamField(508, 10)]
        [Column("ctf_ppdsnamt_2",Order = 79)]
        [Precision(2)]
        public Decimal? Ppdsnamt_2 { get; set; }

        /// <summary>
        /// PPD Sales Net VAT
        /// </summary>
        [IsamField(518, 10)]
        [Column("ctf_ppdsnvat_1",Order = 80)]
        [Precision(2)]
        public Decimal? Ppdsnvat_1 { get; set; }

        /// <summary>
        /// PPD Sales Net VAT
        /// </summary>
        [IsamField(528, 10)]
        [Column("ctf_ppdsnvat_2",Order = 81)]
        [Precision(2)]
        public Decimal? Ppdsnvat_2 { get; set; }

        /// <summary>
        /// PPD Sales Full VAT
        /// </summary>
        [IsamField(538, 10)]
        [Column("ctf_ppdsfvat_1",Order = 82)]
        [Precision(2)]
        public Decimal? Ppdsfvat_1 { get; set; }

        /// <summary>
        /// PPD Sales Full VAT
        /// </summary>
        [IsamField(548, 10)]
        [Column("ctf_ppdsfvat_2",Order = 83)]
        [Precision(2)]
        public Decimal? Ppdsfvat_2 { get; set; }

        /// <summary>
        /// PPD Sales Net Amount for Multi Vat Code
        /// </summary>
        [NotMapped]
        [IsamField(558, 100)]
        [Column("ctf_mvf_ppdsnamt",Order = 84)]
        [Precision(2)]
        public Decimal? MvfPpdsnamt { get; set; }

        /// <summary>
        /// PPD Sales Full VAT for Multi Vat Code
        /// </summary>
        [NotMapped]
        [IsamField(658, 100)]
        [Column("ctf_mvf_ppdsfvat",Order = 85)]
        [Precision(2)]
        public Decimal? MvfPpdsfvat { get; set; }

        /// <summary>
        /// PPD Discount Taken Flag 0-No, 1-Yes
        /// </summary>
        [IsamField(758, 1)]
        [Column("ctf_ppdtaken",Order = 86)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string Ppdtaken { get; set; }

        /// <summary>
        /// PPD VAT Adjustment Transaction
        /// </summary>
        [IsamField(759, 1)]
        [Column("ctf_ppdvatadj",Order = 87)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string Ppdvatadj { get; set; }

        /// <summary>
        /// Delivery Acc (if inv & statement used)
        /// </summary>
        [IsamField(760, 6)]
        [Column("ctf_del_acc",Order = 88)]
        public String DelAcc { get; set; }

        /// <summary>
        /// Spare
        /// </summary>
        [NotMapped]
        [IsamField(766, 259)]
        [Column("ctf_spare",Order = 89)]
        public String Spare { get; set; }

    }
}
