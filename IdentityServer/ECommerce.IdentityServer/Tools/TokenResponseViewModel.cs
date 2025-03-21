using System;

namespace ECommerce.IdentityServer.Tools
{
    public class TokenResponseViewModel
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }


        public TokenResponseViewModel(string token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }

        
    }
}
