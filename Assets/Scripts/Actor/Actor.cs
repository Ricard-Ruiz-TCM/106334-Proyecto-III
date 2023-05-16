using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(ActorMovement))]
[RequireComponent(typeof(ActorSkills))]
[RequireComponent(typeof(ActorStatus))]

public class Actor : BasicActor {

    public SOBox<perkID> _perks;
    protected ActorSkills _skills;
    protected ActorStatus _status;

    [SerializeField, Header("Movimiento:")]
    protected int _maxSteps;
    protected int _stepsDone;

    /** Getters */
    public int StepsRemain() {
        return _maxSteps - _stepsDone;
    }
    public int MaxSteps() {
        return _maxSteps;
    }

    /** Add de Steps */
    public void AddSteps(int amount) {
        _stepsDone -= amount;
    }

    // Grid Movement
    protected ActorMovement _gridMovement;
    public ActorMovement GridM() {
        return _gridMovement;
    }


    // callback
    public Action<Array2DEditor.nodeType> onStepReached;
    public Action onDestinationReached;

    // NavMesh Agent
    private NavMeshAgent _agent;

    public bool _canMove;

    // Destination path
    private int _index;
    public List<Node> VisualRouteValid;
    public List<Node> VisualRouteInvaild;
    private List<Node> _destionationRoute;

    // Unity Awake
    void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        VisualRouteValid = new List<Node>();
        VisualRouteInvaild = new List<Node>();
    }

    /** Establece el origen y el destino del movimiento */
    public void SetDestination(Vector3 origin, GridPlane plane, int amount) {
        _canMove = true;
        _index = -1;
        _destionationRoute = new List<Node>();
        List<Node> tmp = Stage.Pathfinder.FindPath(Stage.StageBuilder.GetGridPlane(origin).node, plane.node);
        for (int i = 0; i < Mathf.Min((tmp != null ? tmp.Count : 0), amount); i++) {
            if (tmp[i].type.Equals(Array2DEditor.nodeType.M)) {
                amount--;
            }
            if (tmp[i].type.Equals(Array2DEditor.nodeType.H)) {
                amount -= 2;
            }
            if (i < Mathf.Min(tmp.Count, amount)) {
                _destionationRoute.Add(tmp[i]);
            }
        }
        NextPoint();
    }

    public List<Node> CalcRoute(Vector3 origin, GridPlane plane, int amount = -1) {
        List<Node> route = Stage.Pathfinder.FindPath(Stage.StageBuilder.GetGridPlane(origin).node, plane.node);
        VisualRouteValid.Clear();
        VisualRouteInvaild.Clear();
        if (route == null)
            return route;

        if (amount != -1) {
            for (int i = 0; i < MathF.Min(amount, route.Count); i++) {
                if (route[i].type == Array2DEditor.nodeType.M) {
                    amount--;
                }
                if (route[i].type == Array2DEditor.nodeType.H) {
                    amount -= 2;
                }
                if (i < MathF.Min(amount, route.Count)) {
                    VisualRouteValid.Add(route[i]);
                }
            }
            for (int i = VisualRouteValid.Count; i < route.Count; i++) {
                VisualRouteInvaild.Add(route[i]);
            }
        } else {
            VisualRouteValid = route;
        }
        return route;
    }

    public int StepsRemain() {
        if (_destionationRoute == null)
            return 0;

        return _destionationRoute.Count - _index;
    }

    // Unity Update
    void Update() {
        if (!_canMove)
            return;

        if (DestinationReached())
            NextPoint();
    }

    /** Comprueba si hemos llegado al punto */
    public bool DestinationReached() {
        return !_agent.hasPath && !_agent.pathPending && _agent.pathStatus == NavMeshPathStatus.PathComplete;
    }

    /** Método para ir al siguiente nodo */
    private void NextPoint() {
        if (_index < _destionationRoute.Count - 1) {
            _index++;
            _agent.SetDestination(Stage.StageBuilder.GetGridPlane(_destionationRoute[_index]).position);
            onStepReached?.Invoke(Stage.StageBuilder.GetGridPlane(_destionationRoute[_index]).node.type);
        } else {
            _canMove = false;
            onDestinationReached?.Invoke();
        }
    }

    public Vector2 GetLastNode() {
        if (_destionationRoute == null)
            return Vector2.zero;
        return new Vector2(_destionationRoute[_destionationRoute.Count - 1].x, _destionationRoute[_destionationRoute.Count - 1].y);
    }


    #region Equipment:

    [SerializeField, Header("Equipo:")]
    protected ArmorInventoryItem _armor;
    [SerializeField]
    protected WeaponInventoryItem _weapon;
    [SerializeField]
    protected ShieldItem _shield;

    /** Getter de armadura */
    public ArmorItem Armor() {
        return _armor.armor;
    }

    /** Getter de escudo */
    public ShieldItem Shield() {
        return _shield;
    }

    /** Getter de arma */
    public WeaponItem Weapon() {
        return _weapon.weapon;
    }

    #endregion

    protected virtual void Awake() {

        _perks = GetComponent<ActorPerks>();
        _skills = GetComponent<ActorSkills>();
        _status = GetComponent<ActorStatus>();
        _inventory = GetComponent<ActorInventory>();

        _gridMovement = GetComponent<ActorMovement>();
        _gridMovement.onStepReached += (Array2DEditor.nodeType t) => {
            _stepsDone++;
            if (t.Equals(Array2DEditor.nodeType.M)) {
                _stepsDone++;
            } else if (t.Equals(Array2DEditor.nodeType.H)) {
                _stepsDone += 2;
            }
        };
        materialDefault = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
        totalHealth = _health;
    }

    protected void BuildSkills() {
        if (_inventory.Weapon() != null) {
            _skills.AddSkill(skillID.Attack);
        }
        if (_inventory.Shield() != null) {
            _skills.AddSkill(skillID.Defense);
        }
        if (_inventory.Weapon() != null) {
            _skills.AddSkill(_inventory.Weapon().skill);
        }
        foreach (Perk pk in _perks.Perks()) {
            if (pk is SkillPerk) {
                _skills.AddSkill(((SkillPerk)pk).skill);
            }
        }
    }
    protected void AddWeaponToCharacter() {
        //GameObject weaponGameOject = GameObject.Instantiate(_inventory.Weapon().weaponPrefab);
        //weaponGameOject.transform.parent = weaponHolder;
    }

    public int TakeDamage(int damage, itemID weaponItem = itemID.NONE) {
        int newH = Mathf.Max(0, _health - Mathf.Max(0, (damage - Defense())));

        if (weaponItem.Equals(itemID.Bow)) {
            if (Status.isStatusActive(buffsID.ArrowProof)) {
                newH = _health;
            }
        }
        if (_status.isStatusActive(buffsID.Invencible)) {
            newH = _health;
        }

        healtDif = _health - newH;
        _health = newH;


        // Cal Result
        if (_health == 0) {
            _alive = false;
            UnSubscribeManger();
            Die();
        }

        return healtDif;
    }

    public override int TotalDamage() {
        throw new System.NotImplementedException();
    }

    public override int TotalDefense() {
        throw new System.NotImplementedException();
    }

    public override void Die() {
        throw new System.NotImplementedException();
    }

    public override bool Act() {
        throw new System.NotImplementedException();
    }

    public override void Move() {
        throw new System.NotImplementedException();
    }

    public int Damage() {
        int dmg = 0;

        // Get Values
        dmg += _inventory.Weapon().damage[0];

        // Apply Modifiers
        foreach (Perk pk in _perks.Perks()) {
            if (pk is ModPerk) {
                if (pk.modType.Equals(modType.damage)) {
                    dmg += (int)((ModPerk)pk).value;
                }
            }
        }

        // check Status
        foreach (BuffItem sitem in _status.ActiveStatus) {
            if (sitem.buff is ModBuff) {
                if (((ModBuff)sitem.buff).type.Equals(modType.damage)) {
                    switch (((ModBuff)sitem.buff).operation) {
                        case modOperation.add:
                            dmg += ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.sub:
                            dmg -= ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.mult:
                            dmg *= ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.div:
                            dmg /= ((ModBuff)sitem.buff).value;
                            break;
                    }

                }
            }
        }

        return dmg;
    }

    public int Defense() {
        int defense = 0;

        // Get Values
        if (_inventory.Armor() != null)
            defense = _inventory.Armor().defense[0];

        // Apply Modifiers
        foreach (Perk pk in _perks.Perks()) {
            if (pk is ModPerk) {
                if (pk.modType.Equals(modType.defense)) {
                    defense += (int)((ModPerk)pk).value;
                }
            }
        }

        // check Status
        foreach (BuffItem sitem in _status.ActiveStatus) {
            if (sitem.buff is ModBuff) {
                if (((ModBuff)sitem.buff).type.Equals(modType.defense)) {
                    switch (((ModBuff)sitem.buff).operation) {
                        case modOperation.add:
                            defense += ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.sub:
                            defense -= ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.mult:
                            defense *= ((ModBuff)sitem.buff).value;
                            break;
                        case modOperation.div:
                            defense /= ((ModBuff)sitem.buff).value;
                            break;
                    }
                }
            }
        }

        return defense;
    }

    public override void TakeDamage() {
        throw new System.NotImplementedException();
    }
}
