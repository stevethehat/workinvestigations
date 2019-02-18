using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Ibcos.GoldAPIServer.DataLayer;

// produced ModelBuilder.exe - 18/02/2019 11:12:59
namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("cmfrec", Schema ="companyccc")]
    [Isam("DV1:CMFccc.ISM", Compressed=true, StaticRFA=true, Length=2048)]
    [IsamKey("CMF_CUSTAC", Start="1", Length="6", IsAscending=true, Index=0)]
    [IsamKey("CMF_REP_NICK", Start="262:266", Length="4:10", Duplicates=true, IsAscending=true, Modifiable=true, Index=1)]
    [IsamKey("CMF_POSTCODE", Start="304:1", Length="9:6", Duplicates=true, IsAscending=true, Modifiable=true, Index=2)]
    [IsamKey("CMF_NICKNAME", Start="266:1", Length="10:6", Duplicates=true, IsAscending=true, Modifiable=true, Index=3)]
    [IsamKey("CMF_AREA_NICK", Start="535:266", Length="2:10", Duplicates=true, IsAscending=true, Modifiable=true, Index=4)]
    [IsamKey("CMF_CURR_NICK", Start="962:266", Length="3:10", Duplicates=true, IsAscending=true, Modifiable=true, Index=5)]
    [IsamKey("CMF_TEL_CUSTAC", Start="167:1", Length="20:6", IsAscending=true, Modifiable=true, Index=6)]
    [IsamKey("CMF_INV_ACC", Start="342:1", Length="6:6", IsAscending=true, Modifiable=true, Index=7)]
    [IsamKey("CMF_STAT_ACC", Start="336:1", Length="6:6", IsAscending=true, Modifiable=true, Index=8)]
    [IsamKey("CMF_EMAIL_ADD", Start="863", Length="60", Duplicates=true, IsAscending=true, Modifiable=true, Index=9)]
    [IsamKey("CMF_CUST_REF", Start="1313", Length="15", Duplicates=true, IsAscending=true, Modifiable=true, Index=10)]
    public partial class Cmfrec : GoldModel
    {
        /// <summary>
        /// Customer Account No ( KEY )
        /// </summary>
        [Key]
        [IsamField(1, 6)]
        [Column("cmf_acc_no",Order = 1)]
        public String AccNo { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        [IsamField(7, 32)]
        [Column("cmf_name",Order = 2)]
        public String Name { get; set; }

        /// <summary>
        /// First address line
        /// </summary>
        [IsamField(39, 32)]
        [Column("cmf_address1",Order = 3)]
        public String Address1 { get; set; }

        /// <summary>
        /// Second address line
        /// </summary>
        [IsamField(71, 32)]
        [Column("cmf_address2",Order = 4)]
        public String Address2 { get; set; }

        /// <summary>
        /// Third address line
        /// </summary>
        [IsamField(103, 32)]
        [Column("cmf_address3",Order = 5)]
        public String Address3 { get; set; }

        /// <summary>
        /// Fouth address line
        /// </summary>
        [IsamField(135, 32)]
        [Column("cmf_address4",Order = 6)]
        public String Address4 { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [IsamField(167, 20)]
        [Column("cmf_tel_no",Order = 11)]
        public String TelNo { get; set; }

        /// <summary>
        /// Balance b/f
        /// </summary>
        [IsamField(187, 12)]
        [Column("cmf_f_bal_bf",Order = 12)]
        public Decimal? FBalBf { get; set; }

        /// <summary>
        /// Credit limit
        /// </summary>
        [IsamField(199, 10)]
        [Column("cmf_f_cred_lmt",Order = 13)]
        public Decimal? FCredLmt { get; set; }

        /// <summary>
        /// B/F Age analysis
        /// </summary>
        [IsamField(209, 12)]
        [Column("cmf_f_age_1",Order = 14)]
        public Decimal? FAge_1 { get; set; }

        /// <summary>
        /// B/F Age analysis
        /// </summary>
        [IsamField(221, 12)]
        [Column("cmf_f_age_2",Order = 15)]
        public Decimal? FAge_2 { get; set; }

        /// <summary>
        /// B/F Age analysis
        /// </summary>
        [IsamField(233, 12)]
        [Column("cmf_f_age_3",Order = 16)]
        public Decimal? FAge_3 { get; set; }

        /// <summary>
        /// B/F Age analysis
        /// </summary>
        [IsamField(245, 12)]
        [Column("cmf_f_age_4",Order = 17)]
        public Decimal? FAge_4 { get; set; }

        /// <summary>
        /// Discount (XX.XX)
        /// </summary>
        [IsamField(257, 4)]
        [Column("cmf_discount",Order = 22)]
        public Decimal? Discount { get; set; }

        /// <summary>
        /// Ledger Status Flag
        /// </summary>
        [IsamField(261, 1)]
        [Column("cmf_acc_typ",Order = 23)]
        [Enum(typeof(CmfrecAccTypEnum), Start = 0, Step = 1)]
        public string AccTyp { get; set; }

        /// <summary>
        /// Representative Code
        /// </summary>
        [IsamField(262, 4)]
        [Column("cmf_rep",Order = 24)]
        public String Rep { get; set; }

        /// <summary>
        /// Nickname
        /// </summary>
        [IsamField(266, 10)]
        [Column("cmf_nick_name",Order = 25)]
        public String NickName { get; set; }

        /// <summary>
        /// Sales to date (whole number)
        /// </summary>
        [IsamField(276, 12)]
        [Column("cmf_sal_td",Order = 26)]
        public Decimal? SalTd { get; set; }

        /// <summary>
        /// Vat registration code
        /// </summary>
        [IsamField(288, 16)]
        [Column("cmf_vat_no",Order = 27)]
        public String VatNo { get; set; }

        /// <summary>
        /// Postcode
        /// </summary>
        [IsamField(304, 9)]
        [Column("cmf_post_cd",Order = 28)]
        public String PostCd { get; set; }

        /// <summary>
        /// Stop Flag
        /// </summary>
        [IsamField(313, 1)]
        [Column("cmf_stop",Order = 30)]
        [Enum(typeof(CmfrecStopEnum), Start = 0, Step = 1)]
        public string Stop { get; set; }

        /// <summary>
        /// Customer Discount Matrix Level
        /// </summary>
        [IsamField(314, 2)]
        [Column("cmf_dis_mtx",Order = 31)]
        public String DisMtx { get; set; }

        /// <summary>
        /// Customer Fax Number
        /// </summary>
        [IsamField(316, 20)]
        [Column("cmf_fax",Order = 32)]
        public String Fax { get; set; }

        /// <summary>
        /// Statement Account Number
        /// </summary>
        [IsamField(336, 6)]
        [Column("cmf_stat_acc",Order = 33)]
        public String StatAcc { get; set; }

        /// <summary>
        /// Invoice Account Number
        /// </summary>
        [IsamField(342, 6)]
        [Column("cmf_inv_acc",Order = 34)]
        public String InvAcc { get; set; }

        /// <summary>
        /// Invoicing Method
        /// </summary>
        [IsamField(348, 1)]
        [Column("cmf_inv_mthd",Order = 35)]
        [Enum(typeof(CmfrecInvMthdEnum), Start = 0, Step = 1)]
        public string InvMthd { get; set; }

        /// <summary>
        /// Accounting Contact Name
        /// </summary>
        [IsamField(349, 28)]
        [Column("cmf_acc_nam",Order = 36)]
        public String AccNam { get; set; }

        /// <summary>
        /// Accounting Contact Tel No.
        /// </summary>
        [IsamField(377, 20)]
        [Column("cmf_lacc_tel",Order = 37)]
        public String LaccTel { get; set; }

        /// <summary>
        /// Sales Contact Name
        /// </summary>
        [IsamField(397, 28)]
        [Column("cmf_sal_nam",Order = 38)]
        public String SalNam { get; set; }

        /// <summary>
        /// Sales Contact Tel No.
        /// </summary>
        [IsamField(425, 20)]
        [Column("cmf_lsal_tel",Order = 39)]
        public String LsalTel { get; set; }

        /// <summary>
        /// Total Uninvoiced value (for Cred Lmt)FC
        /// </summary>
        [IsamField(445, 12)]
        [Column("cmf_f_un_inv_v",Order = 40)]
        public Decimal? FUnInvV { get; set; }

        /// <summary>
        /// Date account created YYYYMMDD
        /// </summary>
        [IsamField(457, 8)]
        [Column("cmf_dat_create",Order = 41)]
        public DateTime? DatCreate { get; set; }

        /// <summary>
        /// Average Amount Paid FC
        /// </summary>
        [IsamField(465, 10)]
        [Column("cmf_f_avg_amt",Order = 42)]
        [Precision(2)]
        public Decimal? FAvgAmt { get; set; }

        /// <summary>
        /// Average Days to pay an invoice
        /// </summary>
        [IsamField(475, 3)]
        [Column("cmf_avg_day",Order = 43)]
        public Decimal? AvgDay { get; set; }

        /// <summary>
        /// Number of payments
        /// </summary>
        [IsamField(478, 5)]
        [Column("cmf_num_pay",Order = 44)]
        public Decimal? NumPay { get; set; }

        /// <summary>
        /// Default Settlement/credit Percentage
        /// </summary>
        [IsamField(483, 4)]
        [Column("cmf_sc_perc",Order = 45)]
        public Decimal? ScPerc { get; set; }

        /// <summary>
        /// Last Payment Date YYYYMMDD
        /// </summary>
        [IsamField(487, 8)]
        [Column("cmf_last_pay",Order = 46)]
        public DateTime? LastPay { get; set; }

        /// <summary>
        /// Last Amount Paid (XXXXXX.XX)  fc
        /// </summary>
        [IsamField(495, 10)]
        [Column("cmf_f_last_amt",Order = 47)]
        [Precision(2)]
        public Decimal? FLastAmt { get; set; }

        /// <summary>
        /// Sales Prev Year 1 tot(Whole No Only 0dp)
        /// </summary>
        [IsamField(505, 12)]
        [Column("cmf_prev_yr1",Order = 48)]
        public Decimal? PrevYr1 { get; set; }

        /// <summary>
        /// Sales Prev Year 2 Tot(Whole No Only 0dp)
        /// </summary>
        [IsamField(517, 12)]
        [Column("cmf_prev_yr2",Order = 49)]
        public Decimal? PrevYr2 { get; set; }

        /// <summary>
        /// Created via NEW_CUST
        /// </summary>
        [IsamField(529, 1)]
        [Column("cmf_pos_wsj",Order = 50)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string PosWsj { get; set; }

        /// <summary>
        /// Number of transactions paid
        /// </summary>
        [IsamField(530, 5)]
        [Column("cmf_num_trn",Order = 51)]
        public Decimal? NumTrn { get; set; }

        /// <summary>
        /// Customer Area
        /// </summary>
        [IsamField(535, 2)]
        [Column("cmf_area",Order = 52)]
        public String Area { get; set; }

        /// <summary>
        /// Spare to extend Area code in future
        /// </summary>
        [IsamField(537, 2)]
        [Column("cmf_spare1",Order = 53)]
        public String Spare1 { get; set; }

        /// <summary>
        /// Apply VAT
        /// </summary>
        [IsamField(539, 1)]
        [Column("cmf_vat",Order = 54)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string Vat { get; set; }

        /// <summary>
        /// Transport code
        /// </summary>
        [IsamField(540, 1)]
        [Column("cmf_transport",Order = 55)]
        public Decimal? Transport { get; set; }

        /// <summary>
        /// Delivery code
        /// </summary>
        [IsamField(541, 3)]
        [Column("cmf_delivery",Order = 56)]
        public String Delivery { get; set; }

        /// <summary>
        /// Default Commodity Code
        /// </summary>
        [IsamField(544, 8)]
        [Column("cmf_def_comcod",Order = 57)]
        public String DefComcod { get; set; }

        /// <summary>
        /// Distance to Farm
        /// </summary>
        [IsamField(552, 3)]
        [Column("cmf_distance",Order = 58)]
        public Decimal? Distance { get; set; }

        /// <summary>
        /// Current Year Workshop Cost
        /// </summary>
        [IsamField(555, 12)]
        [Column("cmf_sal_wsj__cost",Order = 59)]
        public Decimal? SalWsj__Cost { get; set; }

        /// <summary>
        /// Current Year Workshop Sales
        /// </summary>
        [IsamField(567, 12)]
        [Column("cmf_sal_wsj__sales",Order = 60)]
        public Decimal? SalWsj__Sales { get; set; }

        /// <summary>
        /// Current Year POS Cost
        /// </summary>
        [IsamField(579, 12)]
        [Column("cmf_sal_pos__cost",Order = 63)]
        public Decimal? SalPos__Cost { get; set; }

        /// <summary>
        /// Current Year POS Sales
        /// </summary>
        [IsamField(591, 12)]
        [Column("cmf_sal_pos__sales",Order = 64)]
        public Decimal? SalPos__Sales { get; set; }

        /// <summary>
        /// Current Year Wholegood Cost
        /// </summary>
        [IsamField(603, 12)]
        [Column("cmf_sal_wit__cost",Order = 67)]
        public Decimal? SalWit__Cost { get; set; }

        /// <summary>
        /// Current Year Wholegood Sales
        /// </summary>
        [IsamField(615, 12)]
        [Column("cmf_sal_wit__sales",Order = 68)]
        public Decimal? SalWit__Sales { get; set; }

        /// <summary>
        /// Current Year Vehicle Cost
        /// </summary>
        [IsamField(627, 12)]
        [Column("cmf_sal_veh__cost",Order = 71)]
        public Decimal? SalVeh__Cost { get; set; }

        /// <summary>
        /// Current Year Vehicle Sales
        /// </summary>
        [IsamField(639, 12)]
        [Column("cmf_sal_veh__sales",Order = 72)]
        public Decimal? SalVeh__Sales { get; set; }

        /// <summary>
        /// Previous Year Workshop Cost
        /// </summary>
        [IsamField(651, 12)]
        [Column("cmf_psal_wsj__cost",Order = 75)]
        public Decimal? PsalWsj__Cost { get; set; }

        /// <summary>
        /// Previous Year Workshop Sales
        /// </summary>
        [IsamField(663, 12)]
        [Column("cmf_psal_wsj__sales",Order = 76)]
        public Decimal? PsalWsj__Sales { get; set; }

        /// <summary>
        /// Previous Year POS Cost
        /// </summary>
        [IsamField(675, 12)]
        [Column("cmf_psal_pos__cost",Order = 79)]
        public Decimal? PsalPos__Cost { get; set; }

        /// <summary>
        /// Previous Year POS Sales
        /// </summary>
        [IsamField(687, 12)]
        [Column("cmf_psal_pos__sales",Order = 80)]
        public Decimal? PsalPos__Sales { get; set; }

        /// <summary>
        /// Previous Year Wholegoods Cost
        /// </summary>
        [IsamField(699, 12)]
        [Column("cmf_psal_wit__cost",Order = 83)]
        public Decimal? PsalWit__Cost { get; set; }

        /// <summary>
        /// Previous Year Wholegoods Sales
        /// </summary>
        [IsamField(711, 12)]
        [Column("cmf_psal_wit__sales",Order = 84)]
        public Decimal? PsalWit__Sales { get; set; }

        /// <summary>
        /// Previous Year Vehicle Cost
        /// </summary>
        [IsamField(723, 12)]
        [Column("cmf_psal_veh__cost",Order = 88)]
        public Decimal? PsalVeh__Cost { get; set; }

        /// <summary>
        /// Previous Year Vehicle Sales
        /// </summary>
        [IsamField(735, 12)]
        [Column("cmf_psal_veh__sales",Order = 89)]
        public Decimal? PsalVeh__Sales { get; set; }

        /// <summary>
        /// Current Year Hire Cost
        /// </summary>
        [IsamField(747, 12)]
        [Column("cmf_sal_hire__cost",Order = 92)]
        public Decimal? SalHire__Cost { get; set; }

        /// <summary>
        /// Current Year Hire Sales
        /// </summary>
        [IsamField(759, 12)]
        [Column("cmf_sal_hire__sales",Order = 93)]
        public Decimal? SalHire__Sales { get; set; }

        /// <summary>
        /// Previous Year Hire Cost
        /// </summary>
        [IsamField(771, 12)]
        [Column("cmf_psal_hire__cost",Order = 96)]
        public Decimal? PsalHire__Cost { get; set; }

        /// <summary>
        /// Previous Year Hire Sales
        /// </summary>
        [IsamField(783, 12)]
        [Column("cmf_psal_hire__sales",Order = 97)]
        public Decimal? PsalHire__Sales { get; set; }

        /// <summary>
        /// Statement Required
        /// </summary>
        [IsamField(795, 1)]
        [Column("cmf_no_statement",Order = 100)]
        [Enum(typeof(CmfrecNoStatementEnum), Start = 0, Step = 1)]
        public string NoStatement { get; set; }

        /// <summary>
        /// Alternative Telephone Number
        /// </summary>
        [IsamField(796, 20)]
        [Column("cmf_alt_teln",Order = 101)]
        public String AltTeln { get; set; }

        /// <summary>
        /// Default Interest Percentage (0 - 99.99%)
        /// </summary>
        [IsamField(816, 4)]
        [Column("cmf_int_per",Order = 102)]
        [Precision(2)]
        public Decimal? IntPer { get; set; }

        /// <summary>
        /// Code of Debt Chasing Letter Sent
        /// </summary>
        [IsamField(820, 1)]
        [Column("cmf_dbt_code",Order = 103)]
        [Enum(typeof(CmfrecDbtCodeEnum), Start = 0, Step = 1)]
        public string DbtCode { get; set; }

        /// <summary>
        /// Outlet type
        /// </summary>
        [IsamField(821, 1)]
        [Column("cmf_out_typ",Order = 104)]
        public String OutTyp { get; set; }

        /// <summary>
        /// Invoice frequency
        /// </summary>
        [IsamField(822, 1)]
        [Column("cmf_typ_inv",Order = 106)]
        public String TypInv { get; set; }

        /// <summary>
        /// Debt Letter to be sent?
        /// </summary>
        [IsamField(823, 1)]
        [Column("cmf_dbt_let",Order = 107)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DbtLet { get; set; }

        /// <summary>
        /// Debt remark
        /// </summary>
        [IsamField(824, 10)]
        [Column("cmf_dbt_remark",Order = 108)]
        public String DbtRemark { get; set; }

        /// <summary>
        /// Post office sort code
        /// </summary>
        [IsamField(834, 6)]
        [Column("cmf_mail_sort",Order = 109)]
        public String MailSort { get; set; }

        /// <summary>
        /// Accumulate Invoices?
        /// </summary>
        [IsamField(840, 1)]
        [Column("cmf_accum_o",Order = 110)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string AccumO { get; set; }

        /// <summary>
        /// Default salesmen (POS,WSJ,WG,VEH)
        /// </summary>
        [IsamField(841, 4)]
        [Column("cmf_def_rep_1",Order = 111)]
        public String DefRep_1 { get; set; }

        /// <summary>
        /// Default salesmen (POS,WSJ,WG,VEH)
        /// </summary>
        [IsamField(845, 4)]
        [Column("cmf_def_rep_2",Order = 112)]
        public String DefRep_2 { get; set; }

        /// <summary>
        /// Default salesmen (POS,WSJ,WG,VEH)
        /// </summary>
        [IsamField(849, 4)]
        [Column("cmf_def_rep_3",Order = 113)]
        public String DefRep_3 { get; set; }

        /// <summary>
        /// Default salesmen (POS,WSJ,WG,VEH)
        /// </summary>
        [IsamField(853, 4)]
        [Column("cmf_def_rep_4",Order = 114)]
        public String DefRep_4 { get; set; }

        /// <summary>
        /// Default POS invoice type
        /// </summary>
        [IsamField(857, 2)]
        [Column("cmf_def_invtyp",Order = 115)]
        public String DefInvtyp { get; set; }

        /// <summary>
        /// Linked Purchase Account
        /// </summary>
        [IsamField(859, 4)]
        [Column("cmf_pur_acc",Order = 116)]
        public Decimal? PurAcc { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        [IsamField(863, 60)]
        [Column("cmf_email",Order = 118)]
        public String Email { get; set; }

        /// <summary>
        /// Days Credit given
        /// </summary>
        [IsamField(923, 3)]
        [Column("cmf_pay_given",Order = 119)]
        public Decimal? PayGiven { get; set; }

        /// <summary>
        /// Use Tradanet Invoicing
        /// </summary>
        [IsamField(926, 1)]
        [Column("cmf_tradanet",Order = 120)]
        [Enum(typeof(CmfrecTradanetEnum), Start = 0, Step = 1)]
        public string Tradanet { get; set; }

        /// <summary>
        /// Tradanet ANA code
        /// </summary>
        [IsamField(927, 13)]
        [Column("cmf_ana_code",Order = 122)]
        public Decimal? AnaCode { get; set; }

        /// <summary>
        /// Tradanet customer id for us on Tradanet
        /// </summary>
        [IsamField(940, 16)]
        [Column("cmf_tnet_id",Order = 124)]
        public String TnetId { get; set; }

        /// <summary>
        /// Customer Application Ref on Tradanet
        /// </summary>
        [IsamField(956, 6)]
        [Column("cmf_applic_ref",Order = 128)]
        public String ApplicRef { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        [IsamField(962, 3)]
        [Column("cmf_cur_cod",Order = 130)]
        public String CurCod { get; set; }

        /// <summary>
        /// Alt. Currency On Invoice
        /// </summary>
        [IsamField(965, 1)]
        [Column("cmf_cur_alt",Order = 131)]
        [Enum(typeof(CmfrecCurAltEnum), Start = 0, Step = 1)]
        public string CurAlt { get; set; }

        /// <summary>
        /// Sett/Cred Charge Option
        /// </summary>
        [IsamField(966, 1)]
        [Column("cmf_set_cred",Order = 132)]
        [Enum(typeof(CmfrecSetCredEnum), Start = 0, Step = 1)]
        public string SetCred { get; set; }

        /// <summary>
        /// sett/cred base
        /// </summary>
        [IsamField(967, 1)]
        [Column("cmf_sc_from",Order = 133)]
        [Enum(typeof(CmfrecScFromEnum), Start = 0, Step = 1)]
        public string ScFrom { get; set; }

        /// <summary>
        /// Force Order Number?
        /// </summary>
        [IsamField(968, 1)]
        [Column("cmf_force_ord",Order = 134)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string ForceOrd { get; set; }

        /// <summary>
        /// Salutation for debt letter
        /// </summary>
        [IsamField(969, 20)]
        [Column("cmf_salutation",Order = 135)]
        public String Salutation { get; set; }

        /// <summary>
        /// Date Debt Letter Last Sent
        /// </summary>
        [IsamField(989, 8)]
        [Column("cmf_dbt_date",Order = 136)]
        public DateTime? DbtDate { get; set; }

        /// <summary>
        /// Statement alternative currency code
        /// </summary>
        [IsamField(997, 3)]
        [Column("cmf_stat_cur",Order = 137)]
        public String StatCur { get; set; }

        /// <summary>
        /// Analysis Codes - 10 * 1 character
        /// </summary>
        [IsamField(1000, 10)]
        [Column("cmf_analysis",Order = 138)]
        public String Analysis { get; set; }

        /// <summary>
        /// Full Access Modification Made?
        /// </summary>
        [IsamField(1010, 1)]
        [Column("cmf_authorised_acc",Order = 139)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string AuthorisedAcc { get; set; }

        /// <summary>
        /// Account Created by
        /// </summary>
        [IsamField(1011, 3)]
        [Column("cmf_acc_by",Order = 140)]
        public String AccBy { get; set; }

        /// <summary>
        /// Total Outstanding @Month End
        /// </summary>
        [IsamField(1014, 12)]
        [Column("cmf_stat_bfd",Order = 141)]
        public Decimal? StatBfd { get; set; }

        /// <summary>
        /// (Internal IBCOS use only) Bank Sort Code
        /// </summary>
        [IsamField(1026, 6)]
        [Column("cmf_sort_code",Order = 142)]
        public String SortCode { get; set; }

        /// <summary>
        /// (Internal IBCOS use only) Gold Version
        /// </summary>
        [IsamField(1032, 2)]
        [Column("cmf_gold_ver",Order = 143)]
        public String GoldVer { get; set; }

        /// <summary>
        /// Customer Order Value
        /// </summary>
        [IsamField(1034, 10)]
        [Column("cmf_ord_val",Order = 144)]
        [Precision(2)]
        public Decimal? OrdVal { get; set; }

        /// <summary>
        /// First Delivery Address Line
        /// </summary>
        [IsamField(1044, 32)]
        [Column("cmf_del_address1",Order = 145)]
        public String DelAddress1 { get; set; }

        /// <summary>
        /// Second Delivery Address Line
        /// </summary>
        [IsamField(1076, 32)]
        [Column("cmf_del_address2",Order = 146)]
        public String DelAddress2 { get; set; }

        /// <summary>
        /// Third Delivery Address Line
        /// </summary>
        [IsamField(1108, 32)]
        [Column("cmf_del_address3",Order = 147)]
        public String DelAddress3 { get; set; }

        /// <summary>
        /// Fourth Delivery Address Line
        /// </summary>
        [IsamField(1140, 32)]
        [Column("cmf_del_address4",Order = 148)]
        public String DelAddress4 { get; set; }

        /// <summary>
        /// Delivery Postcode
        /// </summary>
        [IsamField(1172, 9)]
        [Column("cmf_del_postcode",Order = 153)]
        public String DelPostcode { get; set; }

        /// <summary>
        /// Exclude From ODBC Reports/Mailshots?
        /// </summary>
        [IsamField(1181, 1)]
        [Column("cmf_exc_from_odbc",Order = 154)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string ExcFromOdbc { get; set; }

        /// <summary>
        /// WSJ Priority Code
        /// </summary>
        [IsamField(1182, 3)]
        [Column("cmf_wsj_pri",Order = 155)]
        public String WsjPri { get; set; }

        /// <summary>
        /// WSJ default labour discount
        /// </summary>
        [IsamField(1185, 4)]
        [Column("cmf_wsj_lab_disc",Order = 156)]
        public Decimal? WsjLabDisc { get; set; }

        /// <summary>
        /// Self Bill Invoice Expiry Date
        /// </summary>
        [IsamField(1189, 8)]
        [Column("cmf_sb_exp_date",Order = 157)]
        public DateTime? SbExpDate { get; set; }

        /// <summary>
        /// Send Invoices By
        /// </summary>
        [IsamField(1197, 1)]
        [Column("cmf_send_inv",Order = 158)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string SendInv { get; set; }

        /// <summary>
        /// Invoice Contact Type Code
        /// </summary>
        [IsamField(1198, 3)]
        [Column("cmf_send_inv_to",Order = 159)]
        public String SendInvTo { get; set; }

        /// <summary>
        /// Send Statements By
        /// </summary>
        [IsamField(1201, 1)]
        [Column("cmf_send_stat",Order = 160)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string SendStat { get; set; }

        /// <summary>
        /// Statement Contact Type Code
        /// </summary>
        [IsamField(1202, 3)]
        [Column("cmf_send_stat_to",Order = 161)]
        public String SendStatTo { get; set; }

        /// <summary>
        /// Send Quotes By
        /// </summary>
        [IsamField(1205, 1)]
        [Column("cmf_send_quote",Order = 162)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string SendQuote { get; set; }

        /// <summary>
        /// Quote Contact Type Code
        /// </summary>
        [IsamField(1206, 3)]
        [Column("cmf_send_quote_to",Order = 163)]
        public String SendQuoteTo { get; set; }

        /// <summary>
        /// Send Mailshots By
        /// </summary>
        [IsamField(1209, 1)]
        [Column("cmf_send_mailshot",Order = 164)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string SendMailshot { get; set; }

        /// <summary>
        /// Mailshot Contact Type Code
        /// </summary>
        [IsamField(1210, 3)]
        [Column("cmf_send_mailshot_to",Order = 165)]
        public String SendMailshotTo { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1213, 10)]
        [Column("cmf_discount_club1",Order = 166)]
        public String DiscountClub1 { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1223, 10)]
        [Column("cmf_discount_club2",Order = 167)]
        public String DiscountClub2 { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1233, 10)]
        [Column("cmf_discount_club3",Order = 168)]
        public String DiscountClub3 { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1243, 10)]
        [Column("cmf_discount_club4",Order = 169)]
        public String DiscountClub4 { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1253, 10)]
        [Column("cmf_discount_club5",Order = 170)]
        public String DiscountClub5 { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1263, 10)]
        [Column("cmf_discount_club6",Order = 171)]
        public String DiscountClub6 { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1273, 10)]
        [Column("cmf_discount_club7",Order = 172)]
        public String DiscountClub7 { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1283, 10)]
        [Column("cmf_discount_club8",Order = 173)]
        public String DiscountClub8 { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1293, 10)]
        [Column("cmf_discount_club9",Order = 174)]
        public String DiscountClub9 { get; set; }

        /// <summary>
        /// Special discount/carriage discount club
        /// </summary>
        [IsamField(1303, 10)]
        [Column("cmf_discount_club10",Order = 175)]
        public String DiscountClub10 { get; set; }

        /// <summary>
        /// Customer's Reference
        /// </summary>
        [IsamField(1313, 15)]
        [Column("cmf_acc_ref",Order = 186)]
        public String AccRef { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(1328, 1)]
        [Column("cmf_debt_reminder_level_1",Order = 187)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DebtReminderLevel_1 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(1329, 1)]
        [Column("cmf_debt_reminder_level_2",Order = 188)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DebtReminderLevel_2 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(1330, 1)]
        [Column("cmf_debt_reminder_level_3",Order = 189)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DebtReminderLevel_3 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(1331, 1)]
        [Column("cmf_debt_reminder_level_4",Order = 190)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DebtReminderLevel_4 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(1332, 1)]
        [Column("cmf_debt_reminder_level_5",Order = 191)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DebtReminderLevel_5 { get; set; }

        /// <summary>
        /// Debt Letter Reminder Level Flag
        /// </summary>
        [IsamField(1333, 1)]
        [Column("cmf_debt_reminder_level_6",Order = 192)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string DebtReminderLevel_6 { get; set; }

        /// <summary>
        /// (IBCOS Internal Use Only) Bad Debt Date
        /// </summary>
        [IsamField(1334, 8)]
        [Column("cmf_bad_debt_date",Order = 193)]
        public DateTime? BadDebtDate { get; set; }

        /// <summary>
        /// (IBCOS Internal Use Only) Program Update
        /// </summary>
        [IsamField(1342, 8)]
        [Column("cmf_pu_date",Order = 194)]
        public DateTime? PuDate { get; set; }

        /// <summary>
        /// VAT Code
        /// </summary>
        [IsamField(1350, 2)]
        [Column("cmf_def_vat",Order = 195)]
        public Decimal? DefVat { get; set; }

        /// <summary>
        /// Code of the primary contact for the cust
        /// </summary>
        [IsamField(1352, 3)]
        [Column("cmf_primary_con_code",Order = 196)]
        public String PrimaryConCode { get; set; }

        /// <summary>
        /// Potential Spend Code
        /// </summary>
        [IsamField(1355, 1)]
        [Column("cmf_potential",Order = 197)]
        public String Potential { get; set; }

        /// <summary>
        /// Customer Loyalty Code
        /// </summary>
        [IsamField(1356, 1)]
        [Column("cmf_cur_loyalty",Order = 198)]
        public String CurLoyalty { get; set; }

        /// <summary>
        /// Send Debt Letters By
        /// </summary>
        [IsamField(1357, 1)]
        [Column("cmf_send_debt",Order = 199)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string SendDebt { get; set; }

        /// <summary>
        /// Debt Letter Contact Type Code
        /// </summary>
        [IsamField(1358, 3)]
        [Column("cmf_send_debt_to",Order = 200)]
        public String SendDebtTo { get; set; }

        /// <summary>
        /// Invoice Email Option
        /// </summary>
        [IsamField(1361, 1)]
        [Column("cmf_inv_email",Order = 201)]
        [Enum(typeof(CmfrecInvEmailEnum), Start = 0, Step = 1)]
        public string InvEmail { get; set; }

        /// <summary>
        /// Max. Email size (Mb)
        /// </summary>
        [IsamField(1362, 2)]
        [Column("cmf_max_email",Order = 202)]
        public Decimal? MaxEmail { get; set; }

        /// <summary>
        /// Total Uninvoiced value (for Cred Lmt)FC
        /// </summary>
        [IsamField(1364, 12)]
        [Column("cmf_f_un_inv_tmp",Order = 203)]
        public Decimal? FUnInvTmp { get; set; }

        /// <summary>
        /// Customer Order Value
        /// </summary>
        [IsamField(1376, 10)]
        [Column("cmf_ord_tmp",Order = 204)]
        [Precision(2)]
        public Decimal? OrdTmp { get; set; }

        /// <summary>
        /// Invoice CC Contact Type Code
        /// </summary>
        [IsamField(1386, 3)]
        [Column("cmf_send_inv_cc",Order = 205)]
        public String SendInvCc { get; set; }

        /// <summary>
        /// Statement CC Contact Type Code
        /// </summary>
        [IsamField(1389, 3)]
        [Column("cmf_send_stat_cc",Order = 206)]
        public String SendStatCc { get; set; }

        /// <summary>
        /// Debt Letter CC Contact Type Code
        /// </summary>
        [IsamField(1392, 3)]
        [Column("cmf_send_debt_cc",Order = 207)]
        public String SendDebtCc { get; set; }

        /// <summary>
        /// Quote CC Contact Type Code
        /// </summary>
        [IsamField(1395, 3)]
        [Column("cmf_send_quote_cc",Order = 208)]
        public String SendQuoteCc { get; set; }

        /// <summary>
        /// Mailshot CC Contact Type Code
        /// </summary>
        [IsamField(1398, 3)]
        [Column("cmf_send_mailshot_cc",Order = 209)]
        public String SendMailshotCc { get; set; }

        /// <summary>
        /// Send POS Invoices Electronically?
        /// </summary>
        [IsamField(1401, 1)]
        [Column("cmf_send_pos_xml",Order = 210)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string SendPosXml { get; set; }

        /// <summary>
        /// Contact Code
        /// </summary>
        [IsamField(1402, 3)]
        [Column("cmf_send_pos_xml_to",Order = 211)]
        public String SendPosXmlTo { get; set; }

        /// <summary>
        /// Send Proforma Invoices By
        /// </summary>
        [IsamField(1405, 1)]
        [Column("cmf_send_pf_inv",Order = 212)]
        [Enum(typeof(DocSendBy), Start = 1, Step = 1)]
        public string SendPfInv { get; set; }

        /// <summary>
        /// Proforma Contact Type Code
        /// </summary>
        [IsamField(1406, 3)]
        [Column("cmf_send_pf_inv_to",Order = 213)]
        public String SendPfInvTo { get; set; }

        /// <summary>
        /// Proforma CC Contact Type Code
        /// </summary>
        [IsamField(1409, 3)]
        [Column("cmf_send_pf_inv_cc",Order = 214)]
        public String SendPfInvCc { get; set; }

        /// <summary>
        /// CNH Prim sub channel (was cmf_analysis).
        /// </summary>
        [IsamField(1412, 2)]
        [Column("cmf_cnh_sub_chn",Order = 215)]
        public String CnhSubChn { get; set; }

        /// <summary>
        /// CNH Prim Payment Channel (was cmf_mail_)
        /// </summary>
        [IsamField(1414, 1)]
        [Column("cmf_cnh_pay_chn",Order = 216)]
        public String CnhPayChn { get; set; }

        /// <summary>
        /// CNH Prim Exceptional demand flag
        /// </summary>
        [IsamField(1415, 1)]
        [Column("cmf_cnh_exp_demand",Order = 217)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string CnhExpDemand { get; set; }

        /// <summary>
        /// Account Obsolete?
        /// </summary>
        [IsamField(1416, 1)]
        [Column("cmf_obsolete",Order = 218)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string Obsolete { get; set; }

        /// <summary>
        /// Original Customer Account
        /// </summary>
        [IsamField(1417, 6)]
        [Column("cmf_orig_cust_acc",Order = 219)]
        public String OrigCustAcc { get; set; }

        /// <summary>
        /// Contact by email?
        /// </summary>
        [IsamField(1423, 1)]
        [Column("cmf_marketing_by_email",Order = 220)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string MarketingByEmail { get; set; }

        /// <summary>
        /// Contact by post/fax?
        /// </summary>
        [IsamField(1424, 1)]
        [Column("cmf_marketing_by_post",Order = 221)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string MarketingByPost { get; set; }

        /// <summary>
        /// Contact by text?
        /// </summary>
        [IsamField(1425, 1)]
        [Column("cmf_marketing_by_text",Order = 222)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string MarketingByText { get; set; }

        /// <summary>
        /// Contact by phone?
        /// </summary>
        [IsamField(1426, 1)]
        [Column("cmf_marketing_by_phone",Order = 223)]
        [Enum(typeof(GoldBool), Start = 1, Step = 1)]
        public string MarketingByPhone { get; set; }

        /// <summary>
        /// Account as it was in Sage or whatever
        /// </summary>
        [IsamField(1427, 20)]
        [Column("cmf_imported_account",Order = 224)]
        public String ImportedAccount { get; set; }

        /// <summary>
        /// Spare
        /// </summary>
        [NotMapped]
        [IsamField(1447, 602)]
        [Column("cmf_spare",Order = 225)]
        public String Spare { get; set; }

    }
}
