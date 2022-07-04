using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        QuitKeys();
    }

    void QuitKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC pressed, quiting game.");
            Application.Quit();
            //This will only work in the actual build of the game.
        }
    }
}
