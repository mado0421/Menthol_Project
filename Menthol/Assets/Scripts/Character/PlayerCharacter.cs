using UnityEditor;
using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField]
    private float rotateSpd = 60;

    override protected void Move()
    {
        int dir = 0;
        if (Input.GetKey(KeyCode.A)) dir--;
        if (Input.GetKey(KeyCode.D)) dir++;
        transform.Rotate(0, rotateSpd * dir * Time.deltaTime, 0);
        transform.Translate(transform.forward * spd * Time.deltaTime, Space.World);
    }

    private void Awake()
    {
        hp = 100;
        spd = 2;
        actor = gameObject.AddComponent<PlayerActor>();
    }
}
