using UnityEngine;

public class Girl_blue_RotationFixer : RotationFixer
{
    protected override void IdleRotate()
    {
        Rotate(-3, 0, 2);
    }
    protected override void RunningRotate()
    {
        Rotate(11, 0, -4);
    }
}