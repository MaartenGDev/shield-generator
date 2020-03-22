using System.Collections.Generic;
using System.Linq;
using State;
using UnityEngine;

namespace Entities
{
    public class FireCannonball : MonoBehaviour
    {
        public Rigidbody cannonballPrefab;

        public float aggressionRange = 500;
        public Transform launchStartLocation;
        public LayerMask layer;

        private bool _shouldRefreshTargetCache = true;
        private List<GameObject> _cachedTargets = new List<GameObject>();

        private void Awake()
        {
            InvokeRepeating(nameof(FireOnClosestTarget), 0, 1f);
        }

        private List<GameObject> GetPossibleTargets()
        {
            _cachedTargets = GameObject
                .FindGameObjectsWithTag("Building")
                .Where(target => Vector3.Distance(target.transform.position, transform.position) < aggressionRange)
                .OrderByDescending(x => x.GetComponent<Building>().importance)
                .ToList();


            return _cachedTargets;
        }

        private void FireOnClosestTarget()
        {
            var possibleTargets = GetPossibleTargets();
            if (!possibleTargets.Any()) return;

            var currentTarget = possibleTargets.First();

            LaunchProjectile(currentTarget.transform.position);
        }


        private void LaunchProjectile(Vector3 targetPosition)
        {
            var sourceDirection = launchStartLocation.position;
            var direction = targetPosition - sourceDirection;

            var canHitTarget = Physics.Raycast(sourceDirection, direction, out var hit, aggressionRange,
                layer);

            if (!canHitTarget) return;

            var initialVelocity = CalculateVelocity(hit.point, launchStartLocation.position, 1f);

            var nextRotation = Quaternion.LookRotation(initialVelocity);
            transform.rotation = nextRotation;

            var rigidBody = Instantiate(cannonballPrefab, sourceDirection, Quaternion.identity);
            rigidBody.velocity = initialVelocity;
        }

        private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
        {
            var distance = target - origin;
            var distanceXz = distance;
            distanceXz.y = 0;

            var vectorY = distance.y;
            var vectorLength = distanceXz.magnitude;

            var velocityXz = vectorLength / time;
            var velocityY = vectorY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

            var result = distanceXz.normalized;
            result *= velocityXz;
            result.y = velocityY;

            return result;
        }
    }
}