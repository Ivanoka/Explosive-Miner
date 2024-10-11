using System.Collections;
using UnityEngine;

public class ExplodingHole : MonoBehaviour
{
    [Header("This Object")]
    [SerializeField] private float objectResizeSmoothTime = 0.2f;
    public State currentState;
    [SerializeField] private GameObject holeLv1Object;
    [SerializeField] private Vector3 cursorColliderLv2Position;

    [Header("Dynamite")]
    [SerializeField] private GameObject dynamitePrefab;
    [SerializeField] private float dynamiteSpawnHeight = 2.0f;
    private GameObject dynamiteObject;
    private Dynamite dynamiteClass;

    [Header("Diamond")]
    [SerializeField] private GameObject diamondPrefab;
    private GameObject diamondObject;
    private Diamond diamondClass;

    [Header("Sign")]
    [SerializeField] private GameObject signObject;
    [SerializeField] private float signIncreaseSize = 1.3f;
    [SerializeField] private float signResizeSmoothTime = 0.1f;
    [SerializeField] private Vector3 signLv2Position;

    [Header("Frame")]
    [SerializeField] private GameObject frameObject;
    [SerializeField] private Vector3 frameLv2Position;

    [Header("Restart")]
    [SerializeField] private GameObject suggestionToRestart;

    private Vector3 scaleVelocity = Vector3.zero;
    private BoxCollider boxCollider;

    public enum State
    {
        Level1,
        Level2,
        End
    }

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        suggestionToRestart.SetActive(false);
    }

    public void Init()
    {
        switch (currentState)
        {
            case State.Level1:
                GoToLevelOne();
                break;
            case State.Level2:
                signObject.transform.localScale = Vector3.one;
                GoToLevelTwo();
                StartCoroutine(ScaleObject(gameObject, Vector3.one, objectResizeSmoothTime));
                break;
            case State.End:
                gameObject.transform.localScale = Vector3.zero;
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.canGameInteract && GameManager.instance.dynamiteCount == 0)
        {
            suggestionToRestart.SetActive(true);
        }

        if (GameManager.instance.canGameInteract && GameManager.instance.dynamiteCount > 0)
        {
            GameManager.instance.ReduceDynamite();
            StartCoroutine(Explosion());
        }
        
    }

    public void HideGameObject()
    {
        StopAllCoroutines();

        if (diamondClass != null)
        {
            diamondClass.OnDestroyed -= ReturnSign;
        }
        if (diamondObject != null)
        {
            Destroy(diamondObject);
        }

        if (dynamiteObject != null)
        {
            Destroy(dynamiteObject);
        }

        StartCoroutine(ScaleObject(gameObject, Vector3.zero , objectResizeSmoothTime));
    }

    public void GoToLevelOne()
    {
        StopAllCoroutines();       

        if (diamondClass != null)
        {
            diamondClass.OnDestroyed -= ReturnSign;
        }
        if (diamondObject != null)
        {
            
            Destroy(diamondObject);
        }

        if (dynamiteObject != null)
        {
            Destroy(dynamiteObject);
        }

        gameObject.transform.localScale = Vector3.zero;

        signObject.SetActive(true);
        signObject.transform.localPosition = Vector3.zero;
        signObject.transform.localScale = Vector3.one;

        frameObject.SetActive(true);
        frameObject.transform.localPosition = Vector3.zero;

        holeLv1Object.SetActive(true);

        boxCollider.center = Vector3.zero;
        boxCollider.size = Vector3.one;

        currentState = State.Level1;
        
        StartCoroutine(ScaleObject(gameObject, Vector3.one, objectResizeSmoothTime));
    }

    private void GoToLevelTwo()
    {
        signObject.SetActive(true);
        signObject.transform.localPosition = signLv2Position;

        frameObject.transform.localPosition = frameLv2Position;

        holeLv1Object.SetActive(false);
        
        boxCollider.center = cursorColliderLv2Position;
        boxCollider.size = Vector3.one;

        currentState = State.Level2;
    }

    private void ReturnSign()
    {
        signObject?.SetActive(true);
        frameObject?.SetActive(true);

        boxCollider.size = Vector3.one;

        StartCoroutine(ScaleObject(signObject, Vector3.one, signResizeSmoothTime));

        if (diamondClass != null)
        {
            diamondClass.OnDestroyed -= ReturnSign;
        }
    }

    private IEnumerator Explosion()
    {
        boxCollider.size = Vector3.zero;

        yield return StartCoroutine(ScaleObject(signObject, new Vector3(signIncreaseSize, signIncreaseSize, signIncreaseSize), signResizeSmoothTime));
        yield return StartCoroutine(ScaleObject(signObject, Vector3.zero, signResizeSmoothTime));

        signObject.SetActive(false);
        frameObject.SetActive(false);

        dynamiteObject = Instantiate(dynamitePrefab, signObject.transform.position + new Vector3(0.0f, dynamiteSpawnHeight, 0.0f), Quaternion.identity);
        dynamiteClass = dynamiteObject.GetComponent<Dynamite>();

        yield return StartCoroutine(dynamiteClass.BlinkAndDestroy());

        if (Random.Range(0.0f, 100.0f) <= GameManager.instance.diamondSpawnRate)
        {
            diamondObject = Instantiate(diamondPrefab, signObject.transform.position, Quaternion.identity);

            diamondClass = diamondObject.GetComponent<Diamond>();
            diamondClass.uiElement = GameManager.instance.diamondUIIcon;
            
            if (currentState == State.Level1) diamondClass.OnDestroyed += ReturnSign;
        }
        else
        {
            ReturnSign();
        }

        if (currentState == State.Level1)
        { 
            GoToLevelTwo();
        }
        else if (currentState == State.Level2)
        {
            currentState = State.End;
            
            gameObject.transform.localScale = Vector3.zero;
        }
    }

    private IEnumerator ScaleObject(GameObject targetObject, Vector3 targetScale, float smoothTime)
    {
        while (Vector3.Distance(targetObject.transform.localScale, targetScale) >= 0.1)
        {
            targetObject.transform.localScale = Vector3.SmoothDamp(targetObject.transform.localScale, targetScale, ref scaleVelocity, smoothTime);
            yield return null;
        }

        targetObject.transform.localScale = targetScale;
    }
}