using UnityEngine;

namespace KaynirGames.Pathfinding
{
    /// <summary>
    /// Объект, нуждающийся в оптимальном пути.
    /// </summary>
    public class Seeker : MonoBehaviour
    {
        public Vector2[] GetFullPath(Vector2 startPoint, Vector2 endPoint)
        {
            Path path = Pathfinder.FindPath(startPoint, endPoint);
            return path.Exist ? path.Waypoints : new Vector2[0];
        }

        public Vector2[] GetSimplePath(Vector2 startPoint, Vector2 endPoint)
        {
            Path path = Pathfinder.FindPath(startPoint, endPoint);
            return path.Exist ? path.SimplifyPath() : new Vector2[0];
        }
    }
}
