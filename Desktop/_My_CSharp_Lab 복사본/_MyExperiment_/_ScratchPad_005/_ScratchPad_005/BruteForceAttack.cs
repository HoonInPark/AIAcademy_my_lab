using System;
namespace _ScratchPad_005
{
	public class BruteForceAttack
	{
        public static string Decrypt(string ciphertext, string key)
        {
            string plaintext = "";

            for (int i = 0; i < ciphertext.Length; i++)
            {
                int c = (int)ciphertext[i] - 32;
                int k = (int)key[i % key.Length] - 32;
                int p = (c - k) % 94;
                if (p < 0)
                {
                    p += 94;
                }
                plaintext += (char)(p + 32);
            }

            return plaintext;
        }

        public static void Attack(string ciphertext)
        {
            string key = "";

            for (int i = 32; i <= 126; i++)
            {
                for (int j = 32; j <= 126; j++)
                {
                    for (int k = 32; k <= 126; k++)
                    {
                        for (int l = 32; l <= 126; l++)
                        {
                            key = ((char)i).ToString() + ((char)j).ToString() + ((char)k).ToString() + ((char)l).ToString();
                            string plaintext = Decrypt(ciphertext, key);
                            if (plaintext == "test")
                            {
                                Console.WriteLine("Key found: " + key);
                                Console.WriteLine("Plaintext: " + plaintext);
                                return;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Key not found.");
        }
    }
}