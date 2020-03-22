using System;
using Entities;
using UnityEngine;

namespace State
{
    public class GameEventStore : MonoBehaviour
    {
        public static GameEventStore Instance;

        private void Start()
        {
            Instance = this;
        }

        public event Action<GameObject> OnBuildingDestroyed;

        public void DispatchOnBuildDestroyed(GameObject targetGameObject)
        {
            OnBuildingDestroyed?.Invoke(targetGameObject);
        }
    }
}
