using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

/**
 * author:qixiao
 * 2017-4-5 15:26:53
 * */
namespace QX_Frame.Bantina
{
    public class Mail_Helper_DG
    {
        /// <summary>
        /// SendBySMTP
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="toMailAddress"></param>
        /// <param name="ccMailAddress"></param>
        /// <param name="fromMailAddress"></param>
        /// <param name="fromMailName"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <param name="isBodyHtml"></param>
        /// <param name="smtpHost"></param>
        /// <param name="smtpPort"></param>
        /// <param name="mailPriority">邮件等级</param>
        /// <returns></returns>
        public static bool SendBySMTP(string account, string password, string[] toMailAddress, string[] ccMailAddress, string fromMailAddress, string fromMailName, string mailSubject, string mailBody, bool isBodyHtml, string smtpHost = "smtp.163.com", int smtpPort = 25, MailPriority mailPriority = MailPriority.Normal)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //可以发送给多人
            foreach (var item in toMailAddress)
            {
                msg.To.Add(item);
            }
            //可以抄送给多人
            foreach (var item in ccMailAddress)
            {
                msg.CC.Add(item);
            }
            /* 3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
            msg.From = new MailAddress(fromMailAddress, fromMailName, System.Text.Encoding.UTF8);
            msg.Subject = mailSubject;//邮件标题   
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码   
            msg.Body = mailBody;//邮件内容   
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
            msg.IsBodyHtml = isBodyHtml;//是否是HTML邮件   
            msg.Priority = mailPriority;//邮件优先级   
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(account, password);//邮箱账号密码
            client.Port = smtpPort;//使用的端口   
            client.Host = smtpHost;
            client.EnableSsl = true;//经过ssl加密   
            object userState = msg;
            try
            {
                //client.Send(msg);   
                client.SendAsync(msg, userState);
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
