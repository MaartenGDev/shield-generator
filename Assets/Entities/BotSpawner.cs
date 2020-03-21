using UnityEngine;

namespace Entities
{
    public class BotSpawner : MonoBehaviour
    {
        public GameObject botPrefab;
        private Vector3 _platformPosition;
        private int _botLimit = 2;
        private int _currentBots = 0;

        private void Awake()
        {
            _platformPosition = transform.position;

            InvokeRepeating(nameof(Spawn), 0, 1f);
        }

        private void Spawn()
        {
            if (_currentBots >= _botLimit)
            {
                return;
            }

            _currentBots++;
            Instantiate(botPrefab, new Vector3(_platformPosition.x, _platformPosition.y + 3f,_platformPosition.z), Quaternion.identity);
        }
    }
}
