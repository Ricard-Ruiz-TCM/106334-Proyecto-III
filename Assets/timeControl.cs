using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 3f; // Acelera el tiempo al doble (puedes ajustar este valor según tus necesidades)
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1f; // Restaura el tiempo normal
        }
    }
}
