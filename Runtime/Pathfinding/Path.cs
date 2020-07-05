using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding
{
    /// <summary>
    /// Оптимальный маршрут.
    /// </summary>
    public class Path
    {
        /// <summary>
        /// Узлы маршрута.
        /// </summary>
        public PathNode[] PathNodes { get; private set; }
        /// <summary>
        /// Точки маршрута.
        /// </summary>
        public Vector2[] Waypoints { get; private set; }
        /// <summary>
        /// Маршрут существует?
        /// </summary>
        public bool Exist { get; private set; }

        public Path(PathNode[] pathNodes, bool exist)
        {
            PathNodes = pathNodes;
            Exist = exist;
            Waypoints = CollectWaypoints(pathNodes);
        }
        /// <summary>
        /// Упростить маршрут до ключевых точек.
        /// </summary>
        public Vector2[] Simplify()
        {
            List<Vector2> simplePath = new List<Vector2>();

            if (PathNodes != null && PathNodes.Length > 0)
            {
                Vector2 oldDirection = Vector2.zero;
                for (int i = 1; i < PathNodes.Length; i++)
                {
                    Vector2 newDirection = PathNodes[i].GridPosition - PathNodes[i - 1].GridPosition;
                    if (newDirection != oldDirection) // Добавляем точку, если изменилось направление маршрута.
                    {
                        simplePath.Add(PathNodes[i - 1].WorldPosition);
                    }
                    oldDirection = newDirection;
                }
                simplePath.Add(PathNodes[PathNodes.Length - 1].WorldPosition); // Также добавляем последнюю точку.
            }
            return simplePath.ToArray();
        }
        /// <summary>
        /// Собрать точки маршрута.
        /// </summary>
        private Vector2[] CollectWaypoints(PathNode[] pathNodes)
        {
            if (pathNodes == null) return new Vector2[0];

            Vector2[] waypoints = new Vector2[pathNodes.Length];
            for (int i = 0; i < pathNodes.Length; i++)
            {
                waypoints[i] = pathNodes[i].WorldPosition;
            }
            return waypoints;
        }
    }
}
