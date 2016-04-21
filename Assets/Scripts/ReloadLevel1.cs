using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadLevel1 : MonoBehaviour
{

    // Use this for restart level1
    public void restartbutton1()
    {
        SceneManager.LoadScene("GameLevel_1.f");
        tutorialreceptorScript.gameWon = false;
    }
}
