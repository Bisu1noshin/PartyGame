using UnityEngine;

public class Girl_yellow_RotationFixer : RotationFixer
{
    protected override void IdleRotate()
    {
        Rotate(-2, 0, 4);
    }
    protected override void RunningRotate()
    {
        Rotate(11, 0, -2);
    }
}