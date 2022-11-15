using System.Collections;
using Abstract;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Employee
{
    public class EmployeeController : GameManager
    {
        [SerializeField] private GameObject moneyPrefab;
        [SerializeField] private Transform employeeDeskArea;
        [SerializeField] private Transform paperBox;
        public static bool isEmployeeWorking;

        private const float WorkTime = 1.0f;
        private const float TrashTime = 1.05f;
        private const float LoopTime = 1.1f;

        private void Start() => StartCoroutine(ReviewPapers());

        private IEnumerator ReviewPapers()
        {
            while (true)
            {
                if (employeeDeskArea.childCount > 0 && !onDroppingPaper && !onMoneyArea)
                {
                    isEmployeeWorking = true;
                
                    var lastIndex = employeeDeskArea.childCount - 1;
                    var paper = employeeDeskArea.GetChild(lastIndex).gameObject;
                    var position = paper.transform.position;
                    paper.transform.DOMoveY(position.y + 2.0f, WorkTime);
                    paper.transform.DOScale(Vector3.zero, WorkTime);
                    StartCoroutine(TrashIt(paper));
                }
                else { isEmployeeWorking = false; }
                yield return new WaitForSeconds(LoopTime);
            }
        }

        private IEnumerator TrashIt(GameObject paper)
        {
            yield return new WaitForSeconds(TrashTime);
            paper.transform.SetParent(paperBox);
            paper.SetActive(false);
            EarnMoney();
        }

        private void EarnMoney()
        {
            var moneyList = MoneyManager.MoneyList;
            var moneyAreaTransform = MoneyManager.moneyArea;
            var money = Instantiate(moneyPrefab, moneyAreaTransform.transform);
            moneyList.Add(money);
            
            var moneyPosition = money.transform.position;
            moneyPosition = new Vector3
            (
                moneyPosition.x,
                moneyAreaTransform.position.y + moneyList.Count * MoneyHeight,
                moneyPosition.z
            );
            money.transform.position = moneyPosition;
        }
    }
}
