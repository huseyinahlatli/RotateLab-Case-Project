using System.Collections;
using Abstract;
using UnityEngine;

namespace Player
{
    public class PlayerTriggerHandler : GameManager
    {
        private void Awake()
        {
            playerBag = transform.GetChild(1).transform;
            deskArea = GameObject.FindGameObjectWithTag(StringCache.DeskArea).transform;
        }
        
        private void OnEnable() // 2 --> Coroutine'leri teke düşür!
        {
            StartCoroutine(CollectPaper());
            StartCoroutine(DropThePaper());
            StartCoroutine(CollectMoney());
        }

        private IEnumerator CollectPaper()
        {
            while (true)  
            {
                if (onCollectArea) { CollectPaperFromTheArea(); }
                yield return new WaitForSeconds(FixedDuration);
            }
        }

        private IEnumerator DropThePaper()
        {
            while (true)
            {
                if (onDropArea) { DropThePaperOnTheArea(); }
                else { onDroppingPaper = false; }
                yield return new WaitForSeconds(FixedDuration);
            }
        }

        private IEnumerator CollectMoney()
        {
            while (true)
            {
                if (onMoneyArea && MoneyManager.moneyArea.childCount > 0) { GetMoney(); }
                yield return new WaitForSeconds(FixedDuration);
            }
        }

        #region Trigger States
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
        #endregion
    }
}
