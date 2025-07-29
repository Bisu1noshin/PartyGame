using UnityEngine;

public class Girl_blue_RotationFixer : RotationFixer
{
    protected override void IdleRotate()
    {
        transform.rotation = Quaternion.Euler(-3, 0, 2);
    }
    protected override void RunningRotate()
    {
        transform.rotation = Quaternion.Euler(11, 0, -4);
    }
}