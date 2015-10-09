using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPOS.Client.Processing
{
    public class Helper
    {
        public static UserManager<ApplicationUser> UserManager { get; private set; }
        static eWalletGW.ChannelAPIClient client = new eWalletGW.ChannelAPIClient();   
        public static void Init()
        {
            UserManager = new UserManager<ApplicationUser>
                (
                new UserStore<ApplicationUser>("CoreDB_Server")
                );
        }
        public static string RequestToServer(string request)
        {
            string response = @"{error_code:'96',error_message:'Có lỗi trong quá trình xử lý. Vui lòng thử lại sau'}";
            if (client.State != System.ServiceModel.CommunicationState.Opened)
            {
                try
                {
                    client.Abort();
                    client = new eWalletGW.ChannelAPIClient();
                    client.Open();
                }
                catch
                {

                }
            }
            try
            {
                response = client.Process(request);
            }
            catch
            {
            }
            return response;
        }
    }

    public class SessionVariables
    {
        public static string CardNumber, CardOwner, CardValidDate, MobileNumber;
    }
    public class ApplicationUser : IdentityUser
    {
        public string Status { get; set; }
    }
}
