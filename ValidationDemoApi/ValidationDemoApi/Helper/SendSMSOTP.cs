using Unity;
using IO.ClickSend.ClickSend.Api;
using IO.ClickSend.ClickSend.Model;
using IO.ClickSend.Client;
using ValidationDemoApi.Models;
using System.Security.Claims;
using ValidationDemoApi.Entities;

namespace ValidationDemoApi.Helper
{
    public class SendSMSOTP
    {
        
        private readonly JWTokenDBContext _db;

        public SendSMSOTP(JWTokenDBContext db)
        {
            
            _db = db;
        }
        public Response SendSMS(string Contact)
        {
            Response response = new Response();
            try
            {
                //----you are check this code to uncomments this code--//
                //var configuration = new Configuration()
                //{
                //    Username = "preyansi.patel@archesoftronix.com",
                //    Password = "10F6284C-D1AE-5772-883B-1F5B881AD355",
                //};

                //var smsApi = new SMSApi(configuration);
                //var listOfSms = new List<SmsMessage>
                //    {
                //        new SmsMessage(
                //            to: Contact,
                //            body: "Welcome To Arche Softornix",
                //            source: "sdk",
                //            from:"+919725515517",
                //            customString : "hello",
                //            listId : 0,
                //            country : "India",
                //            fromEmail :"preyansi.patel@archesoftronix.com"
                //        )
                //    };

                //var smsCollection = new SmsMessageCollection(listOfSms);
                //var response1 = smsApi.SmsSendPost(smsCollection);
                //response.Data = response1;
//----you are check this code to uncomments this code--// 
                response.Data = response;
                response.Message = "Success";
                response.Status = true;
                response.Code = 200;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
                response.Code = 401;
            }

            return response;
        }
        //religion SendSMS
        //        public Response SendSMS(string Contact)
        //        {
        //            Response response1 = new Response();
        //            int otpValue = new Random().Next(100000, 999999);
        //            var status = "";
        //            try
        //            {
        //                string recipient = Contact;
        //                var APIKey = _configuration.GetSection("AppSettings:KeyTokenSMS").Value;


        //                string message = "Your OTP Number is " + otpValue + " ( Sent By : Technotips-Ashish )";
        //                String encodedMessage = HttpUtility.UrlEncode(message);

        //                using (var webClient = new WebClient())
        //                {
        //                    byte[] response = webClient.UploadValues("", new NameValueCollection(){

        //                                         {"apikey" , APIKey},
        //                                         {"numbers" , recipient},
        //                                         {"message" , encodedMessage},
        //                                         {"sender" , "TXTLCL"}});

        //                    string result = System.Text.Encoding.UTF8.GetString(response);

        //                    var jsonObject = JObject.Parse(result);

        //                    status = jsonObject["status"].ToString();

        //                    //        //SessionOptions["CurrentOTP"] = otpValue;

        //                    //    }

        //                    //    response1.Message = status;




        //                    //}
        //                    //catch (Exception e)
        //                    //{

        //                    //    throw (e);

        //                    //}

        //                    response1.Message = "succes";
        //            return response1;

        //        }

        public Response SendOTP(string Contact)
        {
            {
                Response response = new Response();
                try
                {
                    TblOtpmst tblOtpmst = new TblOtpmst();
                    int otpValue = new Random().Next(100000, 999999);
                    string message = "Your OTP Number is " + otpValue;
                    //----you are check this code to uncomments this code--// 
                    //var configuration = new Configuration()
                    //{
                    //    Username = "preyansi.patel@archesoftronix.com",
                    //    Password = "10F6284C-D1AE-5772-883B-1F5B881AD355",
                    //};

                    //var smsApi = new SMSApi(configuration);
                    //var listOfSms = new List<SmsMessage>
                    //{
                    //    new SmsMessage(
                    //        to: Contact,
                    //        body: message,
                    //        source: "sdk",
                    //        from:"+919725515517",
                    //        customString : "hello",
                    //        listId : 0,
                    //        country : "India",
                    //        fromEmail :"preyansi.patel@archesoftronix.com"
                    //    )
                    //};

                    //var smsCollection = new SmsMessageCollection(listOfSms);
                    //var response1 = smsApi.SmsSendPost(smsCollection);
//----you are check this code to uncomments this code--//
                    tblOtpmst.ContactNumber = Contact;
                    tblOtpmst.OneTimePassword = otpValue.ToString();
                    tblOtpmst.Otpcreated = DateTime.Now;
                    tblOtpmst.Otpexpires = DateTime.Now.AddMinutes(1);
                    _db.Add(tblOtpmst);
                    _db.SaveChanges();

                    response.Data = tblOtpmst;
                    response.Message = "Success";
                    response.Status = true;
                    response.Code = 200;
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    response.Data = ex;
                    response.Code = 401;
                }

                return response;
            }
        }
        public Response VerifyOTP(string otp)
        {
            Response response = new Response();
            TblOtpmst tblOtpmsts = _db.TblOtpmsts.Where(x=> x.OneTimePassword == otp).FirstOrDefault();
            if (tblOtpmsts != null)
            {
               if (DateTime.Now  <= tblOtpmsts.Otpexpires)
                {
                    response.Data = tblOtpmsts;
                    response.Message = " OTP Verified";
                    response.Status = true;
                    response.Code = 200;
                }
                else
                {
                    response.Message = "OTP Timedout";
                }
            }
            else
            {
                response.Data = tblOtpmsts;
                response.Message = "Success";
                response.Status = false;
                response.Message = "Please Enter Valid OTP";
            }
            return response;
        }
    }
}
