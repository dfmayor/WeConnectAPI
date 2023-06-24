using System.Text;

namespace WeConnectApi.Utilities
{
    public class GenerateUserCode
    {
        public static string GetCode()
        {
            const string numbers = "0123456789";
            const string letters = "ABCDEFGHIJKLMNPQRSTUVWXYZ";
            var random = new Random();
        
            var code = new StringBuilder();
            for (int i = 0; i < 1; i++)
            {
                code.Append(letters[random.Next(letters.Length)]);
            }
        
            for (int i = 0; i < 7; i++)
            {
                code.Append(numbers[random.Next(numbers.Length)]);
            }
        
            return code.ToString();
        }
    }
}