using System;

namespace PinyinToneMarker
{
    public class Pinyin
    {
        public static string MarkTone(string content, int tone)
        {
            char[] contentArray = content.ToLower().ToCharArray();
            GetFullFormat(contentArray, tone);
            return new String(contentArray);
        }

        private static readonly char[,] Vowels = new char[,]
        {
            { 'a', 'e', 'i', 'o', 'u', 'ü', 'm', 'n'},
            { 'ā', 'ē', 'ī', 'ō', 'ū', 'ǖ', 'm', 'n'},
            { 'á', 'é', 'í', 'ó', 'ú', 'ǘ', 'ḿ', 'ń'},
            { 'ǎ', 'ě', 'ǐ', 'ǒ', 'ǔ', 'ǚ', 'm', 'ň'},
            { 'à', 'è', 'ì', 'ò', 'ù', 'ǜ', 'm', 'ǹ'}
        };
        private enum VowelsIndex : int
        {
            A = 0, E, I, O, U, V
        }
        private enum SyllablesIndex : int
        {
           M = VowelsIndex.V + 1, N, J , Q , X
        }
        
        private static void GetFullFormat(char[] content, int tone)
        {
            if(tone < 1 || tone > 4) return;

            bool hasJQXY = false;
           
            int[] indexes = new int[15];
            for(int i = 0; i < indexes.Length; i++)
            {
                indexes[i] = -1;
            }
            for(int i = 0;i < content.Length; i++)
            {
                switch(content[i])
                {
                    case 'a': indexes[(int)VowelsIndex.A] = i; break;
                    case 'o': indexes[(int)VowelsIndex.O] = i; break;
                    case 'e': indexes[(int)VowelsIndex.E] = i; break;
                    case 'i': indexes[(int)VowelsIndex.I] = i; break;
                    case 'u': indexes[(int)VowelsIndex.U] = i; break;
                    case 'v': indexes[(int)VowelsIndex.V] = i; break;

                    case 'n': indexes[(int)SyllablesIndex.M] = i; break;
                    case 'm': indexes[(int)SyllablesIndex.N] = i; break;

                    case 'j': 
                    case 'q': 
                    case 'x':
                    case 'y': hasJQXY = true; break;
                }
            }
            int index = indexes[(int)VowelsIndex.V];
            if (index >= 0)
            {
                content[index] = hasJQXY ? 'u': 'ü';
            }


            if ((index = indexes[(int)VowelsIndex.A]) >= 0)
            { content[index] = Vowels[tone, (int)VowelsIndex.A]; return; }

            if ((index = indexes[(int)VowelsIndex.O]) >= 0)
            { content[index] = Vowels[tone, (int)VowelsIndex.O]; return; }

            if ((index = indexes[(int)VowelsIndex.E]) >= 0)
            { content[index] = Vowels[tone, (int)VowelsIndex.E]; return; }

            index = indexes[(int)VowelsIndex.I];

            int index2;
            if ((index2 = indexes[(int)VowelsIndex.U]) > index)
            { content[index2] = Vowels[tone, (int)VowelsIndex.U]; return; }

            if ((index2 = indexes[(int)VowelsIndex.V]) > index)
            { content[index2] = Vowels[tone, hasJQXY ? (int)VowelsIndex.U : (int)VowelsIndex.V]; return; }

            if (index >= 0)
            { content[index] = Vowels[tone, (int)VowelsIndex.I]; return; }
            
        }
    }
}
