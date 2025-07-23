using UnityEngine;
using UnityEngine.Rendering;

public enum PlayerAniamtonState {

    Idle,
    Walk,
    Happy,
    Sad
}

public class AnimationContllore
{
    private     Animator                animator    = default;
    private     PlayerAniamtonState     state       = default;

    private string[] AnimationName = new string[4] {

        "Idle",
        "Walk",
        "Happy",
        "Sad"
    };

    public AnimationContllore(Animator anim)
    {
        animator = anim;
    }

    public void SetAniamation(PlayerAniamtonState _state) {

        if (state == _state) return;

        animator.Play(AnimationName[(int)_state], 0, 1);

        state = _state;
    }
}
