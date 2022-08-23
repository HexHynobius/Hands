using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyno
{
    public class API : MonoBehaviour
    {
        public static void UpDownCard(GameObject card, Sprite sprite)
        {
            SpriteRenderer spr = card.GetComponent<SpriteRenderer>();


            if (spr.sprite == sprite)
            {
                card.transform.position = Vector3.Lerp(card.transform.position, new Vector3(card.transform.position.x, 0), 0.15f);
                if (card.transform.position.y < 0.01f)
                {
                    card.transform.position = new Vector3(card.transform.position.x, 0);
                }
            }
            else if (card.transform.position.y > 9.9f)
            {
                card.transform.rotation = Quaternion.identity;
                spr.sprite = sprite;
                spr.flipX = false;
            }
            else
            {
                card.transform.position = Vector3.Lerp(card.transform.position, new Vector3(card.transform.position.x, 10), 0.15f);
            }
        }




        public static void FlipCard(GameObject card, Sprite sprite)
        {
            SpriteRenderer spr = card.GetComponent<SpriteRenderer>();
            BoxCollider2D collider = card.GetComponent<BoxCollider2D>();
            Quaternion rot = Quaternion.AngleAxis(3, Vector3.up * 2);

            if (card.transform.eulerAngles.y >= 180)
            {
                collider.enabled = true;
            }
            else if (card.transform.eulerAngles.y > 90)
            {
                card.transform.rotation *= rot;

                spr.sprite = sprite;
                spr.flipX = true;
            }
            else
            {
                card.transform.rotation *= rot;
            }
        }

        /// <summary>
        /// print牌庫
        /// </summary>
        /// <param name="box"></param>
        public static void ShowElements(List<CardBess> box)
        {
            for (int i = 0; i < box.Count; i++)
            {
                print($"第{i + 1}個=" + box[i].Number + " " + box[i].Suit);
            }
        }

        /// <summary>
        /// 清空重置牌庫
        /// </summary>
        /// <param name="box"></param>
        /// <param name="quantity"></param>
        public static void ResetBox(List<CardBess> box, int quantity)
        {
            box.Clear();

            for (int i = 0; i < quantity; i++)
            {
                box.Add(new CardBess());

                box[i].Number = (i % 13) + 1;
                box[i].Suit = i / 13;
            }
        }



        /// <summary>
        /// 牌庫傳牌庫
        /// </summary>
        /// <param name="sendBox"></param>
        /// <param name="receiveBox"></param>
        /// <param name="quantity"></param>
        public static void TranCard(List<CardBess> sendBox, List<CardBess> receiveBox, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                int rand = Random.Range(0, sendBox.Count);
                receiveBox.Add(sendBox[rand]);
                sendBox.RemoveAt(rand);
            }
        }

        /// <summary>
        /// 牌庫交換牌
        /// </summary>
        /// <param name="sendBox"></param>
        /// <param name="card"></param>
        public static void ChangeCard(List<CardBess> sendBox, CardBess card)
        {
            int rand = Random.Range(0, sendBox.Count);

            //card = sendBox[rand];
            card.Number = sendBox[rand].Number;
            card.Suit = sendBox[rand].Suit;
            sendBox.RemoveAt(rand);

        }


        /// <summary>
        /// 冒泡排序從小到大
        /// </summary>
        /// <param name="box"></param>
        public static void Sort(List<CardBess> box)
        {
            CardBess temp = new CardBess();

            for (int i = 0; i < box.Count - 1; i++)
            {
                for (int j = 0; j < box.Count - 1 - i; j++)
                {
                    if (box[j].Number + box[j].Suit * 13 > box[j + 1].Number + box[j].Suit * 13)
                    {
                        temp = box[j + 1];
                        box[j + 1] = box[j];
                        box[j] = temp;
                    }
                }
            }
        }

        public static int IfCatHand(List<CardBess> box)
        {
            Sort(box);

            if (box[0].Suit == box[1].Suit && box[0].Suit == box[2].Suit && box[0].Suit == box[3].Suit && box[0].Suit == box[4].Suit)
            {
                if (box[0].Number == 1 && box[1].Number == 10 && box[2].Number == 11 && box[3].Number == 12 && box[4].Number == 13)
                { return 1; }//皇家同花順
                else if (box[0].Number + 1 == box[1].Number && box[0].Number + 2 == box[2].Number && box[0].Number + 3 == box[3].Number && box[0].Number + 4 == box[4].Number)
                { return 2; }//同花順
                else
                { return 5; }//同花
            }
            else if (box[0].Number == box[1].Number && box[0].Number == box[2].Number && box[0].Number == box[3].Number)
            { return 3; }//四條
            else if (box[1].Number == box[2].Number && box[1].Number == box[3].Number && box[1].Number == box[4].Number)
            { return 3; }//四條
            else if (box[0].Number == box[1].Number && box[0].Number == box[2].Number && box[3].Number == box[4].Number)
            { return 4; }//葫蘆
            else if (box[0].Number == box[1].Number && box[2].Number == box[3].Number && box[2].Number == box[4].Number)
            { return 4; }//葫蘆
            else if (box[0].Number == 1 && box[1].Number == 10 && box[2].Number == 11 && box[3].Number == 12 && box[4].Number == 13)
            { return 6; }//順子
            else if (box[0].Number + 1 == box[1].Number && box[0].Number + 2 == box[2].Number && box[0].Number + 3 == box[3].Number && box[0].Number + 4 == box[4].Number)
            { return 6; }//順子
            else if (box[0].Number == box[1].Number && box[0].Number == box[2].Number)
            { return 7; }//三條
            else if (box[1].Number == box[2].Number && box[1].Number == box[3].Number)
            { return 7; }//三條
            else if (box[2].Number == box[3].Number && box[2].Number == box[4].Number)
            { return 7; }//三條
            else if (box[0].Number == box[1].Number && box[2].Number == box[3].Number)
            { return 8; }//兩對
            else if (box[0].Number == box[1].Number && box[3].Number == box[4].Number)
            { return 8; }//兩對
            else if (box[1].Number == box[2].Number && box[3].Number == box[4].Number)
            { return 8; }//兩對
            else if (box[0].Number == box[1].Number)
            { return 9; }//一對
            else if (box[1].Number == box[2].Number)
            { return 9; }//一對
            else if (box[2].Number == box[3].Number)
            { return 9; }//一對
            else if (box[3].Number == box[4].Number)
            { return 9; }//一對
            else
            { return 10; }//散牌
        }



        /// <summary>
        /// 判斷牌型 五張限定 方法有問題要修改
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public static int CatHand(List<CardBess> box)
        {
            Sort(box);

            /*檢查用
            if (box[0].Suit == box[1].Suit && box[0].Suit == box[2].Suit && box[0].Suit == box[3].Suit && box[0].Suit == box[4].Suit) { print(1); }
            if (box[0].Number + 1 == box[1].Number && box[0].Number + 2 == box[2].Number && box[0].Number + 3 == box[3].Number && box[0].Number + 4 == box[4].Number) { print(2); }
            if (box[0].Number == 1 && box[1].Number == 10 && box[2].Number == 11 && box[3].Number == 12 && box[4].Number == 13) { print(3); }
            if (box[0].Number == box[1].Number) { print(4); }
            if (box[0].Number == box[2].Number) { print(5); }
            if (box[0].Number == box[3].Number) { print(6); }
            if (box[1].Number == box[2].Number) { print(7); }
            if (box[1].Number == box[3].Number) { print(8); }
            if (box[1].Number == box[4].Number) { print(9); }
            if (box[2].Number == box[3].Number) { print(10); }
            if (box[2].Number == box[4].Number) { print(11); }
            if (box[3].Number == box[4].Number) { print(12); }
            */

            switch (box[0].Suit == box[1].Suit && box[0].Suit == box[2].Suit && box[0].Suit == box[3].Suit && box[0].Suit == box[4].Suit,
                box[0].Number + 1 == box[1].Number && box[0].Number + 2 == box[2].Number && box[0].Number + 3 == box[3].Number && box[0].Number + 4 == box[4].Number,
                box[0].Number == 1 && box[1].Number == 10 && box[2].Number == 11 && box[3].Number == 12 && box[4].Number == 13,
                box[0].Number == box[1].Number, box[0].Number == box[2].Number, box[0].Number == box[3].Number,
                box[1].Number == box[2].Number, box[1].Number == box[3].Number, box[1].Number == box[4].Number,
                box[2].Number == box[3].Number, box[2].Number == box[4].Number,
                box[3].Number == box[4].Number
                )
            {
                //(相同花色,順子,順子例外,0==1,0==2,0==3,1==2,1==3,1==4,2==3,2==4,3==4)
                case (true, false, true, false, false, false, false, false, false, false, false, false):
                    return 1;

                case (true, true, false, false, false, false, false, false, false, false, false, false):
                    return 2;

                case (false, false, false, true, true, true, false, false, false, false, false, false):
                    return 3;

                case (false, false, false, false, false, false, true, true, true, false, false, false):
                    return 3;

                case (false, false, false, true, true, false, false, false, false, false, false, true):
                    return 4;

                case (false, false, false, true, false, false, false, false, false, true, true, false):
                    return 4;

                case (true, false, false, false, false, false, false, false, false, false, false, false):
                    return 5;

                case (false, true, false, false, false, false, false, false, false, false, false, false):
                    return 6;

                case (false, false, true, false, false, false, false, false, false, false, false, false):
                    return 6;

                case (false, false, false, true, true, false, false, false, false, false, false, false):
                    return 7;

                case (false, false, false, false, false, false, true, true, false, false, false, false):
                    return 7;

                case (false, false, false, false, false, false, false, false, false, true, true, false):
                    return 7;

                case (false, false, false, true, false, false, false, false, false, true, false, false):
                    return 8;

                case (false, false, false, false, false, false, true, false, false, false, false, true):
                    return 8;

                case (false, false, false, true, false, false, false, false, false, false, false, false):
                    return 9;

                case (false, false, false, false, false, false, true, false, false, false, false, false):
                    return 9;

                case (false, false, false, false, false, false, false, false, false, true, false, false):
                    return 9;

                case (false, false, false, false, false, false, false, false, false, false, false, true):
                    return 9;

                default:
                    return 10;
            }
        }

        public static int JokerIfCatHand(List<CardBess> box)
        {
            int catHands = 99;
            List<CardBess> boxSort = new List<CardBess>();
            for (int i = 0; i < box.Count; i++)
            {
                boxSort.Add(new CardBess());
            }

            for (int i = 0; i < box.Count; i++)
            {
                if (box[i].Front == 53)
                {
                    for (int j = 0; j <= 3; j++)
                    {
                        for (int k = 1; k <= 13; k++)
                        {
                            for (int n = 0; n < box.Count; n++)
                            {
                                boxSort[n].Number = box[n].Number;
                                boxSort[n].Suit = box[n].Suit;
                            }
                            boxSort[i].Suit = j;
                            boxSort[i].Number = k;
                            if (IfCatHand(boxSort) < catHands)
                            {
                                catHands = IfCatHand(boxSort);
                            }
                        }
                    }
                }
                else
                {
                    for (int n = 0; n < box.Count; n++)
                    {
                        boxSort[n].Number = box[n].Number;
                        boxSort[n].Suit = box[n].Suit;
                    }
                    if (IfCatHand(boxSort) < catHands)
                    {
                        catHands = IfCatHand(boxSort);
                    }
                }
            }
            return catHands;
        }

        public static void MakeCard(GameObject card, int quantity, float start)
        {
            float cutX = (Mathf.Abs(start) * 2) / (quantity - 1);

            for (int i = 0; i < quantity; i++)
            {
                float postX = (start + (cutX * i));

                card.name = $"Card{i}";
                Instantiate(card, new Vector3(postX, 10), Quaternion.identity);
            }
        }


    }
}
