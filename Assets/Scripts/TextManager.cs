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
            txt.SetText("" + convertNum(roundValue(val)));
        }
        public static void displayValue(TextMeshProUGUI txt, string text)
        {
            txt.SetText(text);
        }

        public static void displayValue(TextMeshProUGUI txt, string preUnit, float val)
        {
            txt.SetText(preUnit + convertNum(roundValue(val)));
        }

        public static void displayValue(TextMeshProUGUI txt, float val, string postUnit)
        {
            txt.SetText(convertNum(roundValue(val)) + postUnit);
        }

        public static void displayValue(TextMeshProUGUI txt, string preUnit, float val, string postUnit)
        {
            txt.SetText(preUnit + convertNum(roundValue(val)) + postUnit);
        }

        static float roundValue(float val)
        {
            float roundedVal = Mathf.Round(val*100)/100;
            return roundedVal;
        }

        static string convertNum(float val)
        {
            int count = 0;
            int num = (int) val;
            while (num != 0)
            {
                num = num / 10;
                ++count;
            }
            //Tens
            if (val < 100)
            {
                return "" + val;
            }
            //Hundreds
            else if (val < 1000)
            {
                return "" + Mathf.Round(val);
            }
            //Thousand
            else if (val < 1000000)
            {
                return "" + Mathf.Round(val*Mathf.Pow(10,6 - count) / 1000f) / Mathf.Pow(10, 6 - count) + "K";
            }
            //Million
            else if (val < 1000000000)
            {
                return "" + Mathf.Round(val * Mathf.Pow(10, 9 - count) / 1000000f) / Mathf.Pow(10, 9 - count) + "M";
            }
            //Billion
            else if (val < 1000000000000)
            {
                return "" + Mathf.Round(val * Mathf.Pow(10, 12 - count) / 1000000f) / Mathf.Pow(10, 12 - count) + "B";
            }
            return "" + val;
        }
    }
}
