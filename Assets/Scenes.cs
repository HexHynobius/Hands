using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hyno
{
    public class Scenes : MonoBehaviour
    {
        public void ToGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
