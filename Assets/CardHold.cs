using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyno
{
    public class CardHold : MonoBehaviour
    {
        public bool holdOn;
        
        GameObject frame;

        void Start()
        {
            frame = gameObject.transform.GetChild(0).gameObject;
        }

        private void FixedUpdate()
        {
            if (holdOn)
            {
                frame.transform.localScale = new Vector3(1, 1, 1) + new Vector3(0.05f, 0.05f, 0) * Mathf.Abs(Mathf.Sin(Time.time * 5));
            }
        }

        private void OnMouseDown()
        {
            if (!holdOn)
            {
                frame.SetActive(true);
                holdOn = true;
            }
            else if (holdOn)
            {
                frame.SetActive(false);
                holdOn = false;
            }
        }
    }
}
