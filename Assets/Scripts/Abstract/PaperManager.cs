using System.Collections.Generic;
using DG.Tweening;
using Printer;
using UnityEngine;

namespace Abstract
{
    public abstract class PaperManager : MonoBehaviour
    {
        protected readonly List<GameObject> bagStackList = new();

        [Header("Player Collision Settings")]
        protected Transform playerBag;
        protected Transform deskArea;
        public static bool onCollectArea;
        public static bool onDropArea;
        protected const float Duration = .2f;
        private const float Height = .05f;
        private const int StackLimit = 10;

        // 3 --> Fonksiyon çağırıldı | Liste Dolu | PickArea Child'ler dolu | Toplama işlemi yapilacak.
        protected void CollectPaperFromTheArea() 
        {
            if (playerBag.childCount < StackLimit) // En fazla 10 nesne alabilir
            {
                var paperList = PrintPaper.PaperList;
                var paper = paperList[^1];
                bagStackList.Add(paper); // çantaya listesine ekle
                paperList.Remove(paper); // genel listeden çikart
                paper.transform.SetParent(playerBag);

                var newPosition = Vector3.zero;
                paper.transform.localRotation = Quaternion.identity;
                paper.transform.DOLocalJump
                (
                    new Vector3(newPosition.x, newPosition.y + (bagStackList.Count * Height) , newPosition.z), 1.0f, 1, Duration
                ).SetEase(Ease.OutQuint);
            }
        }

        protected void DropThePaperOnTheArea()
        {
            if (playerBag.childCount > 0)
            {
                var lastIndex = playerBag.childCount - 1;
                var paper = bagStackList[lastIndex];
                paper.transform.SetParent(deskArea);
                bagStackList.Remove(paper);

                var deskAreaTransform = deskArea.transform;
                var targetPosition = deskAreaTransform.position;
                targetPosition.y += deskAreaTransform.childCount * Height;
                
                paper.transform.localRotation = Quaternion.identity;
                paper.transform.DOMove(targetPosition, Duration).SetEase(Ease.OutQuint);
            }
        }
    }
}
