using UnityEngine;

namespace Entities
{
    public class Projectile : MonoBehaviour
    {
        [HideInInspector]
        public bool hasSpawnedInsideShield = false;

        public int damage = 10;
        private void Start()
        {
            Destroy(gameObject, 10);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }

        
    }
}