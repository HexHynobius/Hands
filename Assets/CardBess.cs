using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyno
{
    /// <summary>
    /// �P���Ʀr���
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

            set { suit = value; }// ����=0 , ���=1 , �R��=2 , �®�=3 Joker=4
        }

        public int Back { get { return 0; } }
        public int Front { get { return number + (suit * 13); } }

    }
}
