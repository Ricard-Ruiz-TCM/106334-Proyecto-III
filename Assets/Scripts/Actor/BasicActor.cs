using System;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BasicActor : Turnable {

    /** Callbacks */
    public static event Action onChangeHealth;

    public static event Action onStartMovement;
    public static event Action onStartAct;

    public static event Action onEndMovement;
    public static event Action onEndAct;

    public static event Action onReAct;

    /** --------- */

    protected bool _alive = true;
    [SerializeField, Header("Vida:")]
    protected int _health;
    protected int _maxHealth;
    /** Damage taken this turn */
    protected int _damageTaken;

    [SerializeField]
    private bool _canAttack;

    [SerializeField] GameObject entitieUIPrefab;
    public GameObject entitieUI;
    [SerializeField] float height;

    public SkinnedMeshRenderer skinnedMesh;
    public Material[] skinnedMaterials;

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
    public bool CanAttack() {
        return _canAttack;
    }
    public int healthPercent() {
        return (int)(((float)_health / (float)_maxHealth) * 100f);
    }

    public void disableAttack() {
        _canAttack = false;
    }

    /** Override del start */
    protected override void Start() 
    {
        if(skinnedMesh != null)
        {
            skinnedMaterials = new Material[skinnedMesh.materials.Length];
            skinnedMaterials = skinnedMesh.materials;
        }       
        _maxHealth = _health;
        Stage.Grid.changeNodeType(transform.position, Array2DEditor.nodeType.X);

        if(entitieUIPrefab != null)
        {
            entitieUI = Instantiate(entitieUIPrefab, new Vector3(transform.position.x, transform.position.y + height, transform.position.z), Quaternion.identity);
            entitieUI.transform.SetParent(transform);
        }
        base.Start();
    }

    /** Setters */
    /** Health [0 .. max] */
    public void setHealth(int value) {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
        onChangeHealth?.Invoke();
    }

    /** Override para los cambios de nodo al movernos */
    public override void beginTurn() {
        _canAttack = true;
        Stage.Grid.changeNodeType(transform.position, Array2DEditor.nodeType.__);
        base.beginTurn();
    }
    public override void endTurn() {
        Stage.Grid.changeNodeType(transform.position, Array2DEditor.nodeType.X);
        base.endTurn();
    }

    /** Abstracts para calculo del Daño total */
    public abstract int totalDamage();
    /** Abstracts para calculo de la Defensa total */
    public abstract int totalDefense();

    /** Abstract para calcular recibir daño */
    public virtual void takeDamage(BasicActor from, int damage, itemID weapon = itemID.NONE) {

        _damageTaken = Mathf.Clamp(damage - totalDefense(), 0, _maxHealth);
        _health -= _damageTaken;

        InstantPopUp(-_damageTaken);

        if(entitieUI != null)
        {
            entitieUI.GetComponent<EntitieUI>().SetDamage((float)_damageTaken / (float)_maxHealth);
        }      

        onChangeHealth?.Invoke();

        // Check Death
        if (_health <= 0) {
            _alive = false;
            onActorDeath();
        }

    }

    public virtual void heal(int amount) {

        InstantPopUp(amount);

        _health = Mathf.Clamp(_health + amount, 0, _maxHealth);
        onChangeHealth?.Invoke();

    }

    private void InstantPopUp(int amount) {
        GameObject popUp = Instantiate(Resources.Load<GameObject>("Prefabs/PopUpTextPrefab"), new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        popUp.transform.LookAt(FindObjectOfType<Camera>().transform.position, Vector3.up);
        popUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = (amount > 0 ? "+" : "-") + Mathf.Abs(amount);
        Destroy(popUp, 1f);
    }

    /** Override reAct pra el observer */
    public override void reAct() {
        base.reAct();
        onReAct?.Invoke();
    }

    /** Override del endMovement para limpiar Grid */
    public override void endMovement() {
        base.endMovement();
        onEndMovement?.Invoke();
    }

    /** Override del endAction */
    public override void endAction() {
        base.endAction();
        onEndAct?.Invoke();
    }

    /** Override del startAct */
    public override void startAct() {
        base.startAct();
        onStartAct?.Invoke();
    }

    /** Override dle startMove para el observer */
    public override void startMove() {
        base.startMove();
        onStartMovement?.Invoke();
    }

    /** Abstract para indicar que pasa cuando morimos */
    public virtual void onActorDeath() {
        TurnManager.instance.unsubscribe(this);
        this.enabled = false;
    }

}
