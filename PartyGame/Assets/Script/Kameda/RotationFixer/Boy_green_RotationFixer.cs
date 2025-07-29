using UnityEngine;

public class Boy_green_RotationFixer : RotationFixer
{
    protected override void IdleRotate()
    {
        Rotate(-3, 0, 3);
    }
    protected override void RunningRotate()
    {
        Rotate(11, 0, 1);
    }
}
