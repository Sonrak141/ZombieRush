using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    public void cargeScene(string nameScene)
    {
        Application.LoadLevel(nameScene);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Menu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void PauseClose()
    {
        Menu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
