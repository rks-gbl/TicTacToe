using System.Collections;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [SerializeField] GameObject circlePrefab, crossPrefab;
    [SerializeField] SpriteRenderer image;
    [SerializeField] Transform bg;
    Vector3 bgPosition , bgEulerAngles;

    [SerializeField] Color baseColor , highlightedColor;
    GameObject displayObject;
    [SerializeField]bool occupied = false;
    bool hovered;

    [SerializeField]bool Cross;

    public (int,int) position;
    public Bound bounds;
    public RitikUtils.GameObjectPool crossPool , circlePool;

    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
        ResetBounds();
        bgEulerAngles = bg.localEulerAngles;
    }

    public void ResetBounds()
    {
        bounds = new Bound(transform.position.x - transform.lossyScale.x/2,
                             transform.position.x + transform.lossyScale.x/2,
                             transform.position.y - transform.lossyScale.y/2,
                             transform.position.y + transform.lossyScale.y/2);
    }

    public void Update()
    {
        if(bounds.InsideBound(InteractionHandler.Instance.mouseWorldPos))
        {
            if(!hovered){
            OnPointerEnter();
            hovered = true;
            }
        }
        else
        {
            if(hovered){
            OnPointerExit();
            hovered = false;
            }
        }
    }

    void OnPointerEnter()
    {
        if(occupied || !GameManager.Instance.gameStarted)
        return;
        
        image.color = highlightedColor;
    }

    void OnPointerExit()
    {
        if(occupied || !GameManager.Instance.gameStarted)
        return;

        image.color = baseColor;
    }

    public void SetOccupied()
    {
        if(occupied)
        return;

        Debug.Log("Setting occupied : " + bounds);
        occupied = true;
        if(GameManager.Instance.IsP1Turn())
        {
            displayObject = crossPool.GetObject();
            displayObject.transform.position = transform.position;
            Cross = true;
        }
        else
        {
            displayObject = circlePool.GetObject();
            displayObject.transform.position = transform.position;
            Cross = false;
        }
        if(GameManager.Instance.AI != null)
        {
            GameManager.Instance.AI.RemoveCheckbox(this);
        }

        image.color = baseColor;
        displayObject.SetActive(false);
        AnimateBG();
    }

    Coroutine bgAnimation;
    private void AnimateBG()
    {
        if(bgAnimation != null)
        {
            StopCoroutine(bgAnimation);
            ResetBGTransform();
        }

        SoundManager.Instance.PlayOnce(Consts.Checkbox);
        bgAnimation = StartCoroutine(AnimatorCoroutine(0.2f));
    }

    private void ResetBGTransform()
    {
        bg.localEulerAngles = bgEulerAngles;
    }

    private IEnumerator AnimatorCoroutine(float animationTime)
    {
        float t = 0f;

        while(t < animationTime)
        {
            t += Time.deltaTime;
            if(GameManager.Instance.IsP1Turn())
                bg.localEulerAngles = new Vector3(bg.localEulerAngles.x , 180 * t/animationTime);
            else
                bg.localEulerAngles = new Vector3(180 * t/animationTime,bg.localEulerAngles.y);

            yield return new WaitForEndOfFrame();
        }

        ResetBGTransform();
        //On animation end update turn
        displayObject.SetActive(true);
        GameManager.Instance.UpdateTurn(position,Cross);
    }
    
    public bool GetValue()
    {
        return Cross;
    }

    public bool IsOccupied()
    {
        return occupied;
    }
    public void Reset()
    {
        occupied = false;
        displayObject = null;
        
        if(bgAnimation != null)
            StopCoroutine(bgAnimation);

        crossPool.ResetAll();
        circlePool.ResetAll();
        gameObject.SetActive(false);
    }

    [System.Serializable]
    public class Bound
    {
        public float xMin , xMax;
        public float yMin , yMax;

        public Bound(float _xMin , float _xMax , float _yMin , float _yMax)
        {
            xMin = _xMin;
            xMax = _xMax;
            yMin = _yMin;
            yMax = _yMax;
        }

        public bool InsideBound(Vector2 position)
        {
            if(position.x > xMin && position.x <xMax && position.y > yMin && position.y < yMax)
            return true;
            else
            return false;
        }
    }
}
