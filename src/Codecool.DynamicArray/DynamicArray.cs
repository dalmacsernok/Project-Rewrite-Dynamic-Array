using System;
using System.Linq;

namespace Codecool.DynamicArray
{
    /// <summary>
    /// This class has methods with different forms of greetings.
    /// </summary>
    public class DynamicArray
    {
        public int[] Values { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicArray"/> class with default capacity.
        /// </summary>
        public DynamicArray()
        {
            Capacity = 4;
            Values = new int[Capacity];

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicArray"/> class.
        /// </summary>
        /// <param name="capacity">Starting capacity of array.</param>
        public DynamicArray(int capacity)
        {
            Capacity = capacity;
            Values = new int[Capacity];
        }

        /// <summary>
        /// Gets or sets capacity of array.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets the amount of already inserted elements.
        /// </summary>
        public int Size { get; private set; } = 0;

        /// <summary>
        /// Place a new value into the array.
        /// </summary>
        /// <param name="value">Integre value to be stored.</param>
        public void Add(int value)
        {
            Size++;
            if (Size > Capacity)
            {
                int[] newArray = new int[Capacity * 2];
                for (int i = 0; i < Values.Length; i++)
                {
                    newArray[i] = Values[i];

                }
                Values = newArray;
            }
            Values[Size - 1] = value;
        }

        /// <summary>
        /// Access element by index.
        /// </summary>
        /// <param name="index">Position of requested element.</param>
        /// <returns>Element stored at given index.</returns>
        public int Get(int index)
        {
            if (index >= Size)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                return Values[index];
            }
        }

        /// <summary>
        /// Remove an element at the given index.
        /// </summary>
        /// <param name="index">Index of removable element.</param>
        public void Remove(int index)
        {
            int[] newArray = new int[Capacity];
            for (int i = 0; i < Values.Length; i++)
            {
                if (i != index)
                {
                    newArray[i] = Values[i];
                }
            }
            Values = newArray;

            if (index >= Size || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            Size--;

        }

        /// <summary>
        /// Inserts a value at the given index.
        /// </summary>
        /// <param name="index">Index of newly inserted element.</param>
        /// <param name="value">Value of new element.</param>
        public void Insert(int index, int value)
        {
            Size++;
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (index >= Size - 1)
            {
                Capacity *= 2;
                int[] newArray = new int[Capacity];
                for (int i = 0; i < Values.Length; i++)
                {
                    newArray[i] = Values[i];
                }
                Values = newArray;
                Values[Size - 1] = value;
            }
            else
            {
                int[] newArray = new int[Capacity + 1];
                for (int i = 0; i <= Values.Length; i++)
                {
                    if (i < index)
                    {
                        newArray[i] = Values[i];
                    }
                    else if (i == index)
                    {
                        newArray[i] = value;
                    }
                    else if (i > index)
                    {
                        newArray[i] = Values[i - 1];
                    }
                }

                Values = newArray;
            }

        }

        /// <inheritdoc />
        public override string ToString()
        {
            bool isEmpty = true;
            foreach (int item in Values)
            {
                if (item == 0)
                {
                    isEmpty = true;
                }
                else
                {
                    isEmpty = false;
                    string result = "";
                    foreach (int i in Values)
                    {
                        result += $"{i}, ";
                    }

                    return $"[{result.Substring(0, result.Length - 5)}]";
                }
            }

            return $"[]";

        }
    }
}
