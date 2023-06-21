using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInfoScript : MonoBehaviour
{
    private bool panelIsVisible;

    private void Start()
    {
        panelIsVisible = false;
    }

    public void changeVisibilityControlsPanel()
    {
        panelIsVisible = !panelIsVisible;
        gameObject.SetActive(panelIsVisible);
    }
}
