using System.Collections;
using Abstract;
using UnityEngine;

namespace Player
{
    public class PlayerCollision : PaperManager
    {
        private void Awake()
        {
            playerBag = transform.GetChild(1).transform;
            deskArea = GameObject.FindGameObjectWithTag(StringCache.DeskArea).transform;
        }
        
        private void OnEnable()
        {
            StartCoroutine(CollectPaper());
            StartCoroutine(DropThePaper());
        }

        private IEnumerator CollectPaper()
        {
            while (true)  // 2 --> Koşul sağlanıyorsa Fonksiyonu çağır
            {
                if (onCollectArea) { CollectPaperFromTheArea(); }
                yield return new WaitForSeconds(Duration);
            }
        }

        private IEnumerator DropThePaper()
        {
            while (true)
            {
                if (onDropArea) { DropThePaperOnTheArea(); }
                yield return new WaitForSeconds(Duration);
            }
        }

        #region Trigger States
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag(StringCache.PickArea))
                onCollectArea = true;
            if (other.gameObject.CompareTag(StringCache.DropArea))
                onDropArea = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(StringCache.PickArea))
                onCollectArea = false;
            if (other.gameObject.CompareTag(StringCache.DropArea))
                onDropArea = false;
        }
        #endregion
    }
}
