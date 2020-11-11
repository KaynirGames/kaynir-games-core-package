using UnityEngine;

namespace KaynirGames.Movement
{
    public class MoveTransform : MoveBase
    {
        private void Update()
        {
            Move();
        }

        protected override void Move()
        {
            transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        }
    }
}
