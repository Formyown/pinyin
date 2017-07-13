using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PinyinToneMarker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("输入拼音和声调(多组以空格分割,轻声不标示),Ctrl+C退出");
            Console.Out.WriteLine("例如: > han4 yu3 pin1 yin1");

            while (true)
            {
                Console.Out.Write("> ");
                string[] items = Console.ReadLine().Split(new char[] { ' '});
                for(int i = 0; i < items.Length; ++i)
                {
                    string content = items[i].Trim();
                    if (string.IsNullOrEmpty(content)) continue;
                    string result;
                    if (char.IsDigit(content[content.Length - 1]))
                    {
                        result = Pinyin.MarkTone(content.Substring(0, content.Length - 1), int.Parse(content[content.Length - 1].ToString()));
                    }
                    else
                    {
                        result = Pinyin.MarkTone(content, 0);
                    }
                    Console.Out.WriteLine("{0}.{1}", i, result);
                }

            }
           
        }
    }
}
