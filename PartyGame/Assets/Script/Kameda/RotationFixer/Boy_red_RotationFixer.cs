using UnityEngine;

public class Boy_red_RotationFixer : RotationFixer
{
    protected override void IdleRotate()
    {
        Rotate(0, 0, 0);
    }
    protected override void RunningRotate()
    {
        Rotate(11, 0, 4);
    }
}
