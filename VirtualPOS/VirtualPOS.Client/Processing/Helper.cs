using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using Newtonsoft.Json.Linq;
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
        public static dynamic RequestToServer(string request)
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
            if (String.IsNullOrEmpty(response))
            {
                response = @"{error_code:'96',error_message:'Có lỗi trong quá trình xử lý. Vui lòng thử lại sau'}";
            }
            return new eWallet.Data.DynamicObj(response);
        }

        public static dynamic GetProfile(string profile_id) {
            string request = @"{system:'app_pos_counter', module:'profile', function:'get_detail', type:'two_way', request:{user_name:'"
          + SessionVariables.CardNumber
          + "'}}";
            return Helper.RequestToServer(request).response;
        }

        public static dynamic RegisterCard()
        {
            string request = @"{system:'app_pos_counter', module:'profile', function:'register', type:'two_way', request:{full_name:'"
           + SessionVariables.CardOwner
           + "', id:'" + SessionVariables.CardNumber
           + "', mobile:'" + SessionVariables.MobileNumber + "'}}";
            return Helper.RequestToServer(request);
        }

        public static dynamic GetAccountInfo()
        {
            string request = @"{system:'app_pos_counter', module:'finance', function:'get_balance', type:'two_way', request:{profile_id:"
+ SessionVariables.ProfileId
+ "}}";
            return Helper.RequestToServer(request).response;
        }

    }

    public class SessionVariables
    {
        public static string CardNumber, CardOwner, CardValidDate, MobileNumber;
        public static long ProfileId;
        public static dynamic FinanceAccount;
        public static bool IsRegister;
    }
    public class ApplicationUser : IdentityUser
    {
        public string Status { get; set; }
    }
}
