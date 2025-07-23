using UnityEngine;

public class Girl_yellow_RotationFixer : RotationFixer
{
    protected override void IdleRotate()
    {
        transform.rotation = Quaternion.Euler(-2, 0, 4);
    }
    protected override void RunningRotate()
    {
        transform.rotation = Quaternion.Euler(11, 0, -2);
    }
}