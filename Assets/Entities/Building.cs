using State;
using UnityEngine;

namespace Entities
{
    public class Building : MonoBehaviour
    {
        public int health = 20;
        public int importance = 0;

        private TextMesh _healthLabel;
        // Start is called before the first frame update
        void Awake()
        {
            _healthLabel = transform.GetChild(0).GetComponent<TextMesh>();
            UpdateHealthLabel();
        }

        private void OnCollisionEnter(Collision other)
        {
            var component = other.collider.GetComponent<Projectile>();

            if (component != null)
            {
                HandleDamage(component.damage);
            }
        }

        private void UpdateHealthLabel()
        {
            _healthLabel.text = "Health: " + health;
        }
        
        private void HandleDamage(int damage)
        {
            health -= damage;
            UpdateHealthLabel();
            
            if (health <= 0)
            {
                HandleDestroy(gameObject);
            }
        }

        protected virtual void HandleDestroy(GameObject targetGameObject)
        {
            Destroy(targetGameObject);
            GameEventStore.Instance.DispatchOnBuildDestroyed(targetGameObject);
        }
    }
}