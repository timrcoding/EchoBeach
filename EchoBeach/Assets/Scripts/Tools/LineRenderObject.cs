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
        // StartCoroutine(CreateDots());
        RelationshipBetweenTransforms();
    }

    void RelationshipBetweenTransforms()
    {
        GameObject StartObject = MapManager.instance.MapElementDictionary[StartPosition];
        GameObject EndObject = MapManager.instance.MapElementDictionary[EndPosition];

        Vector3 StartPos = StartObject.transform.position;
        Vector3 EndPos = EndObject.transform.position;

        RectTransform Init = StartObject.GetComponent<RectTransform>();
        Rect Rect = RectTransformToScreenSpace(Init);
        float XBounds = 1; //Rect.size.x;
        float YBounds = 1; // Rect.size.y;

        if (StartPos.y > EndPos.y)
        { 
            LineStartOffset.y = StartPos.y - YBounds;
            LineEndOffset.y = EndPos.y + YBounds;
        }
        if(StartPos.y < EndPos.y)
        {
            LineStartOffset.y = StartPos.y + YBounds;
            LineEndOffset.y = EndPos.y - YBounds;
        }

        if(StartPos.x > EndPos.x)
        {
            LineStartOffset.x = StartPos.x - XBounds;
            LineEndOffset.x = EndPos.x + XBounds;
        }
        if(StartPos.x < EndPos.x)
        {
            LineStartOffset.x = StartPos.x + XBounds;
            LineEndOffset.x = EndPos.x - XBounds;
        }

    }
    void Update()
    {
        LineRenderer Line = GetComponent<LineRenderer>();
        Line.SetPosition(0, MapManager.instance.MapElementDictionary[StartPosition].transform.position);
        Line.SetPosition(1, MapManager.instance.MapElementDictionary[EndPosition].transform.position);
    }

    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }

}
