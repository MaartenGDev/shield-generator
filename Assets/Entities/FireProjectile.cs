using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
    public class FireProjectile : MonoBehaviour
    {
        public Rigidbody projectilePrefab;
        public float speed = 4;
        public float aggressionRange = 10;

        // Start is called before the first frame update
        private void Start()
        {
            InvokeRepeating(nameof(FireOnClosestTarget), 0, 1f);
        }


        private void FireOnClosestTarget()
        {
            var hasEnemyInRange = GameObject.FindGameObjectsWithTag("Building")
                .Any(target =>
                    Vector3.Distance(target.transform.position, transform.position) < aggressionRange);

            if (!hasEnemyInRange) return;

            var projectileEntity = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectileEntity.velocity = -transform.forward * speed;

            var associatedProjectile = projectileEntity.GetComponent<Projectile>();
            if (associatedProjectile == null) return;

            var isInShield = Physics.OverlapSphere(transform.position, 0).Any(x => x.GetComponent<Shield>() != null);

            associatedProjectile.hasSpawnedInsideShield = isInShield;
        }
    }
}