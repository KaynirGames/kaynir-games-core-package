using System;

namespace KaynirGames.Collections
{
    /// <summary>
    /// Узел кучи.
    /// </summary>
    public interface IHeapNode<T> : IComparable<T>
    {
        int HeapIndex { get; set; }
    }
}