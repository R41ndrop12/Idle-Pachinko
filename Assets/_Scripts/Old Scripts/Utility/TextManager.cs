using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utility
{
    static class TextManager
    {
        public static void displayValue(TextMeshProUGUI txt, double val)
        {
            txt.SetText("" + convertNum(val));
        }
        public static void displayValue(TextMeshProUGUI txt, string text)
        {
            txt.SetText(text);
        }

        public static void displayValue(TextMeshProUGUI txt, string preUnit, double val)
        {
            txt.SetText(preUnit + convertNum(val));
        }

        public static void displayValue(TextMeshProUGUI txt, double val, string postUnit)
        {
            txt.SetText(convertNum(val) + postUnit);
        }

        public static void displayValue(TextMeshProUGUI txt, string preUnit, double val, string postUnit)
        {
            txt.SetText(preUnit + convertNum(val) + postUnit);
        }

        static double roundValue(double val)
        {
            double roundedVal = Math.Round(val*100)/100;
            return roundedVal;
        }

        public static string convertNum(double val)
        {
            string[] suffixes = { "K", "M", "B", "T", "q", "Q", "s", "S", "O", "N" };
            string[] bigTens = { "D", "V", "T", "q", "Q", "s", "S", "O", "N", "C" };
            string[] bigOnes = { "", "U", "D", "T", "q", "Q", "s", "S", "O", "N"};
            int count = 0;
            val = Math.Round(val * 100f) / 100f;
            double num = Math.Round(val);
            count = (int) (Math.Log10(num));
            //Tens
            if (val < 100f)
            {
                return "" + val;
            }
            //Hundreds
            else if (val < 1000f)
            {
                return "" + (int) (val);
            }
            //Thousand
            else if(count < 33)
            {
                for (int i = 6; i < 36; i += 3)
                {
                    if (count < i)
                    {
                        return "" + (Math.Round(val / Math.Pow(10f, count - 2)) / Math.Pow(10f, 2 - count % 3)) + suffixes[(i - 6) / 3];
                    }
                }
            }
            else
            {
                for (int i = 33; i < 309; i += 3)
                {
                    if (count < i)
                    {
                        string end = bigOnes[(i - 4) % 30 / 3] + bigTens[((i - 3) / 30) - 1];
                        return "" + (Math.Round(val / Math.Pow(10f, count - 2)) / Math.Pow(10f, 2 - count % 3)) + end;
                    }
                }
            }
            //1234 / 10 = 123.4 / 100 = 1.23
            //10000 / 100 = Round(100.00) / 10 = 10.0
            //123456 / 1000 = 123.456 / 1 = 123
            return val + "";
        }
    }
}
