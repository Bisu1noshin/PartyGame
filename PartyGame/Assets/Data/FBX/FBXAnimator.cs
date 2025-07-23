using UniGLTF;
using UnityEngine;

public class FBXAnimator : MonoBehaviour
{
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gameObject.GetOrAddComponent<Animator>();
    }

    public void PlayAnimation(string name)
    {
        anim.Play(name);
    }
}
