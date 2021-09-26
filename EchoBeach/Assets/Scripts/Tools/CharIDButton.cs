using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharIDButton : MonoBehaviour
{
    public CharacterID CharacterID;
    [SerializeField] private TextMeshProUGUI TMP;
    [SerializeField] private TextMeshProUGUI TMPIdent;
    public void SetCharacterName(CharacterID CharID,IdentType IDType)
    {
        CharacterID = CharID;
        TMP.text = CharacterID.RealNameString;
        TMPIdent.text = IDType.ToString();
    }

    public void CreateLargeId()
    {
        SearchableDatabaseManager.instance.DestroyLargeIDCollection();
        GameObject newLargeID = Instantiate(SearchableDatabaseManager.instance.GetIDPrefab);
        newLargeID.transform.SetParent(SearchableDatabaseManager.instance.GetIDParent.transform);
        newLargeID.transform.localPosition = SearchableDatabaseManager.instance.GetIDParent.transform.position;
        newLargeID.transform.localScale = Vector3.one;
        newLargeID.GetComponent<LargeID>().SetCharacterID(CharacterID);
        FMODUnity.RuntimeManager.PlayOneShot(SearchableDatabaseManager.instance.PrintSound);
    }
}
