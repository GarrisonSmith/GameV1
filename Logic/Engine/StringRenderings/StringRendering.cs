using System;
using System.Text;
using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.StringRenderings
{
    //a character is 9 pixels wide 16 pixels tall.


    public static class StringRendering
    {
        public static string FormatString(string foo, Rectangle area, out bool textFits)
        {
            int height = 16;
            StringBuilder text = new StringBuilder();
            foo.Replace(Environment.NewLine, "");
            string[] parts = foo.Split(' ');

            int lastBreak = 0;
            foreach (string part in parts)
            {
                if (text.Length == 0)
                {
                    text.Append(part);
                }
                else if (((text.Length - lastBreak) + part.Length + 1) * 9 > area.Width)
                {
                    text.Append(Environment.NewLine); height += 16; lastBreak = text.Length - 1;
                    text.Append(part);
                }
                else
                {
                    text.Append(' ');
                    text.Append(part);
                }
            }

            textFits = height <= area.Height;
            return text.ToString();
        }
    }
}