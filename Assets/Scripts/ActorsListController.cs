using System;
using System.Collections.Generic;
using UnityEngine;

// Done
public class ActorsListController : MonoBehaviour {

    /** Callback */
    public Action onModifyList;

    [SerializeField, Header("List:")]
    protected int _current = 0;
    [SerializeField]
    protected List<Turnable> _actors = new List<Turnable>();
    public List<Turnable> Actors => _actors;

    /** Subscribe to manager */
    public void Subscribe(Turnable element) {
        if (!_actors.Contains(element))
            _actors.Add(element);

        onModifyList?.Invoke();
    }

    /** UnSubscribe to manager */
    public void Unsubscribe(Turnable element) {
        if (_actors.Contains(element))
            _actors.Remove(element);

        onModifyList?.Invoke();
    }

    /** Current Actor */
    public Turnable Current() {
        return _actors[_current];
    }

    /** Check if contains */
    public bool Contains(Turnable element) {
        return _actors.Contains(element);
    }

    /** Método que ordena la lista tomando _current como inicio */
    public List<Turnable> SortedByIndex() {
        List<Turnable> sorted = new List<Turnable>();
        for (int i = _current; sorted.Count != _actors.Count; i++) {
            i %= _actors.Count;
            sorted.Add(_actors[i]);
        }
        return sorted;
    }

}
