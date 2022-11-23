using UnityEngine;

public class ClosestTargetFinder : TargetFinder
{
    override public GameObject[] GetTargetsInRange(Vector3 currPosition, float range)
    {
        GameObject[] targets = null;

        // Get Enemy List.
        GameObject enemyPool = GameObject.Find("EnemyPool");
        GameObject[] enemyList = new GameObject[enemyPool.transform.childCount];
        for(int i = 0; i < enemyPool.transform.childCount; i++)
            enemyList[i] = enemyPool.transform.GetChild(i).gameObject;

        // Find Closest Enemy from Enemy List.
        if (enemyList.Length != 0)
        {
            GameObject target = null;
            float distance = range * range; // sqrMagnitude를 사용하기 위함

            foreach (GameObject enemy in enemyList)
            {
                Vector3 offset = enemy.transform.position - currPosition;
                if(distance> offset.sqrMagnitude )
                {
                    distance = offset.sqrMagnitude;
                    target = enemy;
                }
            }

            if(target != null ) { targets = new GameObject[1]; targets[0] = target; }
        }

        return targets;
    }
}
