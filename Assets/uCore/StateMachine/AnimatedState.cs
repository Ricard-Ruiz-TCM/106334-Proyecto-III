using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AnimatedState : BasicState {

    // Animator
    private Animator _animator;
    public Animator Animator {
        get {
            if (_animator == null) {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }

}
