using System.Collections.Generic;

namespace KaynirGames.Collections
{
    /// <summary>
    /// Минимальная двоичная куча (полное бинарное дерево, где приоритет вершин меньше их потомков).
    /// </summary>
    public class MinBinaryHeap<T> : IHeap<T> where T : IHeapNode<T>
    {
        /// <summary>
        /// Список элементов в куче.
        /// </summary>
        private List<T> _heap;
        /// <summary>
        /// Количество элементов в куче.
        /// </summary>
        public int HeapSize => _heap.Count;
        /// <summary>
        /// Создать новую минимальную двоичную кучу.
        /// </summary>
        public MinBinaryHeap()
        {
            _heap = new List<T>();
        }
        /// <summary>
        /// Добавить новый элемент в кучу.
        /// </summary>
        public void Add(T heapNode)
        {
            // Добавляем новый элемент в конец кучи.
            heapNode.HeapIndex = HeapSize;
            _heap.Add(heapNode);

            while (heapNode.HeapIndex > 0)
            {
                // Текущая вершина относительно элемента.
                int parent = (heapNode.HeapIndex - 1) / 2;
                // Если элемент меньше вершины, то меняем их местами.
                if (heapNode.CompareTo(_heap[parent]) < 0)
                {
                    Swap(heapNode, _heap[parent]);
                }
                else break;
            }
        }
        /// <summary>
        /// Забрать первый (минимальный) элемент кучи.
        /// </summary>
        public T RemoveFirst()
        {
            // Сохраняем первый элемент.
            T firstElement = _heap[0];
            if (HeapSize > 1)
            {
                // Ставим последний элемент в начало кучи.
                Swap(_heap[0], _heap[HeapSize - 1]);
                // Удаляем первый элемент, который находится в конце после перестановки.
                _heap.RemoveAt(HeapSize - 1);
                // Упорядочиваем кучу с первого элемента.
                Heapify(_heap[0]);
            }
            else _heap.RemoveAt(0);

            return firstElement;
        }
        /// <summary>
        /// Упорядочить двоичную кучу.
        /// </summary>
        public void Heapify(T heapItem)
        {
            // Пока элемент больше наименьшего потомка, меняем их местами.
            while (true)
            {
                int leftChild = 2 * heapItem.HeapIndex + 1;
                int rightChild = 2 * heapItem.HeapIndex + 2;

                if (leftChild < HeapSize && rightChild < HeapSize)
                {
                    int smallestChild = (_heap[leftChild].CompareTo(_heap[rightChild]) < 0) ? leftChild : rightChild;

                    if (heapItem.CompareTo(_heap[smallestChild]) > 0)
                    {
                        Swap(heapItem, _heap[smallestChild]);
                    }
                    else break;
                }
                else break;
            }
        }
        /// <summary>
        /// Проверить, содержится ли элемент в куче.
        /// </summary>
        public bool Contains(T heapItem)
        {
            return _heap.Contains(heapItem);
        }
        /// <summary>
        /// Поменять местами узлы кучи.
        /// </summary>
        private void Swap(T nodeA, T nodeB)
        {
            _heap[nodeA.HeapIndex] = nodeB;
            _heap[nodeB.HeapIndex] = nodeA;
            int temp = nodeA.HeapIndex;
            nodeA.HeapIndex = nodeB.HeapIndex;
            nodeB.HeapIndex = temp;
        }
    }
}
