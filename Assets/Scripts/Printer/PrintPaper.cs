using System.Collections;
using System.Collections.Generic;
using Abstract;
using UnityEngine;
using DG.Tweening;

namespace Printer
{
    public class PrintPaper : MonoBehaviour
    {
        public static readonly List<GameObject> PaperList = new();
        [SerializeField] private GameObject paperPrefab;
        [SerializeField] private Transform pickArea;

        private const float Duration = .1f;
        private const int MaxPaper = 20;
        private const float Height = .05f;

        private void OnEnable() => StartCoroutine(PrintThePaper());

        private IEnumerator PrintThePaper()
        {
            while (true)
            {
                if (PaperList.Count < MaxPaper && !GameManager.onCollectArea)
                {
                    var paper = Instantiate(paperPrefab, pickArea.transform);
                    PaperList.Add(paper);
                    
                    paper.transform.DOJump
                    (
                        pickArea.transform.position + new Vector3(0.0f, PaperList.Count * Height, 0.0f)
                        , 1f, 1, Duration    
                    ).SetEase(Ease.OutQuint);
                }
                yield return new WaitForSeconds(Duration);
            }
        }
    }
}
