using System.Collections.Generic;
using UnityEngine;

namespace Abstract
{
    public abstract class GameManager : MonoBehaviour
    {
        protected readonly List<GameObject> bagStackList = new();
        protected Transform playerBag;
        protected Transform deskArea;
        
        public static bool onCollectArea;
        protected static bool onDropArea;
        protected static bool onMoneyArea;
        protected static bool onDroppingPaper;
        
        protected const float FixedDuration = .2f;
        protected const float MoneyHeight = .1f;
        protected const float Height = .05f;
        protected const int StackLimit = 10;

        protected int _cashAmount = 0;
    }
}
