using UnityEngine;
using System.Collections;
using DG.Tweening;
using Managers;
using Abstract;
using Printer;
using Cache;

namespace Player
{
    public class PlayerStackController : GameManager
    {
        private void Awake()
        {
            playerBag = transform.GetChild(1).transform;
            deskArea = GameObject.FindGameObjectWithTag(StringCache.DeskArea).transform;
        }
     
        private void OnEnable() => StartCoroutine(StartGameStates());

        private IEnumerator StartGameStates()
        {
            while (true)
            {
                if (onCollectArea)
                    CollectPaperFromTheArea();

                if (onDropArea) { DropThePaperOnTheArea(); }
                else { onDroppingPaper = false; }

                if (onMoneyArea && MoneyManager.moneyArea.childCount > 0)
                    GetMoney();

                yield return new WaitForSeconds(FixedDuration);
            }
        }
        
        private void CollectPaperFromTheArea() 
        {
            if (playerBag.childCount < StackLimit)
            {
                var paperList = PrintPaper.PaperList;
                var paper = paperList[^1];
                bagStackList.Add(paper);
                paperList.Remove(paper);
                paper.transform.SetParent(playerBag);

                var newPosition = Vector3.zero;
                paper.transform.localRotation = Quaternion.identity;
                paper.transform.DOLocalJump
                (
                    new Vector3(newPosition.x, newPosition.y + bagStackList.Count * Height , newPosition.z), 
                    1.0f, 1, FixedDuration
                ).SetEase(Ease.OutQuint);
                
                SoundManager.Instance.PlayCollectSound(SoundManager.Instance.collectSound, paper.transform.position);
            }
        }

        private void DropThePaperOnTheArea()
        {
            if (playerBag.childCount > 0)
            {
                onDroppingPaper = true;
                var lastIndex = playerBag.childCount - 1;
                var paper = bagStackList[lastIndex];
                paper.transform.SetParent(deskArea);
                bagStackList.Remove(paper);

                var deskAreaTransform = deskArea.transform;
                var targetPosition = deskAreaTransform.position;
                targetPosition.y += deskAreaTransform.childCount * Height;
                
                paper.transform.localRotation = Quaternion.identity;
                paper.transform.DOMove(targetPosition, FixedDuration).SetEase(Ease.OutQuint);
                
                SoundManager.Instance.PlayCollectSound(SoundManager.Instance.dropSound, paper.transform.position);
            }
        }

        private void GetMoney()
        {
            var moneyAreaTotalChild = MoneyManager.moneyArea.childCount;
            var moneyList = MoneyManager.MoneyList;

            if (moneyAreaTotalChild > 0)
            {
                var lastIndex = moneyAreaTotalChild - 1;
                var money = moneyList[lastIndex];
                
                money.transform.SetParent(MoneyManager.moneyBox);
                moneyList.Remove(money);
                money.SetActive(false);
                
                UIManager.Instance.UpdateCash(_cashAmount += 1);
                SoundManager.Instance.PlayCollectSound(SoundManager.Instance.moneySound, money.transform.position);
            }
        }
    }
}
