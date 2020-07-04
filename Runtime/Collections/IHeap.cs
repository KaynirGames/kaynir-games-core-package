namespace KaynirGames.Collections
{
    /// <summary>
    /// Куча узлов (дерево поиска).
    /// </summary>
    public interface IHeap<T>
    {
        /// <summary>
        /// Число узлов в куче.
        /// </summary>
        int HeapSize { get; }
        /// <summary>
        /// Добавить узел в кучу.
        /// </summary>
        void Add(T heapNode);
        /// <summary>
        /// Достать верхний элемент кучи.
        /// </summary>
        T RemoveFirst();
        /// <summary>
        /// Упорядочить кучу.
        /// </summary>
        void Heapify(T heapNode);
        /// <summary>
        /// Проверить, содержится ли узел в куче.
        /// </summary>
        bool Contains(T heapNode);
    }
}