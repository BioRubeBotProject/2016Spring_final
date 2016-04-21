using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadLevel2 : MonoBehaviour
{

    // Use this for restart level2
    public void restartbutton2()
    {
        SceneManager.LoadScene("GameLevel_2.f");
        G_ProteinCmdCtrl.gameWon = false;
    }
}
