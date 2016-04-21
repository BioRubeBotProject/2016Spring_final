using UnityEngine;
using System.Collections;
using System;

public class disabletutorial9 : MonoBehaviour
{
    public Rigidbody test;
    static GameObject[] gratsPanel;

    // Use this for initialization

    void Start()
    {
        gratsPanel = GameObject.FindGameObjectsWithTag("TutorialFinish");
        bool isGameBegin = true;


        if (isGameBegin == true)
        {
            // Set active congratulations to ative. Unity does not support activating so had to update
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("TutorialFinish"))
                obj.SetActive(false);
        }
        isGameBegin = false;
    }

    private Rigidbody GetCompnent<T>()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        if (T_RegCmdCtrl.tutorialWon == true)
            // Set active congratulations to ative. Unity does not support activating so had to update
            foreach (GameObject obj2 in gratsPanel)
                obj2.SetActive(true);

    }
}
