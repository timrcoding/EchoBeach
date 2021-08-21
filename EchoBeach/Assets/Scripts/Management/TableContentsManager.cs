using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableContentsManager : MonoBehaviour
{
    public static TableContentsManager instance;
    public Transform TableCardsParent;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
