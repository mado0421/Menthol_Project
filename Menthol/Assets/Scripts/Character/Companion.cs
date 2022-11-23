using UnityEngine;

abstract public class Companion : Character
{
    private TrailManager playerTrail;
    private static int trailCount = 0;
    [SerializeField]
    private int trailOrder;

    private void Awake()
    {
    }

    private void Start()
    {
        trailOrder = trailCount++;
        playerTrail = GameObject.Find("Player").GetComponent<TrailManager>();
    }

    override protected void Move()
    {
        // Follow Player Character's trail.
        transform.position = playerTrail.GetPosition(trailOrder);
    }
}
