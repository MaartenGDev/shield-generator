using System;
using System.Collections.Generic;
using System.Linq;
using State;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class BotController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public float aggressionRange = 100f;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            AttackNextTarget();
        }

        private void AttackNextTarget()
        {
            var possibleTargets = GameObject
                .FindGameObjectsWithTag("Building")
                .Where(target => Vector3.Distance(target.transform.position, transform.position) < aggressionRange)
                .OrderByDescending(x => x.GetComponent<Building>().importance)
                .ToList();

            if (!possibleTargets.Any()) return;
            
            var currentTarget = possibleTargets.First();
            MoveToTarget(currentTarget);
        }
        
        private void MoveToTarget(GameObject currentTarget)
        {
            _agent.destination = currentTarget.transform.position;
            _agent.transform.LookAt(currentTarget.transform);
        }

        private void OnMouseDown()
        {
            CameraController.instance.followTransform = transform;
        }
    }
}
