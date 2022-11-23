using UnityEngine;

abstract public class TargetFinder
{
    abstract public GameObject[] GetTargetsInRange(Vector3 currPosition, float range);
}
