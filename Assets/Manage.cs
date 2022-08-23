using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Hyno
{
    public class Manage : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("牌庫圖案")]
        private Sprite[] sprArray;

        [SerializeField, Header("初始牌庫數量"), Range(0, 300)]
        private int initialbox = 53;

        private List<CardBess> box = new List<CardBess>();

        private List<CardBess> myBox = new List<CardBess>();


        [SerializeField, Header("初始金額"), Range(0, 10000)]
        private int moneyV = 1000;
        [SerializeField, Header("初始金額文字")]
        private TextMeshProUGUI money;

        [SerializeField, Header("下注間距")]
        private int betGap = 50;

        int betV = 100;
        [SerializeField, Header("初始下注文字")]
        private TextMeshProUGUI bet;
        
        [SerializeField, Header("提示文字")]
        private TextMeshProUGUI hint;

        private int stage = 0;

        [SerializeField]
        GameObject card0;
        [SerializeField]
        GameObject card1;
        [SerializeField]
        GameObject card2;
        [SerializeField]
        GameObject card3;
        [SerializeField]
        GameObject card4;

        GameObject frame0;
        GameObject frame1;
        GameObject frame2;
        GameObject frame3;
        GameObject frame4;

        BoxCollider2D collider0;
        BoxCollider2D collider1;
        BoxCollider2D collider2;
        BoxCollider2D collider3;
        BoxCollider2D collider4;

        [SerializeField]
        Button nextButton;

        [SerializeField, Header("確認按鈕文字")]
        private TextMeshProUGUI nextText;

        [SerializeField]
        Button plusButton;
        [SerializeField]
        Button subButton;

        [SerializeField, Header("牌型面板")]
        GameObject hand;
        [SerializeField, Header("牌型文字")]
        private TextMeshProUGUI handText;

        #endregion

        #region 事件

        private void Awake()
        {
            frame0 = card0.transform.GetChild(0).gameObject;
            frame1 = card1.transform.GetChild(0).gameObject;
            frame2 = card2.transform.GetChild(0).gameObject;
            frame3 = card3.transform.GetChild(0).gameObject;
            frame4 = card4.transform.GetChild(0).gameObject;

            collider0 = card0.GetComponent<BoxCollider2D>();
            collider1 = card1.GetComponent<BoxCollider2D>();
            collider2 = card2.GetComponent<BoxCollider2D>();
            collider3 = card3.GetComponent<BoxCollider2D>();
            collider4 = card4.GetComponent<BoxCollider2D>();

        }
        private void Start()
        {

            API.ResetBox(box, initialbox);
            API.TranCard(box, myBox, 5);
        }


        private void FixedUpdate()
        {
            switch (stage)
            {
                case 0:
                    API.UpDownCard(card0, sprArray[0]);
                    API.UpDownCard(card1, sprArray[0]);
                    API.UpDownCard(card2, sprArray[0]);
                    API.UpDownCard(card3, sprArray[0]);
                    API.UpDownCard(card4, sprArray[0]);

                    break;
                case 1:
                    API.FlipCard(card0, sprArray[myBox[0].Front]);
                    API.FlipCard(card1, sprArray[myBox[1].Front]);
                    API.FlipCard(card2, sprArray[myBox[2].Front]);
                    API.FlipCard(card3, sprArray[myBox[3].Front]);
                    API.FlipCard(card4, sprArray[myBox[4].Front]);
                    break;
                case 2:
                    if (!card0.GetComponent<CardHold>().holdOn) { API.UpDownCard(card0, sprArray[0]); }
                    if (!card1.GetComponent<CardHold>().holdOn) { API.UpDownCard(card1, sprArray[0]); }
                    if (!card2.GetComponent<CardHold>().holdOn) { API.UpDownCard(card2, sprArray[0]); }
                    if (!card3.GetComponent<CardHold>().holdOn) { API.UpDownCard(card3, sprArray[0]); }
                    if (!card4.GetComponent<CardHold>().holdOn) { API.UpDownCard(card4, sprArray[0]); }
                    break;
                case 3:
                    if (card0.transform.position.y == 0) { API.FlipCard(card0, sprArray[myBox[0].Front]); }
                    if (card0.transform.position.y == 0) { API.FlipCard(card1, sprArray[myBox[1].Front]); }
                    if (card0.transform.position.y == 0) { API.FlipCard(card2, sprArray[myBox[2].Front]); }
                    if (card0.transform.position.y == 0) { API.FlipCard(card3, sprArray[myBox[3].Front]); }
                    if (card0.transform.position.y == 0) { API.FlipCard(card4, sprArray[myBox[4].Front]); }
                    break;
            }

            RunNumber();
        }
        #endregion

        #region 方法

        public void NextStage()
        {
            stage += 1;
            switch (stage)
            {
                case 0:
                    nextText.text = "開始";
                    hint.text = "請選擇下注金額";
                    nextButton.interactable = false;
                    Invoke("NextButtueON", 1.4f);

                    plusButton.interactable = true;
                    subButton.interactable = true;

                    hand.SetActive(false);

                    API.ResetBox(myBox, 0);
                    API.ResetBox(box, initialbox);
                    API.TranCard(box, myBox, 5);

                    //myBox[0].Suit = 4;
                    //myBox[0].Number = 1;

                    break;
                case 1:
                    nextText.text = "確定";
                    hint.text = "請選擇要保留的牌";
                    nextButton.interactable = false;
                    plusButton.interactable = false;
                    subButton.interactable = false;

                    Invoke("NextButtueON", 1.2f);
                    break;
                case 2:
                    nextText.text = "再一局";
                    hint.text = "結算結果";
                    nextButton.interactable = false;

                    frame0.SetActive(false);
                    frame1.SetActive(false);
                    frame2.SetActive(false);
                    frame3.SetActive(false);
                    frame4.SetActive(false);

                    collider0.enabled = false;
                    collider1.enabled = false;
                    collider2.enabled = false;
                    collider3.enabled = false;
                    collider4.enabled = false;

                    if (!card0.GetComponent<CardHold>().holdOn) { API.ChangeCard(box, myBox[0]); }
                    if (!card1.GetComponent<CardHold>().holdOn) { API.ChangeCard(box, myBox[1]); }
                    if (!card2.GetComponent<CardHold>().holdOn) { API.ChangeCard(box, myBox[2]); }
                    if (!card3.GetComponent<CardHold>().holdOn) { API.ChangeCard(box, myBox[3]); }
                    if (!card4.GetComponent<CardHold>().holdOn) { API.ChangeCard(box, myBox[4]); }

                    Invoke("NextStageUP", 1.5f);
                    break;
                case 3:
                    Invoke("NextStageUP", 1.2f);
                    Invoke("NextButtueON", 1.2f);
                    break;
                case 4:
                    BetMoney(API.JokerIfCatHand(myBox));
                    hand.SetActive(true);

                    card0.GetComponent<CardHold>().holdOn = false;
                    card1.GetComponent<CardHold>().holdOn = false;
                    card2.GetComponent<CardHold>().holdOn = false;
                    card3.GetComponent<CardHold>().holdOn = false;
                    card4.GetComponent<CardHold>().holdOn = false;

                    collider0.enabled = false;
                    collider1.enabled = false;
                    collider2.enabled = false;
                    collider3.enabled = false;
                    collider4.enabled = false;

                    if (moneyV <= 0)
                    {
                        SceneManager.LoadScene(2);
                    }

                    if (betV > moneyV)
                    {
                        betV = moneyV;
                        bet.text = money.text;
                    }

                    stage = -1;
                    break;
            }

        }


        #region UI

        public void NextStageUP()
        {
            NextStage();
        }

        public void NextButtueON()
        {
            nextButton.interactable = true;
        }

        /// <summary>
        /// 增加下注
        /// </summary>
        public void PlusBet()
        {
            if (betV >= moneyV) { return; }
            else
            {
                betV += betGap;
                bet.text = betV.ToString();
            }
        }

        /// <summary>
        /// 減少下注
        /// </summary>
        public void SubBet()
        {
            if (betV == betGap) { return; }
            else
            {
                betV -= betGap;
                bet.text = betV.ToString();
            }
        }

        /// <summary>
        /// 金錢數字跳動
        /// </summary>
        public void RunNumber()
        {
            if (moneyV > int.Parse(money.text))
            {
                money.text = (int.Parse(money.text) + 1).ToString();
            }
            else if (moneyV < int.Parse(money.text))
            {
                money.text = (int.Parse(money.text) - 1).ToString();
            }
        }

        private void BetMoney(int hand)
        {
            switch (hand)
            {
                case 1:
                    handText.text = "皇家同花順";
                    moneyV += betV * 50;
                    break;
                case 2:
                    handText.text = "同花順";
                    moneyV += betV * 30;
                    break;
                case 3:
                    handText.text = "四條";
                    moneyV += betV * 15;
                    break;
                case 4:
                    handText.text = "葫蘆";
                    moneyV += betV * 10;
                    break;
                case 5:
                    handText.text = "同花";
                    moneyV += betV * 8;
                    break;
                case 6:
                    handText.text = "順子";
                    moneyV += betV * 5;
                    break;
                case 7:
                    handText.text = "三條";
                    moneyV += betV * 2;
                    break;
                case 8:
                    handText.text = "兩對";
                    moneyV += betV * 2;
                    break;
                case 9:
                    handText.text = "對子";
                    moneyV -= betV * 1;
                    break;
                case 10:
                    handText.text = "散牌";
                    moneyV -= betV * 1;
                    break;
            }
        }

        #endregion

        #endregion
    }
}
