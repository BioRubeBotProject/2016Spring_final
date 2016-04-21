using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadFree : MonoBehaviour
{

    // Use this for reset all levels when pressing new game from main menu
    public void resetfreelevels()
    {
        SceneManager.LoadScene("Models -Freeplay level");
    }
}
