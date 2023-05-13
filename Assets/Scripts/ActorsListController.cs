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
    protected List<Actor> _actors = new List<Actor>();
    public List<Actor> Actors => _actors;

    /** Subscribe to manager */
    public void Subscribe(Actor element) {
        if (!_actors.Contains(element))
            _actors.Add(element);

        onModifyList?.Invoke();
    }

    /** UnSubscribe to manager */
    public void Unsubscribe(Actor element) {
        if (_actors.Contains(element))
            _actors.Remove(element);

        onModifyList?.Invoke();
    }

    /** Current Actor */
    public Actor Current() {
        return _actors[_current].actor;
    }

    /** Check if contains */
    public bool Contains(Actor element) {
        return _actors.Contains(element);
    }

    /** Método que ordena la lista tomando _current como inicio */
    public List<Actor> SortedByIndex() {
        List<Actor> sorted = new List<Actor>();
        for (int i = _current; sorted.Count != _actors.Count; i++) {
            i %= _actors.Count;
            sorted.Add(_actors[i]);
        }
        return sorted;
    }

}
