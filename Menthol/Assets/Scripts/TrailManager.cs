using System.Collections;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    private GameObject[] trailPoints;
    [SerializeField]
    private int maxPoint = 6;
    [SerializeField]
    private float followUpInterval = 2.0f;
    private float currTime;

    private void Awake()
    {
        GameObject trailListObject = new GameObject("TrailList");
        trailPoints = new GameObject[maxPoint];
        for(int i = 0; i < trailPoints.Length; i++)
        {
            trailPoints[i] = new GameObject("TrailPoint " + i.ToString());
            trailPoints[i].transform.parent = trailListObject.transform;
        }
    }

    private void Start()
    {
        StartCoroutine(RefreshTrail(followUpInterval));
    }

    private IEnumerator RefreshTrail(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            for (int i = trailPoints.Length - 1; i > 0; i--)
            {
                trailPoints[i].transform.position = trailPoints[i - 1].transform.position;
            }
            trailPoints[0].transform.position = transform.position;
            currTime = 0;
        }
    }

    private void Update()
    {
        currTime += Time.deltaTime;
    }

    public Vector3 GetPosition(int index)
    {
        if(0 <= index && index < trailPoints.Length)
        {
            int i0 = index - 1, i1 = index, i2 = index + 1, i3 = index + 2;
            if (i0 < 0) i0 = 0;
            if (i2 > trailPoints.Length - 1) i2 = trailPoints.Length - 1;
            if (i3 > trailPoints.Length - 1) i3 = trailPoints.Length - 1;

            return MathHelper.GetCatmullRomPosition(
                1 - (currTime / followUpInterval),
                trailPoints[i0].transform.position,
                trailPoints[i1].transform.position,
                trailPoints[i2].transform.position,
                trailPoints[i3].transform.position);
        }
        return trailPoints[trailPoints.Length - 1].transform.position;    // exception.
    }
}
