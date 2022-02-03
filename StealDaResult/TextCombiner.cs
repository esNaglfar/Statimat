using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StealDaResult
{
    public static class TextCombiner
    {
        public static string ProcessTheText(string text)
        {
            int DatePos = text.IndexOf(StringConsts.DateString);
            int LotPos = text.IndexOf(StringConsts.DestrPower);
            int LinDencPos = text.IndexOf(StringConsts.LinearDencity);
            //Regex regex = new Regex();C:\Users\3xSou\source\repos\StealDaResult\StealDaResult\packages.config
            
            return $"|| Дата : {ParseNext(DatePos + StringConsts.DateString.Length, 25, text, true)} {System.Environment.NewLine}|| Разрывная нагрузка : {ParseNext(LotPos + StringConsts.DestrPower.Length, 25, text)} Н {System.Environment.NewLine}|| Линейная плотность : {ParseNext(LinDencPos + StringConsts.LinearDencity.Length, 25, text)} Текс {System.Environment.NewLine}";
        }

        private static string ParseNext(int count, int position, string text, bool date = false)
        { 
            try
            {
                if (!date)
                {
                    return text.Substring(count, position).Trim().Split(' ')[1];
                }
                else
                {
                    var str = text.Substring(count, position).Trim().Split('/');
                    return $"{str[1]}-{str[0]}-{str[2]}";
                }
            }
            catch
            {
                return "Data";
            }
        }
    }
}
