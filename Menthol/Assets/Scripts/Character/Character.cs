using UnityEngine;

abstract public class Character : MonoBehaviour
{
    protected Actor actor;
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected float spd;

    abstract protected void Move();

    public void Damage(float damage)
    {
        
        hp -= damage;
    }

    private bool isDead
    {
        get { return hp <= 0; }
    }

    virtual protected void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Move();
        actor.Act();

        if (isDead) Die();
    }
}
