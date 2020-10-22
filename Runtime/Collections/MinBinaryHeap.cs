using System.Collections.Generic;

namespace KaynirGames.Collections
{
    /// <summary>
    /// Минимальная двоичная куча (полное бинарное дерево, где приоритет вершин меньше их потомков).
    /// </summary>
    public class MinBinaryHeap<T> : IHeap<T> where T : IHeapNode<T>
    {
        public int HeapSize => _heap.Count;

        private List<T> _heap;

        public MinBinaryHeap()
        {
            _heap = new List<T>();
        }

        public void Add(T heapNode)
        {
            heapNode.HeapIndex = HeapSize;
            _heap.Add(heapNode);

            while (heapNode.HeapIndex > 0)
            {
                int parent = (heapNode.HeapIndex - 1) / 2;

                if (heapNode.CompareTo(_heap[parent]) < 0)
                {
                    Swap(heapNode, _heap[parent]);
                }
                else break;
            }
        }

        public T RemoveFirst()
        {
            T firstElement = _heap[0];

            if (HeapSize > 1)
            {
                Swap(_heap[0], _heap[HeapSize - 1]);

                _heap.RemoveAt(HeapSize - 1);

                Heapify(_heap[0]);
            }
            else { _heap.RemoveAt(0); }

            return firstElement;
        }

        public void Heapify(T heapItem)
        {
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

        public bool Contains(T heapItem)
        {
            return _heap.Contains(heapItem);
        }

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
