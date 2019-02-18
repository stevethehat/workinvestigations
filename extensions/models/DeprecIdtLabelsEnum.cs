using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// IdtLabelsEnum
    /// </summary>
    public enum DeprecIdtLabelsEnum
    {
        /// <summary>
        /// 'None' <= None
        /// </summary>
        [Description("None")]
        None = 1,

        /// <summary>
        /// 'GrnBarcodeTml0' <= GRN Barcode (TML?0)
        /// </summary>
        [Description("GRN Barcode (TML?0)")]
        GrnBarcodeTml0 = 2,

        /// <summary>
        /// 'GrnBarcodeAvery' <= GRN Barcode (Avery)
        /// </summary>
        [Description("GRN Barcode (Avery)")]
        GrnBarcodeAvery = 3,

        /// <summary>
        /// 'GrnBarcodeIml' <= GRN Barcode (IML)
        /// </summary>
        [Description("GRN Barcode (IML)")]
        GrnBarcodeIml = 4,

        /// <summary>
        /// 'GrnBarcodeIml2' <= GRN Barcode (IML2)
        /// </summary>
        [Description("GRN Barcode (IML2)")]
        GrnBarcodeIml2 = 5,

        /// <summary>
        /// 'XmlZebra' <= XML (Zebra)
        /// </summary>
        [Description("XML (Zebra)")]
        XmlZebra = 6,

    }
}
