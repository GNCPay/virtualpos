using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPOS.Client.Processing
{
    public class Helper
    {
        static eWalletGW.ChannelAPIClient client = new eWalletGW.ChannelAPIClient();   
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
}
