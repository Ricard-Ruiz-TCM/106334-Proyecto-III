using System;
using System.Collections.Generic;
using UnityEngine;

public class SOBox<T> where T : Enum {

    [SerializeField, Header("Elements:")]
    public List<T> _elements;
    public List<T> Elements => _elements;

    public void Add(T item) {
        if (!Contains(item))
            _elements.Add(item);
    }

    public void Remove(T item) {
        if (!Contains(item))
            _elements.Remove(item);
    }

    public bool Contains(T item) {
        return _elements.Contains(item);
    }

}