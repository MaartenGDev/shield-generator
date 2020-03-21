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

            AttackNextTarget(null);

            GameEventStore.Instance.OnBuildingDestroyed += AttackNextTarget;
        }

        private void AttackNextTarget(GameObject deletedGameObject)
        {
            var possibleTargets = GameObject
                .FindGameObjectsWithTag("Building")
                .Where(target => target != deletedGameObject && Vector3.Distance(target.transform.position, transform.position) < aggressionRange)
                .OrderByDescending(x => x.GetComponent<Building>().importance)
                .ToList();

            if (!possibleTargets.Any()) return;
            
            var currentTarget = possibleTargets.First();
            MoveToTarget(currentTarget);
        }
        
        private void MoveToTarget(GameObject currentTarget)
        {
            Debug.Log("Got target!");
            
          

            _agent.destination = currentTarget.transform.position;
            _agent.transform.LookAt(currentTarget.transform);
        }
    }
}
