using KaynirGames.Pathfinding;
using KaynirGames.Tools;
using KaynirGames.Movement;
using UnityEngine;

public class PathfindingTest : MonoBehaviour
{
    [SerializeField] PathfindingMovement pathfindingMovement = null;

    private void Start()
    {
        Pathfinder.Instance.Initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 endPoint = MyToolset.GetMouseWorldPosition();
            pathfindingMovement.SetMovementPosition(endPoint);
        }
    }
}
