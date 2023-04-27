using AutoMapper;
using IO.ClickSend.ClickSend.Api;
using IO.ClickSend.ClickSend.Model;
using IO.ClickSend.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ValidationDemoApi.Entities;
using ValidationDemoApi.Models;


namespace ValidationDemoApi.Helper
{
    public class Validate
    {

        /// <summary>
        ///  validate Email abc=false
        ///  abc@gmail.com = true
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool Email(string email)
        {
            bool isValidEmail = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(email))
                {
                    Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                    isValidEmail = regex.IsMatch(email);
                    if (isValidEmail)
                    {
                        isValidEmail = true;
                    }
                }
            }
            catch (ArgumentException)
            {
                isValidEmail = false;
            }

            return isValidEmail;
        }


        /// <summary>
        ///   validate PinCode 123 232 = true
        ///  223888743 = false
        /// </summary>
        /// <param name="PinCode"></param>
        /// <returns></returns>
        public bool PinCode(int? PinCode)
        {
            bool isPinCode = false;
            if (!string.IsNullOrWhiteSpace(PinCode.ToString()))
            {
                Regex regex = new Regex("^[1-9]{1}[0-9]{2}\\s{0,1}[0-9]{3}$");
                if (regex.IsMatch(PinCode.ToString()))
                {
                    isPinCode = true;
                }
            }
            return isPinCode;
        }


        /// <summary>
        ///www.link.com = false //validate LinkedIn https://www.linkedin.com/in/brajesh-singh-318677177 = true
        ///www.link.com = false
        /// </summary>
        /// <param name="LinkedInEmail"></param>
        /// <returns></returns>
        public bool LinkedIn(string LinkedInEmail)
        {
            bool isValidLinkedInEmail = false;

            if (!string.IsNullOrWhiteSpace(LinkedInEmail))
            {
                Regex regex = new Regex(@"(http(s?)://|[a-zA-Z0-9\-]+\.|[linkedin])[linkedin/~\-]+\.[a-zA-Z0-9/~\-_,&=\?\.;]+[^\.,\s<]");
                isValidLinkedInEmail = regex.IsMatch(LinkedInEmail);
                if (isValidLinkedInEmail)
                {
                    isValidLinkedInEmail = true;
                }
            }
            return isValidLinkedInEmail;
        }

        //Website validation
        public bool Website(string Website)
        {
            bool isValidWebsite = false;

            if (!string.IsNullOrWhiteSpace(Website))
            {
                Regex regex = new Regex(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)");
                isValidWebsite = regex.IsMatch(Website);
                if (isValidWebsite)
                {
                    isValidWebsite = true;
                }
            }
            return isValidWebsite;
        }




    }
}




















