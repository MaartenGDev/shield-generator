using UnityEngine;

namespace Entities
{
    public class ShieldGenerator : Building
    {
        public GameObject associatedShield;

        protected override void HandleDestroy(GameObject targetGameObject)
        {
            base.HandleDestroy(targetGameObject);

            if (associatedShield != null)
            {
                Destroy(associatedShield);
            }
        }
    }
}
