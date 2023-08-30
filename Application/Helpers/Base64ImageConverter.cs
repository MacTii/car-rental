using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class Base64ImageConverter
    {
        public static byte[] RemoveBase64Prefix(string base64String)
        {
            const string prefix = "data:image/png;base64,";
            if (base64String.StartsWith(prefix))
            {
                return Convert.FromBase64String(base64String[prefix.Length..]);
            }
            return Convert.FromBase64String(base64String);
        }

        public static string AddBase64Prefix(byte[] imageBytes)
        {
            return $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}
