using UnityEngine;

abstract public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float dmg;
    [SerializeField]
    protected float spd;

    abstract protected void Move();

    private void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Character>().Damage(dmg);
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}
