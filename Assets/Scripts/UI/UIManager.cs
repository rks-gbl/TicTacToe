using System.Collections;
using System.Collections.Generic;
using RitikUtils;
using UnityEngine;

public class UIManager : MPersistentSingleton<UIManager>
{
    public Dictionary<string,UIPanel> allPanels = new();

    new void Awake()
    {
        base.Awake();
        Refresh();
    }
    public void Refresh()
    {
        allPanels.Clear();
        UIPanel[] panels = FindObjectsByType<UIPanel>(FindObjectsInactive.Include , FindObjectsSortMode.None);

        foreach (UIPanel panel in panels)
        {
            if(string.IsNullOrEmpty(panel.id))
                continue;

            allPanels.Add(panel.id , panel);
            Debug.Log($"Panel : {panel.id} Added");
        }
    }

    public void EnablePanel(string id)
    {
        if(allPanels.ContainsKey(id))
        {
            allPanels[id].Enable();
        }
    }

    public void DisablePanel(string id)
    {
        if(allPanels.ContainsKey(id))
            allPanels[id].Disable();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        Refresh();
    }
#endif
}
