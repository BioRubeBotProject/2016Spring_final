using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadLevel3 : MonoBehaviour
{

    // Use this for restart level3
    public void restartbutton3()
    {
        SceneManager.LoadScene("GameLevel_3.f");
        KinaseCmdCtrl.gameWon= false;
    }
}
