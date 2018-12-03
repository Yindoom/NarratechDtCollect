using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DtCollect.Core.Helpers
{
    public class JwtSecurityKey
    {
        private static byte[] secretBytes = Encoding.UTF8.GetBytes("A secret for HmacSha256");

        public static SecurityKey Key 
        {
            get { return new SymmetricSecurityKey(secretBytes); } 
        } 

        public static void SetSecret(string secret)
        {
            secretBytes = Encoding.UTF8.GetBytes(secret);
        }  
    }
}