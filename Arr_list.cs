namespace lab3 
{ // Объявление обобщенного класса Arr_list<T>, наследующего Base_list<T> и имеющего ограничение типа T: IComparable<T> public class Arr_list<T> : Base_list<T> where T : IComparable<T> 
  {
private T[] buffer; // Приватное поле buffer, хранящее элементы списка

    // Конструктор класса, инициализирующий пустой массив buffer и устанавливающий счетчик count в ноль
    public Arr_list()
    {
        buffer = new T[0];
        count = 0;
    }

    // метод Expand, увеличивающий размер буфера при необходимости
    private void Expand()
    {
        if (count == buffer.Length)
        {
            T[] newBuffer;
            if (buffer.Length == 0)
            {
                newBuffer = new T[2];
            }
            else
            {
                newBuffer = new T[buffer.Length * 2];
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                newBuffer[i] = buffer[i];
            }
            buffer = newBuffer;
        }
    }

    // Переопределение метода Add для добавления элемента в список
    public override void Add(T item)
    {
        Expand();
        buffer[count] = item;
        count++;
    }

    // Переопределение метода Insert для вставки элемента в указанную позицию
    public override void Insert(int pos, T item)
    {
        try
        {
            if (pos < 0 || pos > count)
            {
                ExceptionCounter.ArraayExceptionCounterIncrement(); // Увеличение счетчика исключений
                return;
            }

            Expand();

            for (int i = count; i > pos; i--)
            {
                buffer[i] = buffer[i - 1];
            }

            buffer[pos] = item;
            count++;
        }
        catch(BadIndexException){
            ExceptionCounter.ArrayExceptionCounterIncrement(); // Увеличение счетчика исключений
            return;
        }
    }

    // Переопределение метода Delete для удаления элемента из указанной позиции
    public override void Delete(int pos)
    {
        try{
            if (pos < 0 || pos >= count)
            {
                ExceptionCounter.ArrayExceptionCounterIncrement(); // Увеличение счетчика исключений
                return;
            }

            for (int i = pos; i < count - 1; i++)
            {
                buffer[i] = buffer[i + 1];
            }

            count--;
        }
        catch(BadIndexException){
            ExceptionCounter.ArrayExceptionCounterIncrement(); // Увеличение счетчика исключений
            return;
        }
    }

    // Переопределение метода Clear для очистки списка
    public override void Clear()
    {
        buffer = new int[0];
        count = 0;
    }

    // Переопределение индексатора для доступа к элементам по индексу
    public override T this[int index]
    {
        get
        {
            try{
                if (index < 0 || index >= count)
                {
                    ExceptionCounter.ArrayExceptionCounterIncrement(); // Увеличение счетчика исключений
                    return default(T);
                }
                return buffer[index];
            }
            catch(BadIndexException){
                ExceptionCounter.ArrayExceptionCounterIncrement(); // Увеличение счетчика исключений
                return default(T);
            }
        }
        set
        {
            try{
                if (index < 0 || index >= count)
                {
                    ExceptionCounter.ArrayExceptionCounterIncrement(); // Увеличение счетчика исключений
                    return;
                }
                buffer[index] = value;
            }
            catch(BadIndexException){
                ExceptionCounter.ArrayExceptionCounterIncrement(); // Увеличение счетчика исключений
                return;
            }
        }
    }

    // Переопределение метода EmptyClone для создания копии объекта
    protected override Base_list<T> EmptyClone()
    {
        return new Arr_list<T>();
    }

    // Переопределение метода ToString для представления списка в виде строки
    public override string ToString()
    {
        string str = "";
        for (int i = 0; i < count; i++)
        {
            str += buffer[i] + " ";
        }
        return str;
        // return string.Join(" ", buffer);
    }
}
}
