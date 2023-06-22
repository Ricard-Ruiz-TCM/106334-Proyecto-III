using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInfoScript : MonoBehaviour
{
    public void ShowControlPanel()
    {
        gameObject.SetActive(true);
    }

    public void HideControlPanel()
    {
        gameObject.SetActive(false);
    }
}
