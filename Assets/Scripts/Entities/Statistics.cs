using System;
using UnityEngine;

[Serializable]
public class Statistics {

    public int Health;
    public int Defense;
    public bool IsAlive;

    [Header("Movement:")]
    public int Movement;

    // Constructor
    public Statistics(int health, int defense, int movemet) {
        Health = health;
        Defense = defense;
        Movement = movemet;
    }

    public void TakeDamage(int damage) {
        Health = Mathf.Max(0, Health -= Mathf.Max(0, (damage - Defense)));
        if (Health == 0) {
            IsAlive = false;
        }
    }

}
