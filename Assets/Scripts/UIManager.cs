using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject interactionPanel;
    public GameObject testPanel;

    private Dictionary<string, GameObject> panelDictionary = new Dictionary<string,GameObject>();

    private void Awake()
    {
        panelDictionary.Add("interactionPanel", interactionPanel);
    }


    public void Activate(string panelName)
    {
        GameObject panelGO;
        if(panelDictionary.TryGetValue(panelName, out panelGO))
        {
            panelGO.SetActive(true);
        }
    }

    public void Deactivate(string panelName)
    {
        GameObject panelGO;
        if (panelDictionary.TryGetValue(panelName, out panelGO))
        {
            panelGO.SetActive(false);
        }
    }
}
