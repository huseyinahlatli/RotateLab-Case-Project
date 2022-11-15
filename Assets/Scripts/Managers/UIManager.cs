using UnityEngine;
using Singleton;
using TMPro;

namespace Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private TextMeshProUGUI cashAmount;
        
        public void UpdateCash(int cash) => cashAmount.text = cash.ToString();
    }
}
