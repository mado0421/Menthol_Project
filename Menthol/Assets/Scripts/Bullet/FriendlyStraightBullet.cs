using UnityEngine;

public class FriendlyStraightBullet : Bullet
{
    protected override void Move()
    {
        transform.Translate(transform.forward * Time.fixedDeltaTime * spd, Space.World);
    }

    private void Awake()
    {
        gameObject.layer = 8;   // PlayerBullet.
        if(dmg == 0) dmg = 10;
        if (spd == 0) spd = 10;
    }
}
