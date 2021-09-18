using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderObject : MonoBehaviour
{
    public DeepNetLinkName StartPosition;
    public DeepNetLinkName EndPosition;
    public GameObject DotPrefab;

    public Vector2 LineStartOffset;
    public Vector2 LineEndOffset;
    // Update is called once per frame

    private void Start()
    {
        LineRenderer Line = GetComponent<LineRenderer>();
        Line.numCornerVertices = 5;
        Line.SetPosition(0, MapManager.instance.MapElementDictionary[StartPosition].transform.position);
        Line.SetPosition(1, MapManager.instance.MapElementDictionary[EndPosition].transform.position);
    }
    void Update()
    {
        
    }

    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }

}
