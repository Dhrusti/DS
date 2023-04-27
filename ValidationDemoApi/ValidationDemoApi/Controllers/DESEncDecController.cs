using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ValidationDemoApi.Helper;

namespace ValidationDemoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class DESEncDecController : ControllerBase
    {
        private readonly DESEncDec _desEncDec;

        public DESEncDecController(DESEncDec desEncDec)
        {
           _desEncDec = desEncDec;
          
        }

         
        [HttpGet]
        [Route("Encrypt")]
        public string Encrypt(string text)
        {

            bool cryptographyMode = true;
            //byte[] encryptString = DESEncDec.EncryptDecrypt(text);
         string encryptString = DESEncDec.Encrypt(text);
            
            return encryptString;
        }

        [HttpGet]
        [Route("Decrypt")]
        public string DecryptUsingCBC(string text)
        {
            
           string decryptString = DESEncDec.Decrypt(text); 
           //byte[] res = Encoding.UTF8.GetBytes(decryptString);
            return decryptString;
        }
    }
}

