using System;
using Singleton;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private TextMeshProUGUI cashAmount;
        
        public void UpdateCash(int cash) => cashAmount.text = cash.ToString();
    }
}
