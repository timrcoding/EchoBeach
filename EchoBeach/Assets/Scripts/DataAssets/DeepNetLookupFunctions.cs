using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepNetLookupFunctions : MonoBehaviour
{
    public static DeepNetLookupFunctions instance;

    public SODeepNetLookup MSODeepNetLookup;

    private void Awake()
    {
        instance = this;
    }

    public static SODeepNetPage ReturnDeepNetLinkToPage(DeepNetLink Link, SODeepNetLookup Data)
    {
        return Data.LinkToScriptableDictionary[Link];
    }

    public static string ReturnFontToString(Font Font, SODeepNetLookup Data)
    {
        return Data.FontToStringsDictionary[Font];
    }
}
