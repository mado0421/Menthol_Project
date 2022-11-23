using System.Collections;
using UnityEngine;

abstract public class FireActor : Actor
{
    protected TargetFinder targetFinder;
    protected GameObject bulletPrefab;
    protected GameObject barrel;
    protected float delayTime = 1 / (600 / 60);
    protected float range = 10.0f;
    private bool shootable = true;
    private GameObject bulletPool;

    private void Start()
    {
        bulletPool = GameObject.Find("BulletPool");
    }

    protected void FireToward(Vector3 position) {
        GameObject bullet = Instantiate(bulletPrefab, bulletPool.transform);
        bullet.transform.position = transform.position;
        bullet.transform.LookAt(position);

        shootable = false;
    }
    protected void RotateBarrelTo(Vector3 position)
    {
        if (barrel == null) return;
        if(barrel.transform.position != position)
            barrel.transform.LookAt(position);
    }
    protected IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shootable = true;
    }
    protected bool Shootable
    {
        get { return shootable; }
    }
}
