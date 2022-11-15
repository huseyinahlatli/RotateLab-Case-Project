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
            printPaperArea = GameObject.FindGameObjectWithTag(StringCache.PrintPaper).transform;
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
                    StartCoroutine(GetMoney());

                yield return new WaitForSeconds(FixedDuration);
            }
        }
        
        private void CollectPaperFromTheArea() 
        {
            if (playerBag.childCount < StackLimit && printPaperArea.childCount > 0)
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
                ).SetEase(Ease.OutQuad);
                
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

        private IEnumerator GetMoney()
        {
            var moneyAreaTotalChild = MoneyManager.moneyArea.childCount;
            var moneyList = MoneyManager.MoneyList;

            if (moneyAreaTotalChild > 0)
            {
                var lastIndex = moneyAreaTotalChild - 1;
                var money = moneyList[lastIndex];
                var position = transform.position;

                money.transform.SetParent(MoneyManager.moneyBox);
                money.transform.DOMove(new Vector3(position.x, position.y + 1.0f, position.z), Duration);
                money.transform.DOScale(Vector3.zero, Duration);
                
                UIManager.Instance.UpdateCash(cashAmount += 1);
                SoundManager.Instance.PlayCollectSound(SoundManager.Instance.moneySound, money.transform.position);
                yield return new WaitForSeconds(Duration + .05f);
                
                moneyList.Remove(money);
                money.SetActive(false);
            }
        }
    }
}
