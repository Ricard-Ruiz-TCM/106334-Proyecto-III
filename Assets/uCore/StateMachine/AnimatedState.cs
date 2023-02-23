using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AnimatedState : BasicState {

    // Animator
    private Animator animator;
    public Animator Animator {
        get {
            if (animator == null) {
                animator = GetComponent<Animator>();
            }
            return animator;
        }
    }

}
