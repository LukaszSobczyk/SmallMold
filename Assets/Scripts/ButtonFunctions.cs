using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour {

	public void LoadScene()
    {
        Application.LoadLevel("GraphicsScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
