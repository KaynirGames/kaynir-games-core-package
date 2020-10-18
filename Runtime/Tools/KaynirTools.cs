using UnityEngine;

namespace KaynirGames.Tools
{
    public static class KaynirTools
    {
        public static Vector3 GetPointerRawPosition()
        {
            return Input.touchCount > 0
                ? (Vector3)Input.GetTouch(0).rawPosition
                : Input.mousePosition;
        }

        public static Vector3 GetMouseWorldPosition()
        {
            return GetScreenWorldPosition(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetTouchWorldPosition(int touchIndex)
        {
            return GetScreenWorldPosition(Input.GetTouch(touchIndex).position, Camera.main);
        }

        private static Vector3 GetScreenWorldPosition(Vector3 screenPosition, Camera worldCamera)
        {
            return worldCamera.ScreenToWorldPoint(screenPosition);
        }
    }
}
