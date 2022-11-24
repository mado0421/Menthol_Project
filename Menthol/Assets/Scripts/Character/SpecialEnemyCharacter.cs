using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemyCharacter : EnemyCharacter
{
    private GameObject item;

    private void Awake()
    {
        actor = gameObject.AddComponent<NoneActor>();
        player = GameObject.Find("Player");
        item = Resources.Load<GameObject>("Prefabs/Items/CompanionCapsule");
    }

    override protected void Die()
    {
        GameObject capsule = Instantiate(item);
        capsule.transform.position = transform.position;
        GameManager.Instance.scoreMng.AddScore(10);
        base.Die();
    }
}
