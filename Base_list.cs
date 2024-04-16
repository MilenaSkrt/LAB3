
namespace lab3
{
    // Абстрактный класс Base_list, реализующий интерфейс IEnumerable и ограничивающий тип T интерфейсом IComparable
    public abstract class Base_list<T> : IEnumerable<T> where T : IComparable
    {
        // Защищенное поле для хранения количества элементов в списке
        protected int count;

        // Свойство для получения текущего значения количества элементов в списке
        public int Count
        {
            get { return count; }
        }

        // Абстрактные методы, которые должны быть реализованы в производных классах
        public abstract void Add(T item);
        public abstract void Insert(int pos, T item);
        public abstract void Delete(int pos);
        public abstract void Clear();
        public abstract T this[int i] { get; set; }

        // Метод для вывода элементов списка на консоль
        public void Print()
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(this[i] + " ");
            }
            Console.WriteLine();
        }

        // Метод для копирования элементов из одного списка в другой
        public void Assign(Base_list<T> source)
        {
            Clear();
            for (int i = 0; i < source.count; i++)
            {
                Add(source[i]);
            }
        }

        // Метод для копирования элементов из текущего списка в другой
        public void AssignTo(Base_list<T> dest)
        {
            dest.Assign(this);
        }

        // Метод для создания глубокой копии текущего списка
        public Base_list<T> Clone()
        {
            Base_list<T> clone_list = EmptyClone();
            clone_list.Assign(this);
            return clone_list;
        }

        // Абстрактный метод для создания пустого списка
        protected abstract Base_list<T> EmptyClone();

        // Виртуальный метод для сортировки элементов списка
        public virtual void Sort()
        {
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (this[i] > this[j])
                    {
                        T temp = this[i];
                        this[i] = this[j];
                        this[j] = temp;
                    }
                }
            }
        }

        // Метод для сравнения двух списков на равенство
        public bool IsEqual(Base_list<T> AnotherOne)
        {
            if (AnotherOne == null)
                return false;

            if (this.Count != AnotherOne.Count)
                return false;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] != AnotherOne[i])
                    return false;
            }
            return true;
        }

        // Метод для сохранения элементов списка в файл
        public void SaveToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (T item in this)
                    {
                        writer.WriteLine(item);
                    }
                }
            }
            catch (IOException)
            {
                ExceptionCounter.ChainExceptionCounterIncrement();
                return;
            }
        }

        // Метод для загрузки элементов списка из файла
        public void LoadFromFile(string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        T item = (T)Convert.ChangeType(line, typeof(T));
                        Add(item);
                    }
                }
            }
            catch (IOException)
            {
                ExceptionCounter.ChainExceptionCounterIncrement();
                return;
            }
        }

        // Перегруженные операторы для сравнения списков и объединения списков
        public static bool operator ==(Base_list<T> list1, Base_list<T> list2)
        {
            return list1.IsEqual(list2);
        }

        public static bool operator !=(Base_list<T> list1, Base_list<T> list2)
        {
            return !list1.IsEqual(list2);
        }

        public static bool operator +(Base_list<T> list1, Base_list<T> list2)
        {
            Base_list<T> list3 = new Base_list<T>();
            list3.Assign(list1);
            list3.Assign(list2);
            return list3;
        }

        // Вложенные классы исключений
        public class BadIndexException : Exception
        {
            public BadIndexException(string message) : base(message) { }
        }

        public class BadFileException : Exception
        {
            public BadFileException(string message) : base(message) { }
        }

        // Вложенный класс для подсчета исключений
        public class ExceptionCounter
        {
            protected static int ChainExceptionCount = 0;
            protected static int ArrayExceptionCount = 0;

            public static int ChainExceptionCount
            {
                get { return ChainExceptionCount; }
            }

            public static int ArrayExceptionCount
            {
                get { return ArrayExceptionCount; }
            }

            public static void ChainExceptionCounterIncrement()
            {
                ChainExceptionCount++;
            }

            public static void ArrayExceptionCounterIncrement()
            {
                ArrayExceptionCount++;
            }

            public static void ExceptionCounterReset()
            {
                ChainExceptionCount = 0;
                ArrayExceptionCount = 0;
            }
        }

        // Реализация интерфейса IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return new BaseListEnumerator<T>(this);
        }

        // Реализация неявного интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Вложенный класс для перечисления элементов списка
        private class BaseListEnumerator<T> : IEnumerator<T>
        {
            private Base_list<T> _list;
            private int _index;
            private T _current;

            public BaseListEnumerator(Base_list<T> list)
            {
                _list = list;
                _index = -1;
                _current = default(T);
            }

            public T Current
            {
                get
                {
                    if (_index == -1 || _index == _list.Count)
                    {
                        throw new InvalidOperationException();
                    }
                    return _current;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                _list = null;
                _current = default(T);
            }

            public bool MoveNext()
            {
                if (_index < _list.Count - 1)
                {
                    _index++;
                    _current = _list[_index];
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _index = -1;
                _current = default(T);
            }
        }
    }
}
