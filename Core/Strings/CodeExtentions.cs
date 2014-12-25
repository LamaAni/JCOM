using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCOM.Core.Strings
{
    public static class CodeExtentions
    {
        static string
            numbers = "0123456789",
            letters = "abcdefghijklmnopqrstvwxyz",
            lettersUp = letters.ToUpper(),
            codeAll = numbers + letters + lettersUp;

        static Random m_rand = new Random();

        /// <summary>
        /// Creates a randome code that starts with the prefex.
        /// </summary>
        /// <param name="prefex"></param>
        /// <param name="size">The size of the code to add to the prefex</param>
        /// <returns></returns>
        public static string GenerateCode(this string prefex, int size)
        {
            return prefex.GenerateCode(size, CodeGeneratorType.All);
        }
        /// <summary>
        /// Creates a randome code that starts with the prefex.
        /// </summary>
        /// <param name="prefex"></param>
        /// <param name="size">The size of the code to add to the prefex</param>
        /// <returns></returns>
        public static string GenerateCode(this string prefex, int size, CodeGeneratorType type)
        {
            string source;

            if (type == CodeGeneratorType.All)
            {
                source = codeAll;
            }
            else
            {
                StringBuilder sourceBuilder = new StringBuilder();
                if ((type & CodeGeneratorType.Letters) == CodeGeneratorType.Numbers)
                    sourceBuilder.Append(numbers);
                if ((type & CodeGeneratorType.Letters) == CodeGeneratorType.Letters)
                    sourceBuilder.Append(letters);
                if ((type & CodeGeneratorType.Letters) == CodeGeneratorType.LettersUpperCase)
                    sourceBuilder.Append(lettersUp);

                source = sourceBuilder.ToString();
            }

            return prefex.GenerateCode(size, source);
        }
        /// <summary>
        /// Creates a randome code that starts with the prefex.
        /// </summary>
        /// <param name="prefex"></param>
        /// <param name="size">The size of the code to add to the prefex</param>
        /// <returns></returns>
        public static string GenerateCode(this string prefex, int size, string source)
        {
            StringBuilder code = new StringBuilder();
            code.Append(prefex);
            int maxIndex = source.Length - 1;
            for (int i = 0; i < size; i++)
            {

                code.Append(source[Convert.ToInt32(Math.Round(m_rand.NextDouble() * maxIndex))]);
            }

            return code.ToString();
        }

        public enum CodeGeneratorType { Numbers = 1, Letters = 2, LettersUpperCase = 4, All = 16 };
    }
}
