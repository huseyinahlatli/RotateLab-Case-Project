using Abstract;
using Cache;
using UnityEngine;

namespace Player
{
    public class PlayerTriggerHandler : GameManager
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag(StringCache.PickArea))
                onCollectArea = true;
            if (other.gameObject.CompareTag(StringCache.DropArea))
                onDropArea = true;
            if (other.gameObject.CompareTag(StringCache.MoneyArea))
                onMoneyArea = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(StringCache.PickArea))
                onCollectArea = false;
            if (other.gameObject.CompareTag(StringCache.DropArea))
                onDropArea = false;
            if (other.gameObject.CompareTag(StringCache.MoneyArea))
                onMoneyArea = false;
        }
    }
}
