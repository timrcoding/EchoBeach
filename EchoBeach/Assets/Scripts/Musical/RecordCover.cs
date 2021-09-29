using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordCover : MonoBehaviour
{
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private Image Image;
    

    private void Start()
    {
        
    }
    public void MoveCover(Vector3 OutPosition)
    {
        LeanTween.value(gameObject, 0, 1, RecordManager.instance.AnimSpeed).setOnUpdate((value) =>
        {
            float curve = RecordManager.instance.AnimCurve.Evaluate(value);
            transform.position = Vector2.Lerp(transform.position, OutPosition, curve);
            
        });

        LeanTween.value(gameObject, 0, 1, 2f).setOnUpdate((value) =>
        {
            Image.color = Color.Lerp(Color.black, Color.white, value);
        });
        
    }

    public IEnumerator PrintOut(Vector3 OutPosition)
    {
        for(float i = 0; i <= 1; i += 0.05f)
        {
            transform.position = Vector2.Lerp(transform.position, OutPosition, i);
            yield return new WaitForSeconds(.05f);
        }
    }

    public void SetImage(Sprite S)
    {
        Image.sprite = S;
    }

    public void PutAwayAndDestroy(Vector3 AwayPosition)
    {
        LeanTween.scale(gameObject, Vector3.zero, .5f).setOnComplete(DestroyObject);
        LeanTween.rotateZ(gameObject, 720, .5f).setOnComplete(DestroyObject);
        LeanTween.value(gameObject, 0, 1, 1f).setOnUpdate((value) =>
        {
            CanvasGroup.alpha = Mathf.Lerp(1, 0, value);
        });
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
