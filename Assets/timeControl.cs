using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeControl : MonoBehaviour
{
    private bool speedySoundHasBeenPlayed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!speedySoundHasBeenPlayed)
            {
                FMODManager.instance.PlayOneShot(FMODEvents.instance.PressButtonSpeed);
                speedySoundHasBeenPlayed = true;
            }
            Time.timeScale = 3f; // Acelera el tiempo al doble (puedes ajustar este valor según tus necesidades)
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (speedySoundHasBeenPlayed)
            {
                speedySoundHasBeenPlayed = false;
            }
            Time.timeScale = 1f; // Restaura el tiempo normal
        }
    }
}
