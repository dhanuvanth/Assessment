using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }

    public void OpenBrowser()
    {
        Application.OpenURL("https://www.boxabl.com/reserve");
    }
}
