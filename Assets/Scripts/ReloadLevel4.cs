using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadLevel4 : MonoBehaviour
{

    // Use this for restart level3
    public void restartbutton4()
    {
        SceneManager.LoadScene("GameLevel_4.f");
        T_RegCmdCtrl.tutorialWon = false;
    }
}
