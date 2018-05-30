using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBenk.APi.Services
{
    public interface IMailService
    {
        void Send(string subject, string msg);
    }
    public class LocalMailService: IMailService
    {
        private string _mailTo = "648890146@qq.com";
        private string _mailFrom = "1244025801@qq.com";

        public void Send(string subject, string msg)
        {
            Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
}
