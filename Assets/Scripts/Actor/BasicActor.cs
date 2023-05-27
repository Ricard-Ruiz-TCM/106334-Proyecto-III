using System;
using UnityEngine;
using TMPro;

public abstract class BasicActor : Turnable {

    /** Callbacks */
    public static event Action onChangeHealth;
    /** --------- */

    protected bool _alive = true;
    [SerializeField, Header("Vida:")]
    protected int _health;
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

    /** Override del start */
    protected override void Start() {
        _maxHealth = _health;
        base.Start();
    }

    /** Setters */
    /** Health [0 .. max] */
    public void setHealth(int value) {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
        onChangeHealth?.Invoke();
    }

    /** Abstracts para calculo del Daño total */
    public abstract int totalDamage();
    /** Abstracts para calculo de la Defensa total */
    public abstract int totalDefense();

    /** Abstract para calcular recibir daño */
    public virtual void takeDamage(BasicActor from, int damage, itemID weapon = itemID.NONE) 
    {
        GameObject popUp = Instantiate(Resources.Load<GameObject>("Prefabs/PopUpTextPrefab"), new Vector3(transform.position.x,transform.position.y +2, transform.position.z), Quaternion.identity);
        popUp.transform.LookAt(FindObjectOfType<Camera>().transform.position, Vector3.up);
        popUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-" + damage;
        Destroy(popUp, 1f);

        _damageTaken = Mathf.Clamp(damage - totalDefense(), 0, _maxHealth);
        _health -= _damageTaken;

        onChangeHealth?.Invoke();

        // Check Death
        if (_health <= 0) {
            _alive = false;
            onActorDeath();
        }

    }

    /** Abstract para indicar que pasa cuando morimos */
    public abstract void onActorDeath();

}
