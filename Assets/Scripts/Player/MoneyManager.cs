using System.Collections.Generic;
using Abstract;
using UnityEngine;

namespace Player
{
    public class MoneyManager : GameManager
    {
        public static readonly List<GameObject> MoneyList = new();
        public static Transform moneyArea;
        public static Transform moneyBox;
        
        private void Awake()
        {
            moneyArea = GameObject.FindGameObjectWithTag(StringCache.MoneyArea).transform;
            moneyBox = GameObject.FindGameObjectWithTag(StringCache.MoneyBox).transform;
        }    
    }
}
