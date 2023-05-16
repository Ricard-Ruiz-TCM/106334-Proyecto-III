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
    public int Health() {
        return _health;
    }
    public int MaxHealth() {
        return _maxHealth;
    }
    public int DamageTaken() {
        return _damageTaken;
    }
    public int HealthPercent() {
        return (int)((_health / _maxHealth) * 100f);
    }

    /** Setters */
    /** Health [0 .. max] */
    public void SetHealth(int value) {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
        onHealthChanged?.Invoke();
    }

    /** Abstracts para calculo del Daño total */
    public abstract int TotalDamage();
    /** Abstracts para calculo de la Defensa total */
    public abstract int TotalDefense();

    /** Abstract para calcular recibir daño */
    public virtual void TakeDamage(int damage, itemID weapon = itemID.NONE) {
        _damageTaken = Mathf.Clamp(TotalDefense() - damage, 0, _maxHealth);
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
