using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Tools
{
    public static class MyToolset
    {
        /// <summary>
        /// Позиция мыши на экране в мировых координатах (без учета оси Z).
        /// </summary>
        public static Vector3 GetMouseWorldPosition()
        {
            return GetScreenWorldPosition(Input.mousePosition, Camera.main);
        }
        /// <summary>
        /// Позиция прикосновения к экрану в мировых координатах (без учета оси Z).
        /// </summary>
        public static Vector3 GetTouchWorldPosition(int touchIndex)
        {
            return GetScreenWorldPosition(Input.GetTouch(touchIndex).position, Camera.main);
        }
        /// <summary>
        /// Преобразовать позицию на экране в мировые координаты. 
        /// </summary>
        private static Vector3 GetScreenWorldPosition(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 vector = worldCamera.ScreenToWorldPoint(screenPosition);
            vector.z = 0;
            return vector;
        }
    }
}
