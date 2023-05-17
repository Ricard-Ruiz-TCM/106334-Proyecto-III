using System;
using UnityEngine;

public abstract class BasicActor : Turnable {

    /** Callbacks */
    public Action onHealthChanged;
    /** --------- */

    [SerializeField, Header("Vida:")]
    protected bool _alive;
    [SerializeField]
    protected int _health;
    [SerializeField]
    protected int _maxHealth;
    /** Damage taken this turn */
    protected int _damageTaken;

    /** Getters */
    public bool isAlive() {
        return _alive;
    }
    public int health() {
        return _health;
    }
    public int maxHealth() {
        return _maxHealth;
    }
    public int damageTaken() {
        return _damageTaken;
    }
    public int healthPercent() {
        return (int)((_health / _maxHealth) * 100f);
    }

    /** Setters */
    /** Health [0 .. max] */
    public void setHealth(int value) {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
        onHealthChanged?.Invoke();
    }

    /** Abstracts para calculo del Daño total */
    public abstract int totalDamage();
    /** Abstracts para calculo de la Defensa total */
    public abstract int totalDefense();

    /** Abstract para calcular recibir daño */
    public virtual void takeDamage(int damage, itemID weapon = itemID.NONE) {
        _damageTaken = Mathf.Clamp(totalDefense() - damage, 0, _maxHealth);
        _health -= _damageTaken;

        onHealthChanged?.Invoke();

        // Check Death
        if (_health <= 0) {
            _alive = false;
            onActorDeath();
        }
    }

    /** Abstract para indicar que pasa cuando morimos */
    public abstract void onActorDeath();

}
