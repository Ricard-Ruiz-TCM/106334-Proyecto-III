using UnityEngine;

/** abstract class AnimatedState
 * -----------------------------
 * 
 * Control de estados igual que los BasicState pero añadiendo un acceso al Animator
 * 
 * @see BasicState
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v1.0 (04/2023)
 * 
 */

[RequireComponent(typeof(Animator))]
public abstract class AnimatedState : BasicState {

    /** Animator */
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
