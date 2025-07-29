using UniGLTF;
using UnityEngine;

public class FBXAnimator : MonoBehaviour
{
    Animator anim;
    RotationFixer rf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gameObject.GetOrAddComponent<Animator>();
        rf = GetComponent<RotationFixer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayAnimation("Run");
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayAnimation("Idle");
        }
    }

    public void PlayAnimation(string name)
    {
        if(name == "Idle")
        {
            rf.SetState(RotationFixer.AnimState.Idle);
        }
        if(name == "Run")
        {
            rf.SetState(RotationFixer.AnimState.Run);
        }
        anim.Play(name);
    }
}
