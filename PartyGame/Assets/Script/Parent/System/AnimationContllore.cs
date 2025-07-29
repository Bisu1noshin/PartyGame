using System.Buffers;
using UnityEngine;

public enum PlayerAniamtonState {

    Idle,
    Walk,
    Happy,
    Sad
}

public class AnimationContllore
{
    private     Animator                animator    = default;
    private     GameObject              parent      = default;
    private     GameObject              child       = default;
    private     PlayerAniamtonState     state       = default;

    private string[] AnimationName = new string[4] {

        "Idle",
        "Run",
        "Happy",
        "Sad"
    };

    public AnimationContllore(GameObject obj)
    {
        parent = obj;
        child = parent.transform.GetChild(0).gameObject;
        animator = child.GetComponent<Animator>();
    }

    public void SetAniamation(PlayerAniamtonState _state) {

        if (state == _state) return;

        animator.Play(AnimationName[(int)_state], 0, 1);

        state = _state;
    }

    public void RotaitionContllore(Vector2 vec) {

        Vector2 v = vec.normalized;
        float angle = Vector2.Angle(new Vector2(0, 1), vec);

        if (vec.x < 0) { angle *= -1; }

        Quaternion preQuaternion = parent.transform.rotation;
        Quaternion newQuaternion = Quaternion.Euler(preQuaternion.x, angle, preQuaternion.z);

        parent.transform.rotation = newQuaternion;
    }
}
