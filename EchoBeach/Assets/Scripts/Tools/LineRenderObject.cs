using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderObject : MonoBehaviour
{
    public DeepNetLinkName StartPosition;
    public DeepNetLinkName EndPosition;

    public Vector2 LineStartOffset;
    public Vector2 LineEndOffset;
    LineRenderer Line;
    // Update is called once per frame

    private void Start()
    {
        Line = GetComponent<LineRenderer>();
        Line.numCornerVertices = 5;
    }

    private void Update()
    {
        Line.SetPosition(0, MapManager.instance.MapElementDictionary[StartPosition].transform.position);
        Line.SetPosition(1, MapManager.instance.MapElementDictionary[EndPosition].transform.position);
    }

}
