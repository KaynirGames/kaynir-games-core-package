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

        public static Vector3 GetPointerWorldPosition()
        {
            return Input.touchCount > 0
                ? GetTouchWorldPosition(0)
                : GetMouseWorldPosition();
        }

        public static Vector3 GetMouseWorldPosition()
        {
            return GetWorldPosition(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetTouchWorldPosition(int touchIndex)
        {
            return GetWorldPosition(Input.GetTouch(touchIndex).position, Camera.main);
        }

        private static Vector3 GetWorldPosition(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0;

            return worldPosition;
        }
    }
}
