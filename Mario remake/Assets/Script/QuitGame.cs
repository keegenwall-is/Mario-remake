using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitGame1()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}
