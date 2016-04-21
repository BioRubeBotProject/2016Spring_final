using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadAll : MonoBehaviour {

    // Use this for reset all levels when pressing new game from main menu
    public void resetallevels()
    {
        SceneManager.LoadScene("GameLevel_1.f");
        tutorialreceptorScript.gameWon = false;
        SceneManager.LoadScene("GameLevel_2.f");
        G_ProteinCmdCtrl.gameWon = false;
        SceneManager.LoadScene("GameLevel_3.f");
        KinaseCmdCtrl.gameWon = false;
        SceneManager.LoadScene("GameLevel_4.f");
        T_RegCmdCtrl.tutorialWon = false;
        SceneManager.LoadScene("GameLevel_5.f");
        T_RegCmdCtrl.gameWon = false;
    }
}
