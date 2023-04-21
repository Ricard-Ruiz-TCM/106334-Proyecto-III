using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnemyMovement()
    {
        yield return new WaitForSeconds(5f);



        StartCoroutine(EnemyMovement());
    }
}
