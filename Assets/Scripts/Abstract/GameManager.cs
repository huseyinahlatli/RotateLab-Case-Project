using System.Collections.Generic;
using DG.Tweening;
using Player;
using Printer;
using UnityEngine;

namespace Abstract
{
    public abstract class GameManager : MonoBehaviour
    {
        private readonly List<GameObject> bagStackList = new();
        protected Transform playerBag;
        protected Transform deskArea;
        
        public static bool onCollectArea;
        protected static bool onDropArea;
        protected static bool onMoneyArea;
        protected static bool onDroppingPaper;
        
        protected const float FixedDuration = .2f;
        protected const float MoneyHeight = .1f;
        private const float Height = .05f;
        private const int StackLimit = 10;

        protected void CollectPaperFromTheArea() 
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
            }
        }

        protected void DropThePaperOnTheArea()
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
            }
        }

        protected void GetMoney() // < IndexError > CS:74
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
            }
        }
    }
}
