using UnityEngine;

public class PlayerActor : StraightFireActor
{
    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullets/Bullet");
        targetFinder = new ClosestTargetFinder();
        delayTime = 1.0f / (450.0f / 60.0f);
    }
}
