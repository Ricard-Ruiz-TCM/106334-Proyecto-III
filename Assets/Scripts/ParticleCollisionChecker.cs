using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionChecker : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    [SerializeField] GameObject prefab;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }


    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("hola");
    }
}
