using UnityEngine;

namespace PotionMorph.Map
{
    public class Spatula : Container
    {
        private Vector2 _spawnPos;

        protected override void Awake()
        {
            base.Awake();

            _spawnPos = transform.position;
        }

        private void Update()
        {
            if (transform.position.y < -10f)
            {
                Rigidbody.linearVelocity = Vector2.zero;
                transform.position = _spawnPos;
            }
        }
    }
}
