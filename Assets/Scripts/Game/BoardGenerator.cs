using System.Collections;
using System.Collections.Generic;
using RitikUtils;
using Unity.VisualScripting;
using UnityEngine;

public class BoardGenerator : MSingleton<BoardGenerator>
{
    public Transform container;
    public Transform repositioningParent;
    public GameObject checkBoxPrefab;
    public CheckboxPool checkboxPool;
    public int size;
    public float generationDelayPerBox = 0.01f;
    public void Generate(int _size)
    {
        size = _size;

        checkboxPool.ResetAll();
        checkboxPool.ParentInScene = container;

        if(GameManager.Instance != null){
            GameManager.Instance.checkBoxes = new Dictionary<(int,int), CheckBox>();
        }

        RepositionParent();
        UpdateCameraOrthographicSize();

        StartCoroutine(BoardGenerateCoroutine());
    }

    IEnumerator BoardGenerateCoroutine()
    {
        for(int i=0;i<size; i++)
        {
            for(int j=0;j<size;j++)
            {
                CheckBox box = checkboxPool.GetObject();
                box.name = "checkbox"+i+""+j;
                box.position = (i,j);
                box.transform.localPosition = new Vector3(i,j);
                box.transform.localScale = new Vector3(1,1,1);
                box.ResetBounds();
                if(GameManager.Instance != null){
                    GameManager.Instance.checkBoxes.Add((i,j),box);
                }

                yield return new WaitForSeconds(generationDelayPerBox);
            }
        }
        if(GameManager.Instance.AI != null)
            GameManager.Instance.AI.Reset();
        //On completion
        GameManager.Instance.gameStarted = true;
    }

    void RepositionParent()
    {
        float pos = (float)(size-1)/2;
        repositioningParent.transform.localPosition = new Vector3(-pos , -pos);
    }

    void UpdateCameraOrthographicSize()
    {
        float offset = (size-3)*2;
        CameraSizeUpdater.Instance.offset = offset;
        CameraSizeUpdater.Instance.CheckAndUpdateOrthographicSize();
    }
}
