using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Cryptography;
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
        public static string CMS_Gateway = String.Empty;
        public static void Init()
        {
            string coreServer = ConfigurationSettings.AppSettings["CoreDB_Server"];
            string coreDbString = ConfigurationSettings.AppSettings["CoreDB_Database"];
            CMS_Gateway = ConfigurationSettings.AppSettings["CMS_Gateway"];
            SessionVariables.CounterName = ConfigurationSettings.AppSettings["CounterName"];
            coreServer = Encrypt.DecryptString(coreServer, String.Empty);
            MongoClient client = new MongoClient(coreServer);
            MongoDatabase coreDb = client.GetServer().GetDatabase(coreDbString);
            UserManager = new UserManager<ApplicationUser>
                (
                new UserStore<ApplicationUser>(coreDb)
                );
            DataHelper = new eWallet.Data.MongoHelper(
               coreServer,coreDbString
                );
        }

        public static void AddLogCard(string action_code, string note, long start_balance, long end_balance, long amount)
        {
            string strRequest = "api/ewallet?card_id={0}&start_balance={1}&end_balance={2}&amount={3}&action_code={4}&action_by={5}&note={6}";
            strRequest = String.Format(strRequest,
                SessionVariables.CardId,
                start_balance,
                end_balance,
                amount,
                action_code,
                SessionVariables.TellerUser.UserName,
                note);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(CMS_Gateway);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string result = client.GetStringAsync(strRequest).Result;
                }
            }
            catch {  }

        }

        public static JArray GetStatement()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(CMS_Gateway);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    JArray card_info = JArray.Parse(client.GetStringAsync("api/ewallet/?card_id=" + SessionVariables.CardId + "&last_number=5").Result);
                    return card_info;
                }
            }
            catch { return null; }
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
           + "'}}";
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
            dynamic cashin = Helper.RequestToServer(request);
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

        public static dynamic CashOut(long amount)
        {
            string request = @"{system:'web_frontend', module:'transaction',type:'two_way', function:'CASHOUT',request:{channel:'WEB', profile:'"
                + SessionVariables.CardId + "',service:'GNCP', provider:'BANK',payment_provider:'GNCC',amount: " + amount +
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
                    client.BaseAddress = new Uri(CMS_Gateway);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string card_info = client.GetStringAsync("api/ewallet/?card_id=" + SessionVariables.CardId).Result;
                    if (String.IsNullOrEmpty(card_info)) return;
                    dynamic card = JObject.Parse(card_info);
                    //card_info = card_info.Substring(1, card_info.Length - 2);
                    //string[] values = card_info.Split('|');
                    SessionVariables.CardNumber = card.CardNumber;// Car values[1];
                    SessionVariables.ProfileId = long.Parse(card.CustomerCIF.ToString());// long.Parse("0" + values[2]);
                    SessionVariables.CardPrepaidAmount = card.PrepaidAmount;// long.Parse("0" + values[3]);
                    SessionVariables.IsActived = card.IsActived;// bool.Parse(values[4]);
                    SessionVariables.CardType = card.CardType;// values[5];
                    SessionVariables.CardOwner = card.CardOwnerName;// values[6];
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
                client.BaseAddress = new Uri(CMS_Gateway);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string card_info = client.GetStringAsync("api/ewallet/?card_id=" + SessionVariables.CardId + "&full_name=" + SessionVariables.CardOwner + "&customer_cif=" +
                SessionVariables.ProfileId).Result;
            }
        }

        public static void GetUserConfig(string card_track)
        {
            //RestClient 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CMS_Gateway);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string user_config = client.GetStringAsync("api/ewallet/?card_track=" + card_track).Result;
                if (String.IsNullOrEmpty(user_config))
                {
                    SessionVariables.UserConfig = null;
                    return;
                }
                SessionVariables.UserConfig = JObject.Parse(user_config);
            }
        }
        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri(CMS_Gateway);
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
        public static string CardId, Email, CardNumber, CardOwner, CardValidDate, MobileNumber, CardType, Personal_id, Address, gduser;
        public static long ProfileId, CardPrepaidAmount;
        public static dynamic UserConfig;
        public static bool IsActived;
        public static dynamic FinanceAccount;
        public static ApplicationUser TellerUser;
        public static string CounterName;
        public static bool IsRegister;
    }
    public class ApplicationUser : IdentityUser
    {
        public string Status { get; set; }
    }

    public static class Encrypt
    {
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "payflowpayid3103";
        private const string defaultPIN = "payidpayflow20142015";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        //Encrypt
        public static string EncryptString(string plainText, string passPhrase)
        {
            if (String.IsNullOrEmpty(passPhrase)) passPhrase = defaultPIN;
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        //Decrypt
        public static string DecryptString(string cipherText, string passPhrase)
        {
            if (String.IsNullOrEmpty(passPhrase)) passPhrase = defaultPIN;
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
