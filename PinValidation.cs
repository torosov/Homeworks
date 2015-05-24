using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinValidation
{
    class PinValidation
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            string gender = Console.ReadLine();
            string egn = Console.ReadLine();

            int[] egnDigits = new int[egn.Length];
            bool isEgnMadeOfDigits = true;
            for (int i = 0; i < egn.Length; i++)
            {
                int nextDigit;
                isEgnMadeOfDigits = int.TryParse(egn[i].ToString(), out nextDigit);
                if (!isEgnMadeOfDigits)
                {
                    break;
                }
                egnDigits[i] = nextDigit;
            }

            if (!isEgnMadeOfDigits)
            {
                Console.WriteLine("<h2>Incorrect data</h2>");
                return;
            }

            bool isLegthCorrect = egn.Length == 10;
            bool isGenderCorrect = (gender == "male" && egnDigits[8] % 2 == 0) || (gender == "female" && egnDigits[8] % 2 == 1);
            int year = int.Parse(egn.Substring(0, 2));
            int month = int.Parse(egn.Substring(2, 2));
            int day = int.Parse(egn.Substring(4, 2));

            if (month > 20 && month <= 32)
            {
                month -= 20;
                year = 1800 + year;
            }
            else if (month > 40 && month <= 52)
            {
                month -= 40;
                year = 2000 + year;
            }
            else
            {
                year = 1900 + year;
            }
            DateTime date = new DateTime();
            bool isDateCorrect = DateTime.TryParse(String.Format("{0}.{1}.{2}", day, month, year), out date);

            int checkSum = (egnDigits[0] * 2 + egnDigits[1] * 4 + egnDigits[2] * 8 
                            + egnDigits[3] * 5 + egnDigits[4] * 10 + egnDigits[5] * 9
                            + egnDigits[6] * 7 + egnDigits[7] * 3 + egnDigits[8] * 6) % 11;
            if (checkSum == 10)
            {
                checkSum = 0;
            }

            bool isCheckSumCorrect = (checkSum == egnDigits[9]);

            if (isLegthCorrect && isGenderCorrect && isDateCorrect && isCheckSumCorrect)
            {
                Console.WriteLine("{{\"name\":\"{0}\",\"gender\":\"{1}\",\"pin\":\"{2}\"}}", name, gender, egn);
            }
            else
            {
                Console.WriteLine("<h2>Incorrect data</h2>");
            }
        }
    }
}
