using UnityEngine;

public class Boy_red_RotationFixer : RotationFixer
{
    protected override void IdleRotate()
    {
        transform.rotation = Quaternion.identity;
    }
    protected override void RunningRotate()
    {
        transform.rotation = Quaternion.Euler(11, 0, 4);
    }
}
