namespace lab3
{
    // класс Arr_chain_list наследует класс Base_list
    public class Arr_chain_list : Base_list
    {
        // узел списка
        private Node head = null;

        // внутренний класс Node для представления узла списка
        public class Node
        {
            // данные в узле
            public int Data { get; set; }
            // ссылка на следующий узел
            public Node Next { get; set; }
            // конструктор узла
            public Node(int data, Node next)
            {
                Data = data;
                Next = next;
            }
        }

        // метод для поиска узла по индексу
        private Node NodeFinder(int pos)
        {
            if (pos >= count) {return null;}
            int i = 0;
            Node Checker = head;
            while (Checker != null && i < pos)
            {
                Checker = Checker.Next;
                i++;
            }
            if (i == pos) {return Checker;}
            else {return null;}
        }

        // метод для добавления элемента в конец списка
        public override void Add(int data)
        {
            if (head == null)
            {
                head = new Node(data, null);
            }
            else
            {
                Node lastNode = NodeFinder(count - 1);
                lastNode.Next = new Node(data, null);
            }
            count++;
        }

        // метод для вставки элемента по указанному индексу
        public override void Insert(int pos, int data)
        {
            if (pos < 0 || pos > count)
            {
                return;
            }

            if (pos == 0)
            {
                head = new Node(data, head);
            }
            else
            {
                Node prevNode = NodeFinder(pos - 1);
                prevNode.Next = new Node(data, prevNode.Next);
            }
            count++;
        }

        // метод для удаления элемента по указанному индексу
        public override void Delete(int pos)
        {
            if (pos < 0 || pos >= count)
            {
                return;
            }

            if (pos == 0)
            {
                head = head.Next;
            }
            else
            {
                Node prevNode = NodeFinder(pos - 1);
                prevNode.Next = prevNode.Next.Next;
            }
            count--;
        }

        // метод для очистки списка
        public override void Clear()
        {
            head = null;
            count = 0;
        }

        // индексатор для доступа к элементу списка по индексу
        public override int this[int index]
        {
            get
            {
                Node current = NodeFinder(index);
                if (current == null)
                {
                    return 0;
                }
                return current.Data;
            }
            set
            {
                Node current = NodeFinder(index);
                if (current == null)
                {
                    return;
                }
                current.Data = value;
            }
        }

        // метод для сортировки списка
        public override void Sort()
        {
            if (count <= 1)
            {
                return;
            }

            int temp;

            for (int i = 0; i < count; i++)
            {
                Node current = head;
                while (current != null & current.Next != null)
                {
                    if (current.Data > current.Next.Data)
                    {
                        temp = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = temp;
                    }
                    current = current.Next;
                }            
            }
        }
    }
}
