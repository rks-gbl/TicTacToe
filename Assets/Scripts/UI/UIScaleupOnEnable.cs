using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleupOnEnable : MonoBehaviour
{
    [SerializeField] float delay, duration;
    Coroutine coroutine;
    Vector3 initialScale;

    void Awake()
    {
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void OnEnable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(ScaleUpCoroutine());
    }

    IEnumerator ScaleUpCoroutine()
    {
        yield return new WaitForSeconds(delay);
        float t = 0f;
        while(t < duration)
        {
            t += Time.deltaTime;
            transform.localScale = new Vector3(initialScale.x * t/duration , initialScale.y * t/duration ,initialScale.z * t/duration);
            yield return new WaitForEndOfFrame();
        }

        transform.localScale = initialScale;
    }
}
