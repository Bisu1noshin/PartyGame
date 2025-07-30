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
    private     int                     index       = default;

    private string[] AnimationName = new string[4] {

        "Idle",
        "Run",
        "Happy",
        "Sad"
    };

    private Vector3[,] AniamationAngle = new Vector3[4,4] {//y,x

        // yがindex xがPlayerAniamtonState
        { new Vector3(0, 0, 3),new Vector3(11, 0, -4),new Vector3(),new Vector3()},
        { new Vector3(0, 0, 0),new Vector3(11, 0, 4),new Vector3(),new Vector3()},
        { new Vector3(-2, 0, 4),new Vector3(11, 0, -2),new Vector3(),new Vector3()},
        { new Vector3(-3, 0, 3),new Vector3(11, 0, 1),new Vector3(),new Vector3()}
    };

    public AnimationContllore(GameObject obj,int _index = 0)
    {
        parent = obj;
        index = _index;

        if (parent.transform.GetChild(0).gameObject != null)
        {

            child = parent.transform.GetChild(0).gameObject;
            animator = child.GetComponent<Animator>();
        }
        else {

            animator = null;
        }
    }

    public void SetAniamation(PlayerAniamtonState _state) {

        if (animator == null) { return; }

        if (state == _state) return;

        animator.Play(AnimationName[(int)_state], 0, 1);

        state = _state;
    }

    public void RotaitionContllore(Vector2 vec) {

        if (animator == null) { return; }

        child.transform.localEulerAngles = AniamationAngle[index, (int)state];
        child.transform.localPosition = Vector3.zero;

        if (vec.x == 0 && vec.y == 0) {

            return;
        }

        Vector2 v = vec.normalized;
        float angle = Vector2.Angle(new Vector2(0, 1), vec);

        if (vec.x < 0) { angle *= -1; }

        Quaternion newQuaternion = Quaternion.Euler(0, angle, 0);

        parent.transform.rotation = newQuaternion;
    }
}
