using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public DeepNetLinkName EndPosition;
    void Update()
    {
        Vector3 End = MapManager.instance.MapElementDictionary[EndPosition].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, End, 1f);
        if(Vector3.Distance(transform.position,End) < Time.deltaTime)
        {
            Destroy(gameObject);
        }

    }
}
