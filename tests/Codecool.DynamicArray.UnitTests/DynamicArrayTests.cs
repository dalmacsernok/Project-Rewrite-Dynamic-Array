using NUnit.Framework;
using System;

namespace Codecool.DynamicArray.UnitTests
{
    [TestFixture()]
    public class DynamicArrayTests
    {
        private DynamicArray _arrayUnderTest;

        [SetUp]
        public void Setup()
        {
            _arrayUnderTest = new DynamicArray();
        }

        [Test]
        public void Constructor_TakesOneInteger_SetCapacity()
        {
            const int initialSize = 15;

            _arrayUnderTest = new DynamicArray(initialSize);

            Assert.AreEqual(initialSize, _arrayUnderTest.Capacity);
        }

        [Test]
        public void Constructor_WithoutParameter_SetDefaultCapacityTo4()
        {
            const int defaultCapacity = 4;
            _arrayUnderTest = new DynamicArray();

            Assert.AreEqual(defaultCapacity, _arrayUnderTest.Capacity);
        }

        [Test]
        public void Size_AfterCreation_SetTo0()
        {
            const int expectedSize = 0;
            _arrayUnderTest = new DynamicArray();

            Assert.AreEqual(expectedSize, _arrayUnderTest.Size);
        }

        [Test]
        public void Add_MultipleValues_ChangeSizeAccordingly()
        {
            const int expectedSize = 3;

            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(2);
            _arrayUnderTest.Add(3);

            Assert.AreEqual(expectedSize, _arrayUnderTest.Size);
        }

        [Test]
        public void Get_OneElementAdded_ReturnRequiredElement()
        {
            const int valueToInsert = 321;
            _arrayUnderTest.Add(valueToInsert);

            int result = _arrayUnderTest.Get(0);

            Assert.AreEqual(valueToInsert, result);
        }

        [Test]
        public void Get_MultipleElementsAdded_ReturnValuesRespectively()
        {
            const int valueToInsert1 = 321;
            const int valueToInsert2 = 456;
            const int valueToInsert3 = 987;
            _arrayUnderTest.Add(valueToInsert1);
            _arrayUnderTest.Add(valueToInsert2);
            _arrayUnderTest.Add(valueToInsert3);

            int result1 = _arrayUnderTest.Get(0);
            int result2 = _arrayUnderTest.Get(1);
            int result3 = _arrayUnderTest.Get(2);

            Assert.AreEqual(valueToInsert1, result1);
            Assert.AreEqual(valueToInsert2, result2);
            Assert.AreEqual(valueToInsert3, result3);
        }

        [Test]
        public void Get_IndexBiggerThanCapacity_ThrowsIndexOutOfRangeException()
        {
            const int valueToInsert1 = 321;
            _arrayUnderTest.Add(valueToInsert1);
            const int overSize = 10;

            Assert.Throws<IndexOutOfRangeException>(() => _arrayUnderTest.Get(overSize));
        }

        [Test]
        public void Get_IndexBiggerThanSizeButInCapacity_ThrowsIndexOutOfRangeException()
        {
            const int valueToInsert1 = 321;
            _arrayUnderTest.Add(valueToInsert1);
            const int overSize = 2;

            Assert.Throws<IndexOutOfRangeException>(() => _arrayUnderTest.Get(overSize));
        }

        [Test]
        public void Add_MoreValuesThanCurrentSize_DynamicallyIncreaseArrayCapacity()
        {
            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(2);
            _arrayUnderTest.Add(3);
            _arrayUnderTest.Add(4);

            _arrayUnderTest.Add(5);

            Assert.AreEqual(5, _arrayUnderTest.Get(4));
        }

        [Test]
        public void Remove_LastElement_DecreaseSizeByOne()
        {
            const int expectedSize = 3;

            _arrayUnderTest.Add(321);
            _arrayUnderTest.Add(432);
            _arrayUnderTest.Add(543);
            _arrayUnderTest.Add(654);

            int lastIndex = _arrayUnderTest.Size - 1;
            _arrayUnderTest.Remove(lastIndex);

            Assert.AreEqual(expectedSize, _arrayUnderTest.Size);
        }

        [Test]
        public void Remove_NegativeIndex_ThrowsIndexOutOfRangeException()
        {
            const int negativeIndex = -1;
            Assert.Throws<IndexOutOfRangeException>(() => _arrayUnderTest.Remove(negativeIndex));
        }

        [Test]
        public void Remove_IndexBiggerThanSize_ThrowsIndexOutOfRangeException()
        {
            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(2);
            _arrayUnderTest.Add(3);

            int bigIndex = _arrayUnderTest.Size + 1;

            Assert.Throws<IndexOutOfRangeException>(() => _arrayUnderTest.Remove(bigIndex));
        }

        [Test]
        public void Insert_OneValue_ChangeSizeAccordingly()
        {
            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(2);
            _arrayUnderTest.Add(3);
            const int expectedSize = 4;

            const int index = 1;
            int value = 22;
            _arrayUnderTest.Insert(index, value);

            Assert.AreEqual(expectedSize, _arrayUnderTest.Size);
        }

        [Test]
        public void Insert_OneValueAsLast_CanBeGetByLastIndex()
        {
            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(2);
            _arrayUnderTest.Add(3);

            int index = _arrayUnderTest.Size;
            int value = 22;
            _arrayUnderTest.Insert(index, value);

            int lastIndexAfterInsertion = _arrayUnderTest.Size - 1;
            int result = _arrayUnderTest.Get(lastIndexAfterInsertion);

            Assert.AreEqual(value, result);
        }

        [Test]
        public void Insert_OneValueInTheMiddle_ElementsAfterIndexAreShifted()
        {
            int shiftedElement = 2;
            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(shiftedElement);
            _arrayUnderTest.Add(3);

            int index = 1;
            int insertedValue = 22;
            _arrayUnderTest.Insert(index, insertedValue);
            int secondElement = _arrayUnderTest.Get(index);
            int thirdElement = _arrayUnderTest.Get(index + 1);

            Assert.AreEqual(insertedValue, secondElement);
            Assert.AreEqual(shiftedElement, thirdElement);
        }

        [Test]
        public void Insert_LowerThanZero_ThrowsIndexOutOfRangeException()
        {
            int negativeIndex = -1;

            Assert.Throws<IndexOutOfRangeException>(() => _arrayUnderTest.Remove(negativeIndex));
        }

        [Test]
        public void Insert_IndexBiggerThanSize_ValueInsertedAsLastElement()
        {
            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(2);
            _arrayUnderTest.Add(3);

            int index = _arrayUnderTest.Size + 1;
            int insertedValue = 22;
            _arrayUnderTest.Insert(index, insertedValue);

            int lastValue = _arrayUnderTest.Get(_arrayUnderTest.Size - 1);

            Assert.AreEqual(insertedValue, lastValue);
        }

        [Test]
        public void Insert_CapacityIsFull_DoubleCapacityDynamically()
        {
            const int initialCapacity = 5;
            int expectedCapacity = 5 * 2;
            _arrayUnderTest = new DynamicArray(initialCapacity);
            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(2);
            _arrayUnderTest.Add(3);
            _arrayUnderTest.Add(4);
            _arrayUnderTest.Add(5);

            int index = _arrayUnderTest.Size + 1;
            int insertedValue = 22;
            _arrayUnderTest.Insert(index, insertedValue);

            Assert.AreEqual(expectedCapacity, _arrayUnderTest.Capacity);
        }

        [Test]
        public void Insert_NegativeIndexWhenCapacityIsFull_KeepCapacityUntouched()
        {
            const int initialCapacity = 5;
            _arrayUnderTest = new DynamicArray(initialCapacity);
            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(2);
            _arrayUnderTest.Add(3);
            _arrayUnderTest.Add(4);
            _arrayUnderTest.Add(5);

            int index = -1;
            int insertedValue = 22;
            Assert.Throws<ArgumentOutOfRangeException>(() => _arrayUnderTest.Insert(index, insertedValue));
            Assert.AreEqual(initialCapacity, _arrayUnderTest.Capacity);
        }

        [Test]
        public void ToString_EmptyArray_BracketsWithoutContent()
        {
            _arrayUnderTest = new DynamicArray();
            string expected = "[]";

            String result = _arrayUnderTest.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_NonEmptyArray_ElementsInBrackestSeparatedByComma()
        {
            _arrayUnderTest = new DynamicArray();
            _arrayUnderTest.Add(1);
            _arrayUnderTest.Add(2);
            _arrayUnderTest.Add(3);

            string expected = "[1, 2, 3]";

            string result = _arrayUnderTest.ToString();

            Assert.AreEqual(expected, result);
        }
    }
}