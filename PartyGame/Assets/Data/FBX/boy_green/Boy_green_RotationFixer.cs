using UnityEngine;

public class Boy_green_RotationFixer : RotationFixer
{
    protected override void IdleRotate()
    {
        transform.rotation = Quaternion.Euler(-3, 0, 3);
    }
    protected override void RunningRotate()
    {
        transform.rotation = Quaternion.Euler(11, 0, 1);
    }
}
