using System;
using System.Xml.Linq;
using Internal;

namespace task_6
{
    public static class Task6
    {

        public static void Main(string[] args)
        {
            try
            {
                LinkedList<string> list = new LinkedList<string>();
                list.AddLast("Bye");
                list.AddLast("Welcome");
                list.AddLast("Hey");
                list.AddAt("Hello", 2);
                //dodaj swój kod testujący działanie metod
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }
        }

        /// <summary>
        /// Zaimplementuj metodę rozszerzającą klasę LinkedList<T>, która zwraca n-ty węzeł listy.
        /// Węzły są numerowane od 0 (podobnie jak indeksy tablicy).
        /// W przypadku podania numeru nieistniejącego węzła należy zwrócić null. 
        /// </summary>
        /// <param name="n">typ elementu listy</param>
        public static LinkedListNode<T> FindNode<T>(this LinkedList<T> list, int n)
        {
            LinkedListNode<T> current = list.First;
            for (int i = 0; i < n; i++)
            {
                if (current == null)
                {
                    return null;
                }
                current = current.Next;
            }
            return current;
        }


        /// <summary>
        /// Zdefiniuj metodę rozszerzającą wstawiającą element na n-tej pozycji listy.
        /// Jeśli numer pozycji jest większy od rozmiaru kolejki lub jest ujemny
        /// to lista pozostaje niezmieniona a funkcja powinna zwrócić false.
        /// Dodanie elementu potwierdzane jest przez funkcję wartością true. 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value">wartość wstawiana</param>
        /// <param name="n">numer pozycji o wartości od 0 do Count listy</param>
        /// <typeparam name="T">typ elementu listy</typeparam>
        /// <returns>true, gdy lista została zmodyfikoana, false, gdy wstawienie nie było możliwe</returns>
        /**
         * Uwaga!
         * LinkedListNode ma właściwości tylko get, ich wartości należy określić podczas tworzenia obiektu
         * np. new LinkedListNode<string> node = new {Value: "Adam", Next: null, Previous: node};
         */
        public static bool AddAt<T>(this LinkedList<T> list, T value, int n)
        {
            if (n < 0 || n > list.Count)
            {
                return false;
            }

            LinkedListNode<T> current = list.FindNode(n - 1);
            if (current == null)
            {
                list.AddFirst(value);
            }
            else
            {
                list.AddAfter(current, value);
            }
            return true;
        }

        /// <summary>
        /// Zaimplementuj metodę rozszerzeń, która usuwa węzeł na podanej pozycji n.
        /// Jeśli n jest ujemne lub większe bądź równe Count to zwróć false.
        /// Poprawne usunięcie elementu funkcja sygnalizuje wartości true. 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool RemoveAt<T>(this LinkedList<T> list, int n)
        {
            if (n < 0 || n >= list.Count)
            {
                return false;
            }

            LinkedListNode<T> current = list.FindNode(n);
            if (current == null)
            {
                return false;
            }
            list.Remove(current);
            return true;
        }

        /// <summary>
        ///Zaimplementuj metodę rozszerzeń, która zwraca wartość logiczną true, jeśli lista jest cykliczna.
        /// Lista cykliczna zawiera węzeł, który w polu Next zawiera referencję do umieszczonego już wcześniej liśćie węzła.
        /// Przeglądanie węzłów prowadzo do mieskończonej pętli.
        /// np.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool IsCyclic<T>(this LinkedList<T> list)
        {
            if (list.Count == 0) return false;

            var slow = list.First;
            var fast = list.First;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;

                if (slow == fast) return true;
            }

            return false;
        }

    }
}