using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class InteractionHandler : MonoBehaviour
{private static InteractionHandler instance;
    public static InteractionHandler Instance
    {
        get
        {
            if(instance == null)
                instance = FindAnyObjectByType<InteractionHandler>(FindObjectsInactive.Exclude);

            return instance;
        }
    }
    

    void OnClick()
    {
        foreach(var c in GameManager.Instance.checkBoxes.Values)
        {
            if(c.bounds.InsideBound(mouseWorldPos))
            {
                c.SetOccupied();
            }
        }
    }

    public Vector2 mouseWorldPos;
    public void Update()
    {
        if(!GameManager.Instance || !GameManager.Instance.gameStarted)
            return;
        
        if(!GameManager.Instance.IsP1Turn() && GameManager.Instance.gameMode != GameMode.OfflineTwoPlayer)
            return;
            
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }
}