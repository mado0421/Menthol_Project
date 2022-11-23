using UnityEngine;

public class EnemyCharacter : Character
{
    protected GameObject player;

    override protected void Move()
    {
        if(player.transform.position != transform.position)
        {
            transform.LookAt(player.transform.position);
            transform.Translate(transform.forward * Time.deltaTime * spd, Space.World);
        }
    }

    private void Awake()
    {
        hp = 100;
        spd = 3;
        actor = gameObject.AddComponent<NoneActor>();
        player = GameObject.Find("Player");
    }

}
