using System.Collections;
using System.Collections.Generic;
using RitikUtils;
using UnityEngine;

public class CameraSizeUpdater : MSingleton<CameraSizeUpdater>
{
    [Tooltip("Canvas according to which resolution changes to be detected")]
    [SerializeField] Canvas refCanvas;
    [SerializeField] Camera mainCamera;
    [SerializeField]float initSize;

    [Header("Warning DO NOT change manually")]
    [SerializeField] Vector2 initialRes;
    [SerializeField] Vector2 currentRes;
    [SerializeField]float initRatio;

    public float offset;

    void Awake()
    {
        CheckAndUpdateOrthographicSize();
        Application.focusChanged += OnFocusChanged;
    }

    #if UNITY_EDITOR
    void OnValidate()
    {
        Init();
        //CheckAndUpdateOrthographicSize();
    }
    #endif

    void OnFocusChanged(bool resume)
    {
        if(resume)
            CheckAndUpdateOrthographicSize();
    }

    public void Init()
    {
        if(initRatio == 0 || initialRes == Vector2.zero && refCanvas != null){
            Reset();
        }
    }

    public void Reset()
    {
        initSize = mainCamera.orthographicSize;
        initialRes = new Vector2(refCanvas.renderingDisplaySize.x , refCanvas.renderingDisplaySize.y);
        initRatio = initialRes.y / initialRes.x;
    }

    public void CheckAndUpdateOrthographicSize()
    {
        if(refCanvas != null){ 
            currentRes = new Vector2(refCanvas.renderingDisplaySize.x , refCanvas.renderingDisplaySize.y);
            if(initialRes != currentRes)
            {
                float currentRatio = currentRes.y / currentRes.x;

                if(currentRatio > initRatio)
                {
                    float sizeIncrease = (currentRatio-initRatio)/initRatio * initSize;
                    Debug.Log(currentRatio+"-"+initRatio);
                    Debug.Log("Diff / "+initRatio);
                    
                    Debug.Log(initSize+"+"+sizeIncrease);
                    mainCamera.orthographicSize = initSize + sizeIncrease + offset;
                }
            }
            else
                    mainCamera.orthographicSize = initSize + offset;
        }
    }
}
