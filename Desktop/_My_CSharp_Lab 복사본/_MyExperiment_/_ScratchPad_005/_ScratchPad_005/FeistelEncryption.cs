using System;
namespace _ScratchPad_005
{
    class FeistelEncryption
    {
        static string RandKeyGenerator(int p) // p는 평문의 이진표현을 반으로 나눈 값.
        {
            Random randInt = new Random();
            string key = "";

            for (int i = 0; i < p; i++)
            {
                int temp = randInt.Next(0, 1 + 1);
                key = key + temp.ToString();
            }
            return key;
        }

        static string EXOR(string binPart_1, string binPart_2, int n) // n은 암호화하고자 하는 평문의 정보를 반으로 나눈 값
        {
            string temp = "";

            for (int i = 0; i < n; i++)
            {
                if (binPart_1[i] == binPart_2[i])
                {
                    temp += "0";
                }
                else
                {
                    temp += "1";
                }
            }

            return temp;
        }

        // 이진수를 문자열로 바꾸는 함수는 생략한다. 그냥 키워드로 실행시에 만들어 두면 될 거 같아서.

        static string AsciiMethod(string plainText)
        // 평문을 아스키의 이진표현들의 합으로 가공하는 함수
        {
            List<int> PT_Ascii = new List<int>(); // 평문의 아스키 변환값이 각 문자당 하나씩 들어갈 리스트이다.
            foreach (var eachStr in plainText)
            {
                PT_Ascii.Add((int)eachStr);
            }

            List<string> PT_Bin = new List<string>(); // 아스키로 변환한 문자열의 수 집합을 각각 8자리의 이진수로 변환해서 하나의 문자열로 저장.
            foreach (char c in PT_Ascii)
            {
                string binaryString = Convert.ToString(c, 2); 
                PT_Bin.Add(binaryString);
            }

            string PT_BinStr = "";
            foreach (string eachBin in PT_Bin)
            {
                PT_BinStr += eachBin;
            }

            return PT_BinStr;
        }

        public static string Feistel(string _plainText)
        {
            string _PT_BinStr = AsciiMethod(_plainText);
            int _n = _PT_BinStr.Length / 2;

            string L1 = _PT_BinStr.Substring(0, _n); // 평문의 이진표현을 반으로 나눈 값이 _n이다.
            string R1 = _PT_BinStr.Substring(_n);

            string K1 = RandKeyGenerator(_n); // 키의 길이는 _n
            string K2 = RandKeyGenerator(_n);

            // first round
            string f1 = EXOR(R1, K1, _n);
            string R2 = EXOR(f1, L1, _n);

            string L2 = R1;

            // second round
            string f2 = EXOR(R2, K2, _n);
            string R3 = EXOR(f2, L2, _n);

            string L3 = R2;

            string bin_Data = L3 + R3;
            if (bin_Data.Length % 7 != 0)
            {
                // 패딩(padding) 추가
                int padLength = 7 - bin_Data.Length % 7;
                for (int i = 0; i < padLength; i++)
                {
                    bin_Data += "0";
                }
            }

            string str_Data = "";
            for (int i = 0; i < bin_Data.Length; i += 7)
            {
                int length = Math.Min(7, bin_Data.Length - i);
                string temp_data = bin_Data.Substring(i, length);
                int decimal_data = Convert.ToInt32(temp_data, 2);
                str_Data += (char)decimal_data;
            }

            return str_Data;
        }
    }
}