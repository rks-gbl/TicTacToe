using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public string id;
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    #if UNITY_EDITOR
    public void OnValidate()
    {
        if(string.IsNullOrEmpty(id))
            id = gameObject.name;
    }
    #endif
}
