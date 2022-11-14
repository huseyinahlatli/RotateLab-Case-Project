using System.Collections;
using UnityEngine;

namespace Employee
{
    public class EmployeeController : MonoBehaviour
    {
        public static bool isEmployeeWorking;

        private void Start() => StartCoroutine(ReviewPapers());

        private IEnumerator ReviewPapers()
        {
            while (true)
            {
                
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
