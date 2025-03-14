using POC.Common.Enums;
using System.Security.Cryptography;
using System.Text;

namespace POC.Services.Common.Helpers
{
    public class SecurityHelper
    {
        /// <summary>
        /// The security string
        /// </summary>
        private static readonly string SecurityString = "arch";

        /// <summary>
        /// Generates the hash.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>Hashed string</returns>
        public static string GenerateHash(params object[] tokens)
        {
            byte[] bytes = Encoding.Default.GetBytes(string.Join(SecurityString, tokens));
            SHA256 sha256 = SHA256.Create();
            using (var hashGenerator = sha256)
            {
                byte[] hashBytes = hashGenerator.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();

                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
        /// <summary>
        /// Validates the hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>true if hash is valid, false otherwise</returns>
        public static bool ValidateHash(string hash, params object[] tokens)
        {
            return string.Equals(hash, GenerateHash(tokens), StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Generates the queue token.
        /// </summary>
        /// <param name="objectID">The unique identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Queue security token</returns>
        public static string GenerateObjectToken(Guid objectID, string userName)
        {
            return GenerateHash(objectID, userName, "object-token");
        }

        /// <summary>
        /// Validates the queue token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="objectID">The queue identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>true if token valid, false otherwise</returns>
        public static bool ValidateObjectToken(string token, Guid objectID, string userName)
        {
            return ValidateHash(token, objectID, userName, "object-token");
        }

        /// <summary>
        /// Performs the modulus 10 check against card number.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns>true if the number is valid, false otherwise.</returns>
        public static bool PerformModulus10Check(string cardNumber)
        {
            if (cardNumber == null || cardNumber.Length != 16)
            {
                return false;
            }

            // make sure the number contains only digits
            if (!cardNumber.All(x => x >= '0' && x <= '9'))
            {
                return false;
            }

            int sum = 0;
            int len = cardNumber.Length;

            for (int i = 0; i < len; i++)
            {
                int add = (cardNumber[i] - '0') * (2 - ((i + len) % 2));
                add -= add > 9 ? 9 : 0;
                sum += add;
            }

            return sum % 10 == 0;
        }

        /// <summary>
        /// Masks the card number.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="numberOfDigitsToShow">The number of digits to show.</param>
        /// <returns>Masked bank card number</returns>
        public static string MaskCardNumber(string cardNumber, int numberOfDigitsToShow)
        {
            if (numberOfDigitsToShow <= 0)
            {
                return cardNumber;
            }

            if (string.IsNullOrEmpty(cardNumber))
            {
                return cardNumber;
            }

            int len = cardNumber.Length;
            if (numberOfDigitsToShow >= len)
            {
                return cardNumber;
            }

            int maskLen = len - numberOfDigitsToShow;

            string result = string.Empty;
            while (maskLen-- > 0)
            {
                result += Consts.MaskCharacter;
            }

            result += cardNumber.Substring(cardNumber.Length - numberOfDigitsToShow);
            return result;
        }

        /// <summary>
        /// Masks the card number.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="numberOfDigitsToShow">The number of digits to show.</param>
        /// <returns>
        /// Visible part of card number
        /// </returns>
        /// <remarks>
        /// To handle numeric fields we have to truncate the value to show only visible numbers and then (client side) add mask characters.
        /// </remarks>
        public static decimal? MaskCardNumber(decimal? cardNumber, int numberOfDigitsToShow)
        {
            if (!cardNumber.HasValue || numberOfDigitsToShow <= 0)
            {
                return cardNumber;
            }

            string value = cardNumber.ToString();
            int len = value.Length;

            if (len <= numberOfDigitsToShow)
            {
                return cardNumber;
            }

            // get required number of digits and return 
            string result = value.Substring(len - numberOfDigitsToShow);
            return Convert.ToDecimal(result);
        }

        /// <summary>
        /// Performs the modulus 11 check.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="weights">The weights.</param>
        /// <returns>
        /// true if number is valid, false otherwise
        /// </returns>
        public static bool PerformModulus11Check(string cardNumber, int[] weights)
        {
            if (string.IsNullOrEmpty(cardNumber) || weights == null || weights.Length == 0)
            {
                return false;
            }

            // ensure card number and weights have correct lengths
            if (cardNumber.Length != weights.Length + 1)
            {
                return false;
            }

            int sum = 0;
            if (cardNumber[0] == 'K')
            {
                sum = weights[0] + (weights[1] * 5);
            }
            else if (cardNumber[0] == 'H')
            {
                sum = (weights[0] * 8) + (weights[1] * 6);
            }
            else
            {
                return false;
            }

            for (int i = 2; i < weights.Length; i++)
            {
                char c = cardNumber[i];
                if (!char.IsDigit(c))
                {
                    return false;
                }

                sum += int.Parse(c.ToString()) * weights[i];
            }

            return Convert.ToInt32(cardNumber[cardNumber.Length - 1].ToString(), 16) == (sum % 11);
        }

        /// <summary>
        /// Performs the modulus subtract check.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="weights">The weights.</param>
        /// <returns>
        /// true if number is valid, false otherwise
        /// </returns>
        public static bool PerformModulus11SubtractCheck(string cardNumber, int[] weights)
        {
            if (string.IsNullOrEmpty(cardNumber) || weights == null || weights.Length == 0)
            {
                return false;
            }

            // ensure card number and weights have correct lengths
            if (cardNumber.Length != weights.Length + 1)
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                char c = cardNumber[i];
                if (!char.IsDigit(c))
                {
                    return false;
                }

                sum += int.Parse(c.ToString()) * weights[i];
            }

            int mod = 11 - (sum % 11);
            if (mod > 9)
            {
                mod -= 10;
            }

            return Convert.ToInt32(cardNumber[cardNumber.Length - 1].ToString(), 16) == mod;
        }
    }
}

