using DomainLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Implement
{
    public class Addition : IAddition
    {
        public string SumOfTwoBigNumbers(string firstNumber, string secondNumber)
        {
            string result = string.Empty, resultFraction = string.Empty;
            int length1 = 0, length2 = 0, count = 0, sum = 0, carry = 0;

            string[] firstFraction = firstNumber.Split('.');
            string[] secondFraction = secondNumber.Split('.');

            string firstFractionNumber = firstFraction.Length > 1 ? firstFraction[1] : string.Empty;
            string secondFractionNumber = secondFraction.Length > 1 ? secondFraction[1] : string.Empty;
            //swap if secondFractionNumber is larger.  
            if (secondFractionNumber.Length > firstFractionNumber.Length)
            {
                string t = firstFractionNumber;
                firstFractionNumber = secondFractionNumber;
                secondFractionNumber = t;
            }
            //Calculate sum of Fraction part
            for (int i = firstFractionNumber.Length - 1; i >= 0; i--)
            {
                if (i <= secondFractionNumber.Length - 1)
                {
                    sum = ((int)(firstFractionNumber[i] - '0') +
                           (int)(secondFractionNumber[i] - '0') + carry);
                }
                else
                    sum = ((int)(firstFractionNumber[i] - '0') + carry);

                resultFraction += (char)(sum % 10 + '0');
                carry = sum / 10;
            }

            firstNumber = firstFraction[0];
            secondNumber = secondFraction[0];
            //swap if secondNumber is larger.
            if (secondNumber.Length > firstNumber.Length)
            {
                string t = firstNumber;
                firstNumber = secondNumber;
                secondNumber = t;
            }
            length1 = firstNumber.Length;
            length2 = secondNumber.Length;
            for (int i = length1 - 1; i >= 0; i--)
            {
                if (count <= secondNumber.Length - 1)
                {
                    length2--;
                    sum = ((int)(firstNumber[i] - '0') +
                        (int)(secondNumber[length2] - '0') + carry);
                }
                else
                    sum = ((int)(firstNumber[i] - '0') + carry);

                result += (char)(sum % 10 + '0');
                carry = sum / 10;
                count++;
            }

            // Add remaining carry  
            if (carry > 0)
                result += (char)(carry + '0');

            //reverse resul 
            char[] ch1 = result.ToCharArray();
            Array.Reverse(ch1);
            result = new string(ch1);

            //reverse resultFraction 
            char[] ch2 = resultFraction.ToCharArray();
            Array.Reverse(ch2);
            resultFraction = new string(ch2);
            if (string.IsNullOrEmpty(resultFraction))
                return result;
            else
                return result + "." + resultFraction;
        }
    }
}
