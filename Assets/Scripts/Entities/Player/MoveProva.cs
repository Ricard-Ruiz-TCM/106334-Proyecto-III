using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveProva : MonoBehaviour
{
    private NavMeshAgent _agent;
     
    List<Node> playerPath;
    int index = 0;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ResetPath(List<Node> path)
    {
        playerPath = path;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPath != null)
        {
            MoveProvaNormal();
            _agent.SetDestination(playerPath[index].worldPos);
        }

    }
    private void MoveProvaNormal()
    {
        if(transform.position.x == playerPath[index].gridObj.transform.position.x && transform.position.z == playerPath[index].gridObj.transform.position.z)
        {
            index++;
            if (index == playerPath.Count)
            {
                playerPath = null;
            }
        }
        else
        {
            //transform.position = Vector3.MoveTowards(transform.position, playerPath[index].gridObj.transform.position, 10 * Time.deltaTime);
        }
    }
}
