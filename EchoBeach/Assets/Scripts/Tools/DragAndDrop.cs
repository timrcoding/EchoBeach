using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
   [SerializeField] protected bool CanMove;
   [SerializeField] protected float DragCooldown;
    [SerializeField] protected float DragCoolDownAmount;
    protected Vector3 dragOffset;

    public bool ReturnCanMove()
    {
        return CanMove;
    }

    public virtual void SetMove()
    {
        DragCooldown += 0.1f;
    }
    public virtual void StartDrag()
    {
        CanMove = true;
        DragCooldown = -DragCoolDownAmount; 
        dragOffset = transform.position - returnCameraPoint(Input.mousePosition);
    }

    public virtual void StopDrag()
    {
        CanMove = false;
    }

    protected Vector3 returnCameraPoint(Vector3 InputVector)
        {
            var screenPoint = InputVector;
            screenPoint.z = 10.0f;
            return Camera.main.ScreenToWorldPoint(screenPoint);
        }
    }


