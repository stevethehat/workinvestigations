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
    [Table("deprec", Schema ="companyccc")]
    [Isam("DV1:DEPccc.ISM", Compressed=true, StaticRFA=true, Length=1536)]
    [IsamKey("DEP_CODE", Start="1", Length="1", IsAscending=true, Index=0)]
    public partial class Deprec : GoldModel
    {
        /// <summary>
        /// Depot code
        /// </summary>
        [Key]
        [IsamField(1, 1)]
        [Column("dep_code",Order = 1)]
        public String Code { get; set; }

        /// <summary>
        /// Depot Short Name
        /// </summary>
        [IsamField(2, 10)]
        [Column("dep_short_nam",Order = 2)]
        public String ShortNam { get; set; }

        /// <summary>
        /// Depot Name
        /// </summary>
        [IsamField(12, 28)]
        [Column("dep_name",Order = 3)]
        public String Name { get; set; }

        /// <summary>
        /// Depot Address
        /// </summary>
        [IsamField(40, 32)]
        [Column("dep_address_1",Order = 4)]
        public String Address_1 { get; set; }

        /// <summary>
        /// Depot Address
        /// </summary>
        [IsamField(72, 32)]
        [Column("dep_address_2",Order = 5)]
        public String Address_2 { get; set; }

        /// <summary>
        /// Depot Address
        /// </summary>
        [IsamField(104, 32)]
        [Column("dep_address_3",Order = 6)]
        public String Address_3 { get; set; }

        /// <summary>
        /// Depot Address
        /// </summary>
        [IsamField(136, 32)]
        [Column("dep_address_4",Order = 7)]
        public String Address_4 { get; set; }

        /// <summary>
        /// Postcode
        /// </summary>
        [IsamField(168, 9)]
        [Column("dep_postcode",Order = 8)]
        public String Postcode { get; set; }

        /// <summary>
        /// Print Depot name top WSJ inst print
        /// </summary>
        [IsamField(177, 1)]
        [Column("dep_ins_hed",Order = 9)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string InsHed { get; set; }

        /// <summary>
        /// print depot address on statement
        /// </summary>
        [IsamField(178, 1)]
        [Column("dep_sta_hed",Order = 10)]
        [Enum(typeof(DeprecStaHedEnum), Start = 0, Step = 1)]
        public string StaHed { get; set; }

        /// <summary>
        /// Depot Telephone Number
        /// </summary>
        [IsamField(179, 17)]
        [Column("dep_tel",Order = 11)]
        public String Tel { get; set; }

        /// <summary>
        /// Print depot address on blank invoice
        /// </summary>
        [IsamField(196, 1)]
        [Column("dep_inv_hed",Order = 12)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string InvHed { get; set; }

        /// <summary>
        /// Depot VAT number
        /// </summary>
        [IsamField(197, 15)]
        [Column("dep_vat_no",Order = 13)]
        public String VatNo { get; set; }

        /// <summary>
        /// I.D.T sales base 0-Retail, 1-Cost, 2-Ave
        /// </summary>
        [IsamField(212, 1)]
        [Column("dep_idt_base",Order = 14)]
        public Decimal? IdtBase { get; set; }

        /// <summary>
        /// Uplift % (+ or -)
        /// </summary>
        [IsamField(213, 4)]
        [Column("dep_idt_lift",Order = 15)]
        public Decimal? IdtLift { get; set; }

        /// <summary>
        /// Default IDT printer number
        /// </summary>
        [IsamField(217, 3)]
        [Column("dep_idt_prn",Order = 16)]
        public Decimal? IdtPrn { get; set; }

        /// <summary>
        /// Next IDT order number 1 - 9999999
        /// </summary>
        [IsamField(220, 7)]
        [Column("dep_idt_num",Order = 17)]
        public Decimal? IdtNum { get; set; }

        /// <summary>
        /// Allow IDT cost to be modified
        /// </summary>
        [IsamField(227, 1)]
        [Column("dep_idt_pri",Order = 18)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string IdtPri { get; set; }

        /// <summary>
        /// Depot Fax Number
        /// </summary>
        [IsamField(228, 17)]
        [Column("dep_fax_num",Order = 19)]
        public String FaxNum { get; set; }

        /// <summary>
        /// Autogen Contract No.
        /// </summary>
        [IsamField(245, 1)]
        [Column("dep_gen_contract",Order = 20)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string GenContract { get; set; }

        /// <summary>
        /// Autogen Booking No.
        /// </summary>
        [IsamField(246, 1)]
        [Column("dep_gen_booking",Order = 21)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string GenBooking { get; set; }

        /// <summary>
        /// Autogen Transfer No.
        /// </summary>
        [IsamField(247, 1)]
        [Column("dep_gen_transfer",Order = 22)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string GenTransfer { get; set; }

        /// <summary>
        /// Allow Multi-hire
        /// </summary>
        [IsamField(248, 1)]
        [Column("dep_multi_hire",Order = 23)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string MultiHire { get; set; }

        /// <summary>
        /// Default Plant Invoice Type
        /// </summary>
        [IsamField(249, 2)]
        [Column("dep_plant_inv_type",Order = 24)]
        public String PlantInvType { get; set; }

        /// <summary>
        /// {WG} email address
        /// </summary>
        [IsamField(251, 60)]
        [Column("dep_email_wg",Order = 25)]
        public String EmailWg { get; set; }

        /// <summary>
        /// Depot Name
        /// </summary>
        [IsamField(311, 28)]
        [Column("dep_name_2",Order = 26)]
        public String Name2 { get; set; }

        /// <summary>
        /// BAGMA Account Number
        /// </summary>
        [IsamField(339, 6)]
        [Column("dep_bagma_acc",Order = 27)]
        public String BagmaAcc { get; set; }

        /// <summary>
        /// Print Depot name top Parts Order
        /// </summary>
        [IsamField(345, 1)]
        [Column("dep_pop_hed",Order = 28)]
        [Enum(typeof(DeprecPopHedEnum), Start = 0, Step = 1)]
        public string PopHed { get; set; }

        /// <summary>
        /// Tradanet ANA Code
        /// </summary>
        [IsamField(346, 13)]
        [Column("dep_ana_code",Order = 29)]
        public Decimal? AnaCode { get; set; }

        /// <summary>
        /// Tradanet password
        /// </summary>
        [IsamField(359, 8)]
        [Column("dep_ana_pwd",Order = 30)]
        public String AnaPwd { get; set; }

        /// <summary>
        /// email address
        /// </summary>
        [IsamField(367, 60)]
        [Column("dep_email",Order = 31)]
        public String Email { get; set; }

        /// <summary>
        /// Min Workshop Job Number
        /// </summary>
        [IsamField(427, 5)]
        [Column("minseq__dep_min_wsj",Order = 32)]
        public Decimal? Minseq__MinWsj { get; set; }

        /// <summary>
        /// Min Goods Receipt Number
        /// </summary>
        [IsamField(432, 5)]
        [Column("minseq__dep_min_grn",Order = 33)]
        public Decimal? Minseq__MinGrn { get; set; }

        /// <summary>
        /// Min Invoice Number
        /// </summary>
        [IsamField(437, 5)]
        [Column("minseq__dep_min_inv",Order = 34)]
        public Decimal? Minseq__MinInv { get; set; }

        /// <summary>
        /// Min Stock Transfer Number
        /// </summary>
        [IsamField(442, 5)]
        [Column("minseq__dep_min_s_tfr",Order = 35)]
        public Decimal? Minseq__MinSTfr { get; set; }

        /// <summary>
        /// Min Stock Adjustment Number
        /// </summary>
        [IsamField(447, 5)]
        [Column("minseq__dep_min_s_adj",Order = 36)]
        public Decimal? Minseq__MinSAdj { get; set; }

        /// <summary>
        /// Min Warranty Claim Number
        /// </summary>
        [IsamField(452, 5)]
        [Column("minseq__dep_min_warr",Order = 37)]
        public Decimal? Minseq__MinWarr { get; set; }

        /// <summary>
        /// Min Quote Number
        /// </summary>
        [IsamField(457, 5)]
        [Column("minseq__dep_min_quote",Order = 38)]
        public Decimal? Minseq__MinQuote { get; set; }

        /// <summary>
        /// Min Advice Note Number
        /// </summary>
        [IsamField(462, 5)]
        [Column("minseq__dep_min_advice",Order = 39)]
        public Decimal? Minseq__MinAdvice { get; set; }

        /// <summary>
        /// Min Vehicle Receipt/depot Transfer Numbe
        /// </summary>
        [IsamField(467, 5)]
        [Column("minseq__dep_min_v_tfr",Order = 40)]
        public Decimal? Minseq__MinVTfr { get; set; }

        /// <summary>
        /// Min Vehicle Purchase Order Number
        /// </summary>
        [IsamField(472, 5)]
        [Column("minseq__dep_min_s_pur",Order = 41)]
        public Decimal? Minseq__MinSPur { get; set; }

        /// <summary>
        /// Min Plant Contract No
        /// </summary>
        [IsamField(477, 5)]
        [Column("minseq__dep_min_contract_no",Order = 42)]
        public Decimal? Minseq__MinContractNo { get; set; }

        /// <summary>
        /// Min Plant Booking No
        /// </summary>
        [IsamField(482, 5)]
        [Column("minseq__dep_min_booking_no",Order = 43)]
        public Decimal? Minseq__MinBookingNo { get; set; }

        /// <summary>
        /// Min Plant Transfer No
        /// </summary>
        [IsamField(487, 5)]
        [Column("minseq__dep_min_transfer_no",Order = 44)]
        public Decimal? Minseq__MinTransferNo { get; set; }

        /// <summary>
        /// Min Self_Bill No
        /// </summary>
        [IsamField(492, 5)]
        [Column("minseq__dep_min_self_bill",Order = 45)]
        public Decimal? Minseq__MinSelfBill { get; set; }

        /// <summary>
        /// Min Hort. receipt batch
        /// </summary>
        [IsamField(497, 5)]
        [Column("minseq__dep_min_hrt",Order = 46)]
        public Decimal? Minseq__MinHrt { get; set; }

        /// <summary>
        /// Min Picking List Number
        /// </summary>
        [IsamField(502, 5)]
        [Column("minseq__dep_min_pck_no",Order = 47)]
        public Decimal? Minseq__MinPckNo { get; set; }

        /// <summary>
        /// Min Misc Purchase Order No
        /// </summary>
        [IsamField(507, 5)]
        [Column("minseq__dep_min_mpr_ord",Order = 48)]
        public Decimal? Minseq__MinMprOrd { get; set; }

        /// <summary>
        /// Min Tradanet File or Plant Quotation No
        /// </summary>
        [IsamField(512, 5)]
        [Column("minseq__dep_min_tnet",Order = 49)]
        public Decimal? Minseq__MinTnet { get; set; }

        /// <summary>
        /// Min Consignment No or Plant Off-Hire No
        /// </summary>
        [IsamField(517, 5)]
        [Column("minseq__dep_min_consign",Order = 50)]
        public Decimal? Minseq__MinConsign { get; set; }

        /// <summary>
        /// Min Warranty Registration
        /// </summary>
        [IsamField(522, 5)]
        [Column("minseq__dep_min_warr_reg",Order = 51)]
        public Decimal? Minseq__MinWarrReg { get; set; }

        /// <summary>
        /// Max Workshop Job Number
        /// </summary>
        [IsamField(527, 5)]
        [Column("maxseq__dep_max_wsj",Order = 52)]
        public Decimal? Maxseq__MaxWsj { get; set; }

        /// <summary>
        /// Max Goods Receipt Number
        /// </summary>
        [IsamField(532, 5)]
        [Column("maxseq__dep_max_grn",Order = 53)]
        public Decimal? Maxseq__MaxGrn { get; set; }

        /// <summary>
        /// Max Invoice Number
        /// </summary>
        [IsamField(537, 5)]
        [Column("maxseq__dep_max_inv",Order = 54)]
        public Decimal? Maxseq__MaxInv { get; set; }

        /// <summary>
        /// Max Stock Transfer Number
        /// </summary>
        [IsamField(542, 5)]
        [Column("maxseq__dep_max_s_tfr",Order = 55)]
        public Decimal? Maxseq__MaxSTfr { get; set; }

        /// <summary>
        /// Max Stock Adjustment Number
        /// </summary>
        [IsamField(547, 5)]
        [Column("maxseq__dep_max_s_adj",Order = 56)]
        public Decimal? Maxseq__MaxSAdj { get; set; }

        /// <summary>
        /// Max Warranty Claim Number
        /// </summary>
        [IsamField(552, 5)]
        [Column("maxseq__dep_max_warr",Order = 57)]
        public Decimal? Maxseq__MaxWarr { get; set; }

        /// <summary>
        /// Max Quote Number
        /// </summary>
        [IsamField(557, 5)]
        [Column("maxseq__dep_max_quote",Order = 58)]
        public Decimal? Maxseq__MaxQuote { get; set; }

        /// <summary>
        /// Max Advice Note Number
        /// </summary>
        [IsamField(562, 5)]
        [Column("maxseq__dep_max_advice",Order = 59)]
        public Decimal? Maxseq__MaxAdvice { get; set; }

        /// <summary>
        /// Max Vehicle Receipt/depot Transfer Numbe
        /// </summary>
        [IsamField(567, 5)]
        [Column("maxseq__dep_max_v_tfr",Order = 60)]
        public Decimal? Maxseq__MaxVTfr { get; set; }

        /// <summary>
        /// Max Vehicle Purchase Order Number
        /// </summary>
        [IsamField(572, 5)]
        [Column("maxseq__dep_max_s_pur",Order = 61)]
        public Decimal? Maxseq__MaxSPur { get; set; }

        /// <summary>
        /// Max Plant Contract No
        /// </summary>
        [IsamField(577, 5)]
        [Column("maxseq__dep_max_contract_no",Order = 62)]
        public Decimal? Maxseq__MaxContractNo { get; set; }

        /// <summary>
        /// Max Plant Booking No
        /// </summary>
        [IsamField(582, 5)]
        [Column("maxseq__dep_max_booking_no",Order = 63)]
        public Decimal? Maxseq__MaxBookingNo { get; set; }

        /// <summary>
        /// Max Plant Transfer No
        /// </summary>
        [IsamField(587, 5)]
        [Column("maxseq__dep_max_transfer_no",Order = 64)]
        public Decimal? Maxseq__MaxTransferNo { get; set; }

        /// <summary>
        /// Max Self-bill No
        /// </summary>
        [IsamField(592, 5)]
        [Column("maxseq__dep_max_self_bill",Order = 65)]
        public Decimal? Maxseq__MaxSelfBill { get; set; }

        /// <summary>
        /// Max Hort. receipt batch
        /// </summary>
        [IsamField(597, 5)]
        [Column("maxseq__dep_max_hrt",Order = 66)]
        public Decimal? Maxseq__MaxHrt { get; set; }

        /// <summary>
        /// Max Picking List Number
        /// </summary>
        [IsamField(602, 5)]
        [Column("maxseq__dep_max_pck_no",Order = 67)]
        public Decimal? Maxseq__MaxPckNo { get; set; }

        /// <summary>
        /// Max Misc Purchase Order No
        /// </summary>
        [IsamField(607, 5)]
        [Column("maxseq__dep_max_mpr_ord",Order = 68)]
        public Decimal? Maxseq__MaxMprOrd { get; set; }

        /// <summary>
        /// Max Tradanet File or Plant Quotation
        /// </summary>
        [IsamField(612, 5)]
        [Column("maxseq__dep_max_tnet",Order = 69)]
        public Decimal? Maxseq__MaxTnet { get; set; }

        /// <summary>
        /// Max Consignment No or Plant Off-Hire No
        /// </summary>
        [IsamField(617, 5)]
        [Column("maxseq__dep_max_consign",Order = 70)]
        public Decimal? Maxseq__MaxConsign { get; set; }

        /// <summary>
        /// Max Warranty Registration
        /// </summary>
        [IsamField(622, 5)]
        [Column("maxseq__dep_max_warr_reg",Order = 71)]
        public Decimal? Maxseq__MaxWarrReg { get; set; }

        /// <summary>
        /// Next Workshop Job Number
        /// </summary>
        [IsamField(627, 5)]
        [Column("nxtseq__dep_nxt_wsj",Order = 72)]
        public Decimal? Nxtseq__NxtWsj { get; set; }

        /// <summary>
        /// Next Goods Receipt Number
        /// </summary>
        [IsamField(632, 5)]
        [Column("nxtseq__dep_nxt_grn",Order = 73)]
        public Decimal? Nxtseq__NxtGrn { get; set; }

        /// <summary>
        /// Next Invoice Number
        /// </summary>
        [IsamField(637, 5)]
        [Column("nxtseq__dep_nxt_inv",Order = 74)]
        public Decimal? Nxtseq__NxtInv { get; set; }

        /// <summary>
        /// Next Stock Transfer Number
        /// </summary>
        [IsamField(642, 5)]
        [Column("nxtseq__dep_nxt_s_tfr",Order = 75)]
        public Decimal? Nxtseq__NxtSTfr { get; set; }

        /// <summary>
        /// Next Stock Adjustment Number
        /// </summary>
        [IsamField(647, 5)]
        [Column("nxtseq__dep_nxt_s_adj",Order = 76)]
        public Decimal? Nxtseq__NxtSAdj { get; set; }

        /// <summary>
        /// Next Warranty Claim Number
        /// </summary>
        [IsamField(652, 5)]
        [Column("nxtseq__dep_nxt_warr",Order = 77)]
        public Decimal? Nxtseq__NxtWarr { get; set; }

        /// <summary>
        /// Next Quote Number
        /// </summary>
        [IsamField(657, 5)]
        [Column("nxtseq__dep_nxt_quote",Order = 78)]
        public Decimal? Nxtseq__NxtQuote { get; set; }

        /// <summary>
        /// Next Advice Note Number
        /// </summary>
        [IsamField(662, 5)]
        [Column("nxtseq__dep_nxt_advice",Order = 79)]
        public Decimal? Nxtseq__NxtAdvice { get; set; }

        /// <summary>
        /// Next Vehicle Receipt/depot Transfer Numb
        /// </summary>
        [IsamField(667, 5)]
        [Column("nxtseq__dep_nxt_v_tfr",Order = 80)]
        public Decimal? Nxtseq__NxtVTfr { get; set; }

        /// <summary>
        /// Next Vehicle Purchase Order Number
        /// </summary>
        [IsamField(672, 5)]
        [Column("nxtseq__dep_nxt_s_pur",Order = 81)]
        public Decimal? Nxtseq__NxtSPur { get; set; }

        /// <summary>
        /// Next Plant Contract No
        /// </summary>
        [IsamField(677, 5)]
        [Column("nxtseq__dep_nxt_contract_no",Order = 82)]
        public Decimal? Nxtseq__NxtContractNo { get; set; }

        /// <summary>
        /// Next Plant Booking No
        /// </summary>
        [IsamField(682, 5)]
        [Column("nxtseq__dep_nxt_booking_no",Order = 83)]
        public Decimal? Nxtseq__NxtBookingNo { get; set; }

        /// <summary>
        /// Next Plant Transfer No
        /// </summary>
        [IsamField(687, 5)]
        [Column("nxtseq__dep_nxt_transfer_no",Order = 84)]
        public Decimal? Nxtseq__NxtTransferNo { get; set; }

        /// <summary>
        /// Next Self-bill No
        /// </summary>
        [IsamField(692, 5)]
        [Column("nxtseq__dep_nxt_self_bill",Order = 85)]
        public Decimal? Nxtseq__NxtSelfBill { get; set; }

        /// <summary>
        /// Next Hort. receipt batch
        /// </summary>
        [IsamField(697, 5)]
        [Column("nxtseq__dep_nxt_hrt",Order = 86)]
        public Decimal? Nxtseq__NxtHrt { get; set; }

        /// <summary>
        /// Next Picking List Number
        /// </summary>
        [IsamField(702, 5)]
        [Column("nxtseq__dep_nxt_pck_no",Order = 87)]
        public Decimal? Nxtseq__NxtPckNo { get; set; }

        /// <summary>
        /// Next Misc Purchase Order No
        /// </summary>
        [IsamField(707, 5)]
        [Column("nxtseq__dep_nxt_mpr_ord",Order = 88)]
        public Decimal? Nxtseq__NxtMprOrd { get; set; }

        /// <summary>
        /// Next Tradanet File or Plant Quotation No
        /// </summary>
        [IsamField(712, 5)]
        [Column("nxtseq__dep_nxt_tnet",Order = 89)]
        public Decimal? Nxtseq__NxtTnet { get; set; }

        /// <summary>
        /// Next Consignment or Plant Off-Hire No
        /// </summary>
        [IsamField(717, 5)]
        [Column("nxtseq__dep_nxt_consign",Order = 90)]
        public Decimal? Nxtseq__NxtConsign { get; set; }

        /// <summary>
        /// Next Warranty Registration
        /// </summary>
        [IsamField(722, 5)]
        [Column("nxtseq__dep_nxt_warr_reg",Order = 91)]
        public Decimal? Nxtseq__NxtWarrReg { get; set; }

        /// <summary>
        /// email address-goldlink/online ordering
        /// </summary>
        [IsamField(727, 60)]
        [Column("dep_email_gl",Order = 92)]
        public String EmailGl { get; set; }

        /// <summary>
        /// SMS Text Identifier
        /// </summary>
        [IsamField(787, 30)]
        [Column("dep_sms_txt_ident",Order = 93)]
        public String SmsTxtIdent { get; set; }

        /// <summary>
        /// Use PDA Picking Confirmation?
        /// </summary>
        [IsamField(817, 1)]
        [Column("dep_pda_pick",Order = 94)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string PdaPick { get; set; }

        /// <summary>
        /// Use PDA Picking after GRN?
        /// </summary>
        [IsamField(818, 1)]
        [Column("dep_grn_pda_pick",Order = 95)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string GrnPdaPick { get; set; }

        /// <summary>
        /// POS Advice XML Label Format
        /// </summary>
        [IsamField(819, 8)]
        [Column("dep_adv_label",Order = 96)]
        public String AdvLabel { get; set; }

        /// <summary>
        /// Default Inv Type for New Custs POS/WSJ
        /// </summary>
        [IsamField(827, 2)]
        [Column("dep_def_newcus_ivt",Order = 97)]
        public String DefNewcusIvt { get; set; }

        /// <summary>
        /// Default POS Invoice Method
        /// </summary>
        [IsamField(829, 1)]
        [Column("dep_def_inv_mthd",Order = 98)]
        [Enum(typeof(DeprecDefInvMthdEnum), Start = 0, Step = 1)]
        public string DefInvMthd { get; set; }

        /// <summary>
        /// Representative Code
        /// </summary>
        [IsamField(830, 4)]
        [Column("dep_def_rep",Order = 99)]
        public String DefRep { get; set; }

        /// <summary>
        /// Area code
        /// </summary>
        [IsamField(834, 2)]
        [Column("dep_def_area",Order = 100)]
        public String DefArea { get; set; }

        /// <summary>
        /// Outlet type
        /// </summary>
        [IsamField(836, 1)]
        [Column("dep_def_out_type",Order = 101)]
        public String DefOutType { get; set; }

        /// <summary>
        /// Default Analysis Codes
        /// </summary>
        [IsamField(837, 10)]
        [Column("dep_def_anal_codes",Order = 102)]
        public String DefAnalCodes { get; set; }

        /// <summary>
        /// Discount Matrix Level
        /// </summary>
        [IsamField(847, 2)]
        [Column("dep_def_disc_level",Order = 103)]
        public String DefDiscLevel { get; set; }

        /// <summary>
        /// Invoice frequency
        /// </summary>
        [IsamField(849, 1)]
        [Column("dep_def_inv_freq",Order = 104)]
        public String DefInvFreq { get; set; }

        /// <summary>
        /// Accumulate invoices?
        /// </summary>
        [IsamField(850, 1)]
        [Column("dep_def_acc_inv",Order = 105)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DefAccInv { get; set; }

        /// <summary>
        /// WSJ Priority Code
        /// </summary>
        [IsamField(851, 3)]
        [Column("dep_def_wsj_pri",Order = 106)]
        public String DefWsjPri { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(854, 10)]
        [Column("dep_def_disc_club",Order = 107)]
        public String DefDiscClub { get; set; }

        /// <summary>
        /// Credit limit
        /// </summary>
        [IsamField(864, 10)]
        [Column("dep_def_cred_lim",Order = 108)]
        public Decimal? DefCredLim { get; set; }

        /// <summary>
        /// Debt Letter to be sent?
        /// </summary>
        [IsamField(874, 1)]
        [Column("dep_def_dbt_let",Order = 109)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DefDbtLet { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(875, 1)]
        [Column("dep_def_debt_rem_level_1",Order = 110)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DefDebtRemLevel_1 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(876, 1)]
        [Column("dep_def_debt_rem_level_2",Order = 111)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DefDebtRemLevel_2 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(877, 1)]
        [Column("dep_def_debt_rem_level_3",Order = 112)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DefDebtRemLevel_3 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(878, 1)]
        [Column("dep_def_debt_rem_level_4",Order = 113)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DefDebtRemLevel_4 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(879, 1)]
        [Column("dep_def_debt_rem_level_5",Order = 114)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DefDebtRemLevel_5 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(880, 1)]
        [Column("dep_def_debt_rem_level_6",Order = 115)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DefDebtRemLevel_6 { get; set; }

        /// <summary>
        /// Default Settlement/credit Percentage
        /// </summary>
        [IsamField(881, 4)]
        [Column("dep_def_sc_perc",Order = 116)]
        public Decimal? DefScPerc { get; set; }

        /// <summary>
        /// Days Credit given
        /// </summary>
        [IsamField(885, 3)]
        [Column("dep_def_days_given",Order = 117)]
        public Decimal? DefDaysGiven { get; set; }

        /// <summary>
        /// sett/cred base
        /// </summary>
        [IsamField(888, 1)]
        [Column("dep_def_sc_from",Order = 118)]
        [Enum(typeof(DeprecDefScFromEnum), Start = 0, Step = 1)]
        public string DefScFrom { get; set; }

        /// <summary>
        /// Sett/Cred Charge Option
        /// </summary>
        [IsamField(889, 1)]
        [Column("dep_def_set_cred",Order = 119)]
        [Enum(typeof(DeprecDefSetCredEnum), Start = 0, Step = 1)]
        public string DefSetCred { get; set; }

        /// <summary>
        /// WSJ default labour discount
        /// </summary>
        [IsamField(890, 4)]
        [Column("dep_def_wsj_lab_disc",Order = 120)]
        public Decimal? DefWsjLabDisc { get; set; }

        /// <summary>
        /// Contact Type Code
        /// </summary>
        [IsamField(894, 3)]
        [Column("dep_def_contact",Order = 121)]
        public String DefContact { get; set; }

        /// <summary>
        /// Send Invoices By;
        /// </summary>
        [IsamField(897, 1)]
        [Column("dep_def_send_inv",Order = 122)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string DefSendInv { get; set; }

        /// <summary>
        /// Send Statements By;
        /// </summary>
        [IsamField(898, 1)]
        [Column("dep_def_send_stat",Order = 123)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string DefSendStat { get; set; }

        /// <summary>
        /// Send Quotes By;
        /// </summary>
        [IsamField(899, 1)]
        [Column("dep_def_send_quote",Order = 124)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string DefSendQuote { get; set; }

        /// <summary>
        /// Send CRM Mailshots By;
        /// </summary>
        [IsamField(900, 1)]
        [Column("dep_def_send_mailshot",Order = 125)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string DefSendMailshot { get; set; }

        /// <summary>
        /// Std Parts SMS message for Cash customers
        /// </summary>
        [IsamField(901, 3)]
        [Column("dep_sms_grn_cash",Order = 126)]
        public Decimal? SmsGrnCash { get; set; }

        /// <summary>
        /// Std Parts SMS for Account Customers
        /// </summary>
        [IsamField(904, 3)]
        [Column("dep_sms_grn_acc",Order = 127)]
        public Decimal? SmsGrnAcc { get; set; }

        /// <summary>
        /// Use PDA Picking for IDT?
        /// </summary>
        [IsamField(907, 1)]
        [Column("dep_idt_pda_pick",Order = 128)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string IdtPdaPick { get; set; }

        /// <summary>
        /// Default Stock Check Printer
        /// </summary>
        [IsamField(908, 3)]
        [Column("dep_skchk_prn",Order = 129)]
        public Decimal? SkchkPrn { get; set; }

        /// <summary>
        /// Max. users for this Depot
        /// </summary>
        [IsamField(911, 3)]
        [Column("dep_max_users",Order = 130)]
        public Decimal? MaxUsers { get; set; }

        /// <summary>
        /// GRN Label Type
        /// </summary>
        [IsamField(914, 1)]
        [Column("dep_grn_labels",Order = 131)]
        [Enum(typeof(DeprecGrnLabelsEnum), Start = 0, Step = 1)]
        public string GrnLabels { get; set; }

        /// <summary>
        /// IDT Label Type
        /// </summary>
        [IsamField(915, 1)]
        [Column("dep_idt_labels",Order = 132)]
        [Enum(typeof(DeprecIdtLabelsEnum), Start = 0, Step = 1)]
        public string IdtLabels { get; set; }

        /// <summary>
        /// Verifone Account ID
        /// </summary>
        [IsamField(916, 16)]
        [Column("dep_verifone_account",Order = 133)]
        public String VerifoneAccount { get; set; }

        /// <summary>
        /// Statement Required
        /// </summary>
        [IsamField(932, 1)]
        [Column("dep_def_statement",Order = 134)]
        [Enum(typeof(DeprecDefStatementEnum), Start = 0, Step = 1)]
        public string DefStatement { get; set; }

        /// <summary>
        /// Parts Email address
        /// </summary>
        [IsamField(933, 60)]
        [Column("dep_parts_email",Order = 135)]
        public String PartsEmail { get; set; }

        /// <summary>
        /// 3rd Party Nofification Email
        /// </summary>
        [IsamField(993, 60)]
        [Column("dep_notification_email",Order = 136)]
        public String NotificationEmail { get; set; }

        /// <summary>
        /// Spare
        /// </summary>
        [NotMapped]
        [IsamField(1053, 484)]
        [Column("dep_spare",Order = 137)]
        public String Spare { get; set; }

    }
}
