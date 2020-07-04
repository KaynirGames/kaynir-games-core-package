using KaynirGames.Pathfinding;
using KaynirGames.Tools;
using UnityEngine;

public class PathfindingTest : MonoBehaviour
{
    [SerializeField] Seeker _seeker = null;
    [SerializeField] Pathfinder _pathfinder = null;

    private void Start()
    {
        _pathfinder.CreateGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 endPoint = MyToolset.GetMouseWorldPosition();
            if (_seeker.IsRequestDone)
                _seeker.RequestPath(_seeker.transform.position, endPoint);
        }
    }
}
