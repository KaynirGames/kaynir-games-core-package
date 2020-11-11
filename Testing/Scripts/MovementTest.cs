using UnityEngine;
using KaynirGames.Tools;
using KaynirGames.Movement;

public class MovementTest : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 endPoint = KaynirTools.GetMouseWorldPosition();
            foreach (GameObject character in _characters)
            {
                character.GetComponent<MovePositionBase>().SetPosition(endPoint);
            }
        }
    }
}
