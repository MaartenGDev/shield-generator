using System;
using UnityEngine;

namespace Entities
{
    public class Shield : MonoBehaviour
    {
        private bool _isSafeZone;
        private Color _originalColor;

        private
            void Awake()
        {
            _isSafeZone = CompareTag("IgnoreCollision");
            _originalColor = GetComponent<Renderer>().material.color;
        }

        private void OnTriggerEnter(Collider other)
        {

            var projectile = other.GetComponent<Projectile>();
            if (projectile == null) return;

            if (!projectile.hasSpawnedInsideShield)
            {
                Destroy(other.gameObject);
                ShowHitAnimation();
            }
        }


        private void ShowHitAnimation()
        {
            var nextColor = new Color(_originalColor.r,_originalColor.g, _originalColor.b, _originalColor.a + 0.1f);
            
            GetComponent<Renderer>().material.color = nextColor;
            
            Invoke(nameof(ResetColor), 0.2f);
        }

        private void ResetColor()
        {
            GetComponent<Renderer>().material.color = _originalColor;
        }
    }
}