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
        TMP.text = CharacterID.MRNameStr;
        TMPIdent.text = IDType.ToString();
    }

    public void CreateLargeId()
    {
        SearchableDatabaseManager.instance.DestroyLargeIDCollection();
        GameObject newLargeID = Instantiate(SearchableDatabaseManager.instance.LargeIDPrefab);
        newLargeID.transform.SetParent(SearchableDatabaseManager.instance.LargeIDParent.transform);
        newLargeID.transform.localPosition = SearchableDatabaseManager.instance.LargeIDParent.transform.position;
        newLargeID.transform.localScale = Vector3.one;
        newLargeID.GetComponent<LargeID>().SetCharacterID(CharacterID);
    }
}
