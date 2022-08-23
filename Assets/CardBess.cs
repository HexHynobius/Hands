using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyno
{
    /// <summary>
    /// 牌的數字花色
    /// </summary>
    public class CardBess
    {
        private int number;

        public int Number
        {
            get { return number; }

            set { number = value; }
        }

        private int suit;

        public int Suit
        {
            get { return suit; }

            set { suit = value; }// 梅花=0 , 方塊=1 , 愛心=2 , 黑桃=3 Joker=4
        }

        public int Back { get { return 0; } }
        public int Front { get { return number + (suit * 13); } }

    }
}
