using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog__ASP.NET_MVC_Practical_Project.Classes___Aditional
{
    public class Utils
    {
        public static string CutText(string text, int maxLenght = 100)
        {
            if (text == null||text.Length <= maxLenght)         
            {
                return text;
            }
            var shortText = text.Substring(0, maxLenght) + "...";
            return shortText;
        }
    }
}