namespace task_10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<(string, string, bool)> tests1 = new List<(string, string, bool)>()
            {
                ("abcdefg", "abcdefg", true),
                ("abcdefg", "*", true),
                ("abcdefg", "a??d", false),
                ("abcdefg", "?b*h", false),
                ("abcdefg", "???", false),
                ("abcdefg", "a*?", true),
                ("abcdefg", "a*b*c*d*e*f*g", true),
                ("aaaaaaa", "a??a", false),
            };
            int points1 = 0;
            foreach (var itemCase in tests1)
            {
                try
                {
                    points1 += Match(itemCase.Item1, itemCase.Item2) == itemCase.Item3 ? 1 : 0;
                }
                catch
                {

                }
            }
            Console.WriteLine($"Test passed: {points1}, Test failed: {tests1.Count - points1}");
            List<(string, string, int)> tests2 = new List<(string, string, int)>()
            {
                ("abcd efgh ijkl ab knbw  qwjh q kj ab q909qw 8jklk qwekj qle", "abc", 1),
                ("abcd abcd abcd efg jkij klkjjda abcd ajkabcdee", "abcd", 5),
                ("xyzx alj lkj 932wlkejf wlke wkej zxyzxyzx", "xyzx", 2),
                ("abcd ejfl aka klsf a alkjfsfl asda aa", "a", 7),
                ("aaa aaa a a aaa a aaa", "ba", 0),
                ("aaa aaa a a aaa a aaa", "aaa", 4),
                ("", "a", 0),
                ("asd a sd a", "asd", 1)
            };
            int points2 = 0;
            foreach (var itemCase in tests2)
            {
                try
                {
                    points2 += CountOccurrences(itemCase.Item1, itemCase.Item2) == itemCase.Item3 ? 1 : 0;
                }
                catch
                {

                }
            }
            Console.WriteLine($"Test passed: {points2}, Test failed: {tests2.Count - -points2}");
            Console.WriteLine($"Total points {(points1 + points2)} / {(tests1.Count + tests2.Count)}");
            Console.WriteLine($"Percent: {100 * (points1 + points2) / (double)(tests1.Count + tests2.Count)}");
        }

        /**
         * Zaimplementuj metodę zwracająca wartość logiczną, jeśli łańcuch input
         * pasuje do wzorca.
         * We wzorcu moga wystąpić znaki specjalne: * i ?
         * ? - jeden dowolny znak
         * * - zero lub dowolna liczba dowolnych znaków
         * Przykłady
         * input = "abcdefg", pattern = "abcdefg"   => true
         * input = "abcdefg", pattern = "*"         => true
         * input = "abcdefg", pattern = "a??d*"     => true
         * input = "abcdefg", pattern = "?b*h"      => false
         * input = "abcdefg", pattern = "???"       => false
         * input = "abcdefg", pattern = "a*?"       => true
         * input = "abcdefg", pattern = "a*b*c*d*e*f*g" => true
         */
        public static bool Match(string input, string pattern)
        {
            int i = 0;
            int j = 0;
            while (i < input.Length)
            {
                if (j < pattern.Length && (input[i] == pattern[j] || pattern[j] == '?'))
                {
                    i++;
                    j++;
                }
                else if (j < pattern.Length && pattern[j] == '*')
                {
                    int k = i;
                    int l = j;
                    while (k <= input.Length)
                    {
                        if (Match(input.Substring(k), pattern.Substring(l + 1)))
                            return true;
                        k++;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            while (j < pattern.Length && pattern[j] == '*')
            {
                j++;
            }
            return j == pattern.Length;
        }


        /**
         * Zaimplementuj metodę zwracającą liczbę wystąpień słowa w łańcuchu input.
         * Wystąpienie oznacza także zawieranie się szukanego słowa w słowach wejścia.
         * np. dla wejscia "abc" i szukanego słowa "a" jest jedno wystąpienie jako podłańcuch w "abc".
         * Wystąpienia nie mogą mieć wspólnych znaków np. dla wejścia "ababa" i słowa "aba" jest tylko jedno wystąpienie, choć
         * szukane słowo występuje dwa razy "abaxx" i "xxaba", ale oba wystąpienia korzystają ze wspólnego znaku "a".
         * Przykłady
         * input = "abc defg abc",          word = "abc"        => 2
         * input = "abcdefg",               word = "abc"        => 1
         * input = "a abc ahga",            word = "a"          => 4
         * input = "aba efgj ijk ababa"     word = "aba"        => 2
         * 
         */
        public static int CountOccurrences(string input, string word)
        {
            int count = 0;
            int index = input.IndexOf(word);
            while (index != -1)
            {
                count++;
                index = input.IndexOf(word, index + word.Length);
            }
            return count;
        }

    }
}