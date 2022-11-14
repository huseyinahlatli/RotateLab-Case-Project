using System.Collections;
using Abstract;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Employee
{
    public class EmployeeController : PaperManager
    {
        [SerializeField] private Transform employeeDeskArea;
        [SerializeField] private Transform employeeTrashArea;
        public static bool isEmployeeWorking;
        
        private const float WorkTime = 1.0f;
        private const float LoopTime = 1.1f;

        private void Start() => StartCoroutine(ReviewPapers());

        private IEnumerator ReviewPapers()
        {
            while (true)
            {
                if (employeeDeskArea.childCount > 0)
                {
                    Debug.Log("Amele is working!");
                    isEmployeeWorking = true;
                
                    var lastIndex = employeeDeskArea.childCount - 1;
                    var paper = employeeDeskArea.GetChild(lastIndex).gameObject;
                    var position = transform.position;
                    paper.transform.DOMoveY(position.y + 2.0f, WorkTime);
                    paper.transform.DOScale(Vector3.zero, WorkTime);
                    TrashIt(paper);
                }
                else { isEmployeeWorking = false; }
                yield return new WaitForSeconds(LoopTime);
            }
        }

        private void TrashIt(GameObject paper)
        {
            paper.transform.SetParent(employeeTrashArea);
            paper.SetActive(false);
        }
    }
}
