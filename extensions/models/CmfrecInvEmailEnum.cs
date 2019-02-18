using System.ComponentModel;

namespace Net.Ibcos.GoldAPIServer.DataLayer.Models
{
    /// <summary>
    /// InvEmailEnum
    /// </summary>
    public enum CmfrecInvEmailEnum
    {
        /// <summary>
        /// 'OnePerEmail' <= 1 per Email
        /// </summary>
        [Description("1 per Email")]
        OnePerEmail = 0,

        /// <summary>
        /// 'SepAttachments' <= Sep. Attachments
        /// </summary>
        [Description("Sep. Attachments")]
        SepAttachments = 1,

        /// <summary>
        /// 'SingleAttachment' <= Single Attachment
        /// </summary>
        [Description("Single Attachment")]
        SingleAttachment = 2,

    }
}
