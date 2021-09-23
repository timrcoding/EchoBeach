using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField InputField;

    public void SelectInput()
    {
        InputField.Select();
    }

}
