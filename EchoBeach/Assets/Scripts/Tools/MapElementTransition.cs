using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapElementTransition : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler,IPointerExitHandler
{
    public Transform Parent;
    public void OnPointerDown(PointerEventData eventData)
    {
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.value(gameObject, 1, 1.1f, .25f).setOnUpdate((value) =>
           {
               Parent.localScale = new Vector2(value, value);
           }).setEaseInCubic();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.value(gameObject, new Vector2(Parent.localScale.x,Parent.localScale.y), Vector2.one, .5f).setOnUpdate((value) =>
        {
            Parent.localScale = new Vector2(value, value);
        }).setEaseInCubic();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
