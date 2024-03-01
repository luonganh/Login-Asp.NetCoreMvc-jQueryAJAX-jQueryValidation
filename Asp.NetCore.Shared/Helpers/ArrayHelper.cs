namespace Asp.NetCore.Shared.Helpers
{
    public class ArrayHelper
    {
        /// <summary>
        /// Checks if the given array or collection is null or has no elements.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool HasLength(ICollection collection)
        {
            return !((collection == null) || (collection.Count == 0));
        }

        /// <summary>
        /// Tests equality of two single-dimensional arrays by checking each element
        /// for equality.
        /// </summary>
        /// <param name="a">The first array to be checked.</param>
        /// <param name="b">The second array to be checked.</param>
        /// <returns>True if arrays are the same, false otherwise.</returns>
        public static bool AreEqual(Array a, Array b)
        {
            if (a == null && b == null)
            {
                return true;
            }

            if (a != null && b != null)
            {
                if (a.Length == b.Length)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        object elemA = a.GetValue(i);
                        object elemB = b.GetValue(i);

                        if (elemA is Array && elemB is Array)
                        {
                            if (!AreEqual(elemA as Array, elemB as Array))
                            {
                                return false;
                            }
                        }
                        else if (!Equals(elemA, elemB))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns hash code for an array that is generated based on the elements.
        /// </summary>
        /// <remarks>
        /// Hash code returned by this method is guaranteed to be the same for
        /// arrays with equal elements.
        /// </remarks>
        /// <param name="array">
        /// Array to calculate hash code for.
        /// </param>
        /// <returns>
        /// A hash code for the specified array.
        /// </returns>
        public static int GetHashCode(Array array)
        {
            int hashCode = 0;

            if (array != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    object el = array.GetValue(i);
                    if (el != null)
                    {
                        if (el is Array)
                        {
                            hashCode += 17 * GetHashCode(el as Array);
                        }
                        else
                        {
                            hashCode += 13 * el.GetHashCode();
                        }
                    }
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Returns string representation of an array.
        /// </summary>
        /// <param name="array">
        /// Array to return as a string.
        /// </param>
        /// <returns>
        /// String representation of the specified <paramref name="array"/>.
        /// </returns>
        public static string ToString(Array array)
        {
            if (array == null)
            {
                return "null";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append('{');

            for (int i = 0; i < array.Length; i++)
            {
                object val = array.GetValue(i);
                sb.Append(val == null ? "null" : val.ToString());

                if (i < array.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append('}');

            return sb.ToString();
        }
    }
}