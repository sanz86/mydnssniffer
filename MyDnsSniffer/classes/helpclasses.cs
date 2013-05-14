using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDnsSniffer.classes
{
    class zippin
    {
        static IEnumerable<KeyValuePair<T, U>> Zip<T, U>(IEnumerable<T> first, IEnumerable<U> second)
        {
            IEnumerator<T> firstEnumerator = first.GetEnumerator();
            IEnumerator<U> secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext())
            {
                if (secondEnumerator.MoveNext())
                {
                    yield return new KeyValuePair<T, U>(firstEnumerator.Current, secondEnumerator.Current);
                }
                else
                {
                    yield return new KeyValuePair<T, U>(firstEnumerator.Current, default(U));
                }
            }
            while (secondEnumerator.MoveNext())
            {
                yield return new KeyValuePair<T, U>(default(T), secondEnumerator.Current);
            }
        }

    }
}
