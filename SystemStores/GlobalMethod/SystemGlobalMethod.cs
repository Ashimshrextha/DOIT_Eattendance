using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using SystemStores.GlobalModels;
using SystemStores.PairModels;
using static SystemStores.ENUMData.EnumGlobal;

namespace SystemStores.GlobalMethod
{
    public class SystemGlobalMethod
    {
        public static PaginationModel Get_PaginationValue(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            return new PaginationModel
            {
                PageNumber = pageNumber.HasValue && pageNumber > 0 ? int.Parse(pageNumber.ToString()) : (int)Pagination.PageNumber,
                PageSize = pageSize.HasValue && pageSize > 0 ? int.Parse(pageSize.ToString()) : (int)Pagination.PageSize,
                OrderingBy = string.IsNullOrEmpty(orderingBy) ? Pagination.Id.ToString() : orderingBy,
                OrderingDirection = string.IsNullOrEmpty(orderingDirection) ? Pagination.DESC.ToString() : orderingDirection
            };
        }

        public static string NepaliToEnglishMonth(NepaliMonths nepaliMonths)
        {
            switch (nepaliMonths)
            {
                case NepaliMonths.बैशाख:
                    return "April – May";

                case NepaliMonths.जेठ:
                    return "May – June";

                case NepaliMonths.असार:
                    return "June – July";

                case NepaliMonths.श्रावण:
                    return "July – August";

                case NepaliMonths.भदौ:
                    return "August – September";

                case NepaliMonths.आश्विन:
                    return "September – October";

                case NepaliMonths.कार्तिक:
                    return "October – November";

                case NepaliMonths.मंसिर:
                    return "November – December";

                case NepaliMonths.पुष:
                    return "December – January";

                case NepaliMonths.माघ:
                    return "January – February";

                case NepaliMonths.फाल्गुन:
                    return "February – March";

                case NepaliMonths.चैत्र:
                    return "March – April";

                default:
                    return string.Empty;
            }
        }

        public static string Encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        private static byte[] GenerateSalt(int saltSize)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[saltSize];
            rng.GetBytes(buff);
            return buff;
        }

        //CODE: Generate HASH Using SHA512
        public static string Get_HASH_SHA512(string password, string username, int saltSize)
        {
            byte[] salt = GenerateSalt(saltSize);
            try
            {
                //required NameSpace: using System.Text;
                //Plain Text in Byte
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(password + username);

                //Plain Text + SALT Key in Byte
                byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
                }

                HashAlgorithm hash = new SHA512Managed();
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
                byte[] hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashWithSaltBytes[i] = hashBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    hashWithSaltBytes[hashBytes.Length + i] = salt[i];
                }

                return Convert.ToBase64String(hashWithSaltBytes);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public static string EncryptDataDES(string strData, string strKey)
        {
            byte[] key = { }; //Encryption Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray;

            try
            {
                key = Encoding.UTF8.GetBytes(strKey);
                // DESCryptoServiceProvider is a cryptography class defind in c#.  
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(strData);
                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();
                return Convert.ToBase64String(Objmst.ToArray());//encrypted string  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DecryptData(string strData, string strKey)
        {
            byte[] key = { };// Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray = new byte[strData.Length];

            try
            {
                key = Encoding.UTF8.GetBytes(strKey);
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strData);

                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(Objmst.ToArray());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static string LocalIP()
        {
            try
            {
                string localIP;
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIP = endPoint.Address.ToString();
                }
                return localIP;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.InnerException.Message);
            }
        }

        public static string RemoteTerminalId
        {
            get
            {
                string szRemoteAddr = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                string szXForwardedFor = System.Web.HttpContext.Current.Request.ServerVariables["X_FORWARDED_FOR"];
                string cfIP = System.Web.HttpContext.Current.Request.Headers["CF-CONNECTING-IP"];
                string szIP = "";
                if (cfIP != null)
                {
                    szIP = cfIP.Trim().ToString();
                }
                else if (szRemoteAddr != null && szRemoteAddr != "::1")
                {
                    szIP = szRemoteAddr.Trim();
                }
                else if (szXForwardedFor != null)
                {
                    string szIPx = szXForwardedFor;
                    if (szIPx.IndexOf(",") > 0)
                    {
                        string[] arIPs = szIP.Split(',');
                        foreach (string item in arIPs)
                        {
                            szIP += item.Trim();
                        }
                    }
                }
                else szIP = LocalIP();
                return szIP;
            }
        }

        public static KeyValueIdentityPair<Guid, string, string> GetLoginFormat(string userName, string password)
        {
            try
            {
                string md5 = Encryption(password);
                string sha1 = Get_HASH_SHA512(password, userName, 256);
                Guid id = Guid.NewGuid();
                return new KeyValueIdentityPair<Guid, string, string> { Key = id, Value = md5, Identity = sha1 };
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        //return message
        public static string GetErrorMessage(UserErrorMessage message)
        {
            switch (message)
            {
                case UserErrorMessage.SAMEYEAR:
                    return "बिदा बैशाख-चैत्र सम्म मात्र";

                case UserErrorMessage.EXISTLeave:
                    return "तपाइले यो मितिमा बिदा अथवा काज लिनुभएको छ";

                case UserErrorMessage.FromToGreaterThanToDate:
                    return "शुरुको मिती अन्तिम मिती भन्दा सानो तथा बराबर हुनुपर्छ ";

                case UserErrorMessage.USERNAMEEMPTY:
                    return "कृपया सही लग - इन आईडी लेख्नुहोस";

                case UserErrorMessage.USERNAMEEXIST:
                    return "लग - इन आईडी प्रयोगमा छ !";

                case UserErrorMessage.PISNUMBEREMPTY:
                    return "कृपया कर्मचारी संकेत नं. लेख्नुहोस";

                case UserErrorMessage.PISNUMBEREXIST:
                    return "कर्मचारी संकेत नं. प्रयोगमा छ  !";

                case UserErrorMessage.MOBILEEXIST:
                    return "कर्मचारी मोबाइल नं. प्रयोगमा छ  !";

                case UserErrorMessage.NATIONALIDENTITYEMPTY:
                    return "कृपया पहिचान नं. लेख्नुहोस";

                case UserErrorMessage.NATIONALIDENTITYEXIST:
                    return " पहिचान नं. प्रयोगमा छ  !";

                case UserErrorMessage.IDENROLLEMPTY:
                    return "कृपया उपकरणको आईडी लेख्नुहोस";

                case UserErrorMessage.IDENROLLEXIST:
                    return "उपकरणको आईडी प्रयोगमा छ  !";

                case UserErrorMessage.IMAGESIZE:
                    return "कृपया फोटो 1 MB भन्दा बढी  अपलोड नगर्नुहोला ";

                default:
                    return "कृपया फारम जाँच गर्नुहोस्";
            }
        }

        public static string GetButtonText(SubmitButtonText text)
        {
            switch (text)
            {
                case SubmitButtonText.Create:
                    return "नयाँ थप्नुहोस";
                case SubmitButtonText.Update:
                    return "सम्पादन";
                case SubmitButtonText.Export:
                    return "निर्यात";
                case SubmitButtonText.Print:
                    return "छाप्नुहोस्";
                case SubmitButtonText.Submit:
                    return "पेश गर्नुहोस्";
                case SubmitButtonText.View:
                    return "हेर्नुहोस्";
                case SubmitButtonText.Delete:
                    return "हटाउनुहोस्";
                case SubmitButtonText.Upload:
                    return "अपलोड गर्नुहोस्";
                case SubmitButtonText.Search:
                    return "खोज्नुहोस्";
                case SubmitButtonText.Cancel:
                    return "रद्द गर्नुहोस्";
                default:
                    return string.Empty;
            }
        }

        public static bool ValidateImageFileType(string fileType)
        {
            switch (fileType)
            {
                case "image/jpeg":
                    return true;
                case "image/jpg":
                    return true;
                case "image/png":
                    return true;
                default:
                    return false;
            }
        }

        public static bool ValidateDocumentType(string fileType)
        {
            switch (fileType)
            {
                case "image/jpeg":
                    return true;
                case "image/jpg":
                    return true;
                case "image/png":
                    return true;
                case "image/pdf":
                    return true;
                default:
                    return false;
            }
        }
    }
}
