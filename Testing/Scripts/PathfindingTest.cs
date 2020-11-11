using KaynirGames.Pathfinding;
using KaynirGames.Tools;
using KaynirGames.Movement;
using UnityEngine;

public class PathfindingTest : MonoBehaviour
{
    [SerializeField] MovePositionPathfinding _pathfindingMovement = null;
    [SerializeField] Pathfinder _pathfinder = null;

    private void Start()
    {
        _pathfinder.Initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 endPoint = KaynirTools.GetMouseWorldPosition();
            _pathfindingMovement.SetPosition(endPoint);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _pathfindingMovement.StopMovement();
        }
    }
}
