using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utility
{
    static class TextManager
    {
        public static void displayValue(TextMeshProUGUI txt, float val)
        {
            txt.SetText("" + convertNum(val));
        }
        public static void displayValue(TextMeshProUGUI txt, string text)
        {
            txt.SetText(text);
        }

        public static void displayValue(TextMeshProUGUI txt, string preUnit, float val)
        {
            txt.SetText(preUnit + convertNum(val));
        }

        public static void displayValue(TextMeshProUGUI txt, float val, string postUnit)
        {
            txt.SetText(convertNum(val) + postUnit);
        }

        public static void displayValue(TextMeshProUGUI txt, string preUnit, float val, string postUnit)
        {
            txt.SetText(preUnit + convertNum(val) + postUnit);
        }

        static float roundValue(float val)
        {
            float roundedVal = Mathf.Round(val*100)/100;
            return roundedVal;
        }

        public static string convertNum(float val)
        {
            string[] suffixes = {"K", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "D"};
            int count = 0;
            val = Mathf.Round(val * 100f) / 100f;
            float num = Mathf.Round(val);
            count = (int) (Mathf.Log10(num)) + 1;
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
            else
            {
                for (int i = 6; i < 39; i += 3)
                {
                    if (val < Mathf.Pow(10, i))
                    {
                        return "" + (Mathf.Round(val / Mathf.Pow(10f, i - 3) * Mathf.Pow(10f, i - count)) / Mathf.Pow(10f, i - count)) + suffixes[(i - 6) / 3];
                    }
                }
            }
            return "THE END";
        }
    }
}
