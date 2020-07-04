using System;

namespace KaynirGames.Collections
{
    /// <summary>
    /// Узел кучи.
    /// </summary>
    public interface IHeapNode<T> : IComparable<T>
    {
        /// <summary>
        /// Индекс узла кучи.
        /// </summary>
        int HeapIndex { get; set; }
    }
}