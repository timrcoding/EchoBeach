using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseTemplate : MonoBehaviour
{
    protected ScriptableObject MScriptableObject;

    //TMP ELEMENTS
    [SerializeField] protected TextMeshPro TMPName;

    public void SetScriptableObject(ScriptableObject scriptableObject)
    {
        MScriptableObject = scriptableObject;
    }
}
