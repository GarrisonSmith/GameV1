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
            StringBuilder text = new StringBuilder(foo);
            int height = 16;

            int lineStart = 0;
            int lastBreak = 0;
            int curBreak = 0;
            foo.Replace(Environment.NewLine, "");
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    lastBreak = curBreak;
                    curBreak = i;

                    if ((curBreak - lineStart) * 9 > area.Width)
                    {
                        if ((i - lineStart) * 9 > area.Width)
                        {
                            //split curBreak up somehow
                            System.Diagnostics.Debug.WriteLine("fasfa");

                            text = text.Remove(lastBreak, 1);
                            text = text.Insert(lastBreak, Environment.NewLine);

                            i += Environment.NewLine.Length;

                            lineStart = i;
                        }
                        else
                        {
                            text = text.Remove(lastBreak, 1);
                            text = text.Insert(lastBreak, Environment.NewLine);

                            i += Environment.NewLine.Length;
                            lineStart = i;
                        }
                    }
                }
            }

            /*
            for (int i = area.Width-1; i < foo.Length; i += (area.Width+Environment.NewLine.Length))
            {
                foo = foo.Insert(i, Environment.NewLine);
                height += 16;
            }
            */
            textFits = height <= area.Height;
            return text.ToString();
        }
    }
}
