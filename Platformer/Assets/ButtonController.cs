using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {
    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
    }

}
