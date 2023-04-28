using System;
using UnityEngine;

[Serializable]
public class Expertise {
    public items item;

    [Range(0, 2)]
    public int level;

}