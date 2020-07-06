using UnityEngine;

namespace KaynirGames.Pathfinding
{
    /// <summary>
    /// Объект, нуждающийся в оптимальном пути.
    /// </summary>
    public class Seeker : MonoBehaviour
    {
        private Pathfinder _activePathfinder; // Действующий искатель пути, осуществляющий вычисления.

        private void Awake()
        {
            Pathfinder.OnActivePathfinderChange += SetActivePathfinder;
        }
        /// <summary>
        /// Получить полный оптимальный маршрут между точками.
        /// </summary>
        public Vector2[] GetFullPath(Vector2 startPoint, Vector2 endPoint)
        {
            Path path = _activePathfinder.FindPath(startPoint, endPoint);
            return path.Exist ? path.Waypoints : new Vector2[0];
        }
        /// <summary>
        /// Получить упрощенный оптимальный маршрут между точками.
        /// </summary>
        public Vector2[] GetSimplePath(Vector2 startPoint, Vector2 endPoint)
        {
            Path path = _activePathfinder.FindPath(startPoint, endPoint);
            return path.Exist ? path.Simplify() : new Vector2[0];
        }
        /// <summary>
        /// Установить действующего искателя пути.
        /// </summary>
        private void SetActivePathfinder(Pathfinder pathfinder)
        {
            // Одновременно может быть активен только один искатель пути.
            // Устанавливаем искателя, если его не было изначально, либо отключился предыдущий.
            if (_activePathfinder == null || pathfinder == null)
            {
                _activePathfinder = pathfinder;
            }
        }
    }
}
