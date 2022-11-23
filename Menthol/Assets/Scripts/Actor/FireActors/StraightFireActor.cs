using UnityEngine;

abstract public class StraightFireActor : FireActor
{
    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullets/Bullet");
        targetFinder = new ClosestTargetFinder();
    }

    public override void Act() 
    {
        GameObject[] targets = targetFinder.GetTargetsInRange(transform.position, range);

        if (targets != null)
        {
            RotateBarrelTo(targets[0].transform.position);
            if (Shootable)
            {
                FireToward(targets[0].transform.position);
                StartCoroutine(Delay(delayTime));
            }
        }
    }
}
