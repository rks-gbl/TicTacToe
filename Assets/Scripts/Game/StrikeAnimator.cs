using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeAnimator : MonoBehaviour
{
    public string id;
    public float animateTime;

    Vector3 initScale;
    Vector3 initPos;

    void Awake()
    {
        Init();
    }

    public void ResetInitPos()
    {
        initPos = transform.localPosition;
    }
    
    void Init()
    {
        ResetInitPos();
        initScale = transform.localScale;
    }

    #if UNITY_EDITOR
    void OnValidate()
    {
        if(string.IsNullOrEmpty(id))
            id = gameObject.name;

        Init();
    }
    #endif

    void OnEnable()
    {
        ResetValues();
        StartCoroutine(StartAnimation());
    }

    void ResetValues()
    {
        transform.localPosition = initPos;
        transform.localScale = initScale;
    }

    IEnumerator StartAnimation()
    {
        float d = 0;
        while(d < animateTime)
        {
            d += Time.deltaTime;
            transform.localScale = new Vector3(initScale.x , initScale.y * d/animateTime , initScale.z);
            yield return new WaitForEndOfFrame();
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
