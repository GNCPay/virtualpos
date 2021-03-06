﻿using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualPOS.Client.Processing
{
    public class Helper
    {
        public static eWallet.Data.MongoHelper DataHelper;
        public static UserManager<ApplicationUser> UserManager { get; private set; }
        static eWalletGW.ChannelAPIClient client = new eWalletGW.ChannelAPIClient();
        public static void Init()
        {
            UserManager = new UserManager<ApplicationUser>
                (
                new UserStore<ApplicationUser>("CoreDB_Server")
                );
            DataHelper = new eWallet.Data.MongoHelper(
               ConfigurationSettings.AppSettings["CoreDB_Server"],
                ConfigurationSettings.AppSettings["CoreDB_Database"]
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

        public static dynamic GetProfile()
        {
            string request = @"{system:'app_pos_counter', module:'profile', function:'get_detail', type:'two_way', request:{user_name:'"
          + SessionVariables.CardId
          + "'}}";
            return Helper.RequestToServer(request).response;
        }

        public static dynamic RegisterCard()
        {
            string request = @"{system:'app_pos_counter', module:'profile', function:'register', type:'two_way', request:{full_name:'"
           + SessionVariables.CardOwner
           + "', id:'" + SessionVariables.CardId
           + "', mobile:'" + SessionVariables.MobileNumber 
           + "', email:'" + SessionVariables.Email
           + "', personal_id:'" + SessionVariables.Personal_id
           + "',address:'" + SessionVariables.Address
           +"'}}";
            return Helper.RequestToServer(request);
        }

        public static dynamic GetAccountInfo()
        {
            string request = @"{system:'app_pos_counter', module:'finance', function:'get_balance', type:'two_way', request:{profile_id:"
+ SessionVariables.ProfileId
+ "}}";
            return Helper.RequestToServer(request).response;
        }
        public static dynamic CashIn(long amount)
        {
            string request = @"{system:'web_frontend', module:'transaction',type:'two_way', function:'CASHIN',request:{channel:'WEB', profile:'"
               + SessionVariables.CardId + "',service:'GNCP', provider:'VINHOME',payment_provider:'VINHOME',amount: " + amount +
         ", note: '" + "NAP TIEN CHO THE" +
         "'}}";
            dynamic cashin= Helper.RequestToServer(request);
            string trans_id;
            if (cashin.error_code == "00")
            {
                trans_id = cashin.response.trans_id;
                string confirm = @"{system:'web_frontend', module:'transaction',type:'two_way',function:'confirm',request:{user_id:'" + SessionVariables.CardId
                   + "',transaction_type:'" + "CASHIN" + "', trans_id:'" + trans_id + "', amount: " + amount + "}}";
                dynamic confirm_result = Helper.RequestToServer(confirm);
                confirm_result.trans_id = trans_id;
                confirm_result.amount = amount;
                confirm_result.transaction_type = "CASHIN";
                return confirm_result;
            }
            return cashin;
        }

        public static dynamic CashOut(long amount) {
            string request = @"{system:'web_frontend', module:'transaction',type:'two_way', function:'CASHOUT',request:{channel:'WEB', profile:'"
                +SessionVariables.CardId + "',service:'GNCP', provider:'BANK',payment_provider:'GNCC',amount: " + amount +
          ", note: '" + "RUT TIEN MAT TU THE" +
          "', receiver:{account_bank:'" + "GNC PAY TELLER" +
          "', account_branch:'" + "VINHOMES" + "',account_number:'" + SessionVariables.TellerUser.UserName +
          "',account_name:'" + SessionVariables.TellerUser.UserName + "'}}}";
            dynamic cashout = Helper.RequestToServer(request);
            string trans_id;
            if (cashout.error_code == "00")
            {
                trans_id = cashout.response.trans_id;
                string confirm = @"{system:'web_frontend', module:'transaction',type:'two_way',function:'confirm',request:{user_id:'" + SessionVariables.CardId
                   + "',transaction_type:'" + "CASHOUT" + "', trans_id:'" + trans_id + "', amount: " + amount + "}}";
                dynamic confirm_result = Helper.RequestToServer(confirm);
                confirm_result.trans_id = trans_id;
                confirm_result.amount = amount;
                confirm_result.transaction_type = "CASHOUT";
                return confirm_result;
            }
            return cashout;
        }

        public static dynamic PayBill(string bill_no, long amount)
        {
            string request = @"{system:'web_frontend', module:'transaction',type:'two_way', function:'payment',request:{channel:'web', profile:'"
               + SessionVariables.CardId
              + "', product_code: '" + bill_no
              + "', service: '" + "ECOM"
              + "', provider: '" + "VINHOME"
              + "', amount: " + amount
              + ", payment_provider: '" + "GNCC"
              + "', bank: '" + "GNCC" +
          "'}}";
            dynamic response = Helper.RequestToServer(request);
            string trans_id;
            if (response.error_code == "00")
            {
                trans_id = response.response.trans_id;
                string confirm = @"{system:'web_frontend', module:'transaction',type:'two_way',function:'confirm',request:{user_id:'" + SessionVariables.CardNumber
                   + "',transaction_type:'" + "PAYMENT" + "', trans_id:'" + trans_id + "', amount: " + amount + "}}";
                dynamic confirm_result = Helper.RequestToServer(confirm);
                confirm_result.trans_id = trans_id;
                confirm_result.amount = amount;
                return confirm_result;
            }
            return response;
        }

        public static char GetCharFromKeys(Keys keyData)
        {
            char KeyValue;
            switch (keyData)
            {
                case Keys.Add:
                case Keys.Oemplus:
                    KeyValue = '+';
                    break;
                case Keys.OemMinus:
                case Keys.Subtract:
                    KeyValue = '-';
                    break;
                case Keys.OemQuestion | Keys.Shift:
                    KeyValue = '?';
                    break;
                case Keys.OemQuestion:
                case Keys.Divide:
                    KeyValue = '/';
                    break;
                default:
                    if ((0x60 <= (int)keyData) && (0x69 >= (int)keyData))
                    {
                        KeyValue = (char)((int)keyData - 0x30);
                    }
                    else
                    {
                        KeyValue = (char)keyData;
                    }
                    break;
            }
            return KeyValue;
        }

        public static void CheckCard()
        {
            //RestClient 
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://210.211.116.19:8888");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string card_info = client.GetStringAsync("api/ewallet/?card_id=" + SessionVariables.CardId).Result;
                    if (String.IsNullOrEmpty(card_info)) return;
                    card_info = card_info.Substring(1, card_info.Length - 2);
                    string[] values = card_info.Split('|');
                    SessionVariables.CardNumber = values[1];
                    SessionVariables.ProfileId = long.Parse("0" + values[2]);
                    SessionVariables.CardPrepaidAmount = long.Parse("0" + values[3]);
                    SessionVariables.IsActived = bool.Parse(values[4]);
                    SessionVariables.CardType = values[5];
                    SessionVariables.CardOwner = values[6];
                    //card_info.CardId,
                    //card_info.CardNumber,
                    //card_info.CustomerCIF,
                    //card_info.PrepaidAmount,
                    //card_info.IsActived,
                    //card_info.CardType1.TypeName);

                }
            }
            catch (Exception ex) { }
        }

        public static void RegisterWalletToCard()
        {
            //RestClient 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://210.211.116.19:8888");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string card_info = client.GetStringAsync("api/ewallet/?card_id=" + SessionVariables.CardId + "&full_name=" + SessionVariables.CardOwner + "&customer_cif=" + 
                SessionVariables.ProfileId).Result;
                //if (String.IsNullOrEmpty(card_info)) return;
                //card_info = card_info.Substring(1, card_info.Length - 2);
                //string[] values = card_info.Split('|');
                //SessionVariables.CardNumber = values[1];
                //SessionVariables.ProfileId = long.Parse("0" + values[2]);
                //SessionVariables.CardPrepaidAmount = long.Parse("0" + values[3]);
                //SessionVariables.IsActived = bool.Parse(values[4]);
                //SessionVariables.CardType = values[5];
                //card_info.CardId,
                //card_info.CardNumber,
                //card_info.CustomerCIF,
                //card_info.PrepaidAmount,
                //card_info.IsActived,
                //card_info.CardType1.TypeName);

            }
        }
        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri("http://210.211.116.19:8888");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/ewallet/1");
                if (response.IsSuccessStatusCode)
                {
                    string product = await response.Content.ReadAsStringAsync();
                    
                }
            }
        }
    }

    public class SessionVariables
    {
        public static string CardId,Email, CardNumber, CardOwner, CardValidDate, MobileNumber, CardType, Personal_id, Address, gduser;
        public static long ProfileId, CardPrepaidAmount;
        public static bool IsActived;
        public static dynamic FinanceAccount;
        public static ApplicationUser TellerUser;
        public static bool IsRegister;
    }
    public class ApplicationUser : IdentityUser
    {
        public string Status { get; set; }
    }
}
