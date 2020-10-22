namespace KaynirGames.Collections
{
    /// <summary>
    /// Куча узлов (дерево поиска).
    /// </summary>
    public interface IHeap<T>
    {
        int HeapSize { get; }

        void Add(T heapNode);

        T RemoveFirst();

        void Heapify(T heapNode);

        bool Contains(T heapNode);
    }
}