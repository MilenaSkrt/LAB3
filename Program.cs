namespace lab3
{
// объявление класса Program
class Program
{
static void Main(string[] args)
{
// создание объекта класса Arr_list
Arr_list array = new Arr_list();
// создание объекта класса Arr_chain_list
Arr_chain_list chain = new Arr_chain_list();
// создание объекта класса Random для генерации случайных чисел
Random rnd = new Random();

c
Copy code
        // цикл для выполнения операций
        for (int i = 0; i < 15000; i++)
        {
            // генерация случайной операции
            int operation = rnd.Next(5);
            // генерация случайного элемента
            int item = rnd.Next(100);
            // генерация случайной позиции
            int pos = rnd.Next(1000);
            
            // выполнение операции в зависимости от случайного значения
            switch (operation)
            {
                // добавление элемента в массив и связанный список
                case 0:
                    array.Add(item);
                    chain.Add(item);
                    break;
                // удаление элемента из массива и связанного списка
                case 1:
                    array.Delete(pos);
                    chain.Delete(pos);
                    break;
                // вставка элемента на определенную позицию в массиве и связанном списке
                case 2:
                    array.Insert(pos, item);
                    chain.Insert(pos, item);
                    break;
                // очистка массива и связанного списка
                case 3:
                    array.Clear();
                    chain.Clear();
                    break;
                // установка значения элемента на определенной позиции в массиве и связанном списке
                case 4:
                    array[pos] = item;
                    chain[pos] = item;
                    break;
            }
        }
        
        // проверка на равенство массива и связанного списка
        if (!array.IsEqual(chain))
            Console.WriteLine("Error");
        else
            Console.WriteLine("Successful");

        // очистка массива и связанного списка
        array.Clear();
        chain.Clear();
    }
}
}
