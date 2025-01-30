using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CandyTap : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressing;

    private float spawnTime = 0.13f;
    private Coroutine spawnCoroutine;

    public GameObject candyPrefab;

    //**
    private Queue<GameObject> candyPool = new Queue<GameObject>(); // Queue cho Pool
    public int poolSize = 10;

    // Start is called before the first frame update
    void Start()
    {//**
        // Tao Pool dtg Candy
        for (int i = 0; i < poolSize; i++)
        {
            GameObject candy = Instantiate(candyPrefab);
            candy.SetActive(false);
            candy.GetComponent<Candy>().SetCandyTap(this);// Gan CandyTap cho moi Candy
            candyPool.Enqueue(candy); // Them vao Pool
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Tap()
    {
        Vector2 localPos;
        RectTransform rt = GetComponent<RectTransform>();

        // change pos: mousePos in screen space -> local space (rt cua button)
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, Camera.main, out localPos))
        {
            Vector2 randomPos = new Vector2(Random.Range(localPos.x - 20f, localPos.x + 20f), localPos.y);
            Vector2 spawnPos = rt.TransformPoint(randomPos);// pos local space -> world space

            //Instantiate(candyPrefab, spawnPos, Quaternion.identity);

            //**
            // Lay Candy tu Pool or tao moi
            GameObject candy = GetPooledCandy();
            candy.transform.position = spawnPos;
            candy.SetActive(true);
        }
    }

    // check mousePos nam tren hcn cua rt (Button UI) ko 
    private bool IsPointerOverBtn()
    {
        RectTransform rt = GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(rt, Input.mousePosition, Camera.main);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        spawnCoroutine = StartCoroutine(SpawnCandy());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnCandy()
    {
        while (isPressing)
        {
            yield return new WaitForSeconds(spawnTime);
            if (IsPointerOverBtn()) Tap();
        }
    }

    //**
    private GameObject GetPooledCandy()
    {
        if (candyPool.Count > 0)
            return candyPool.Dequeue(); // Lay dtg Candy tu Pool
        else
        {
            // Neu Pool k con dtg -> tao ms va them vao Pool
            GameObject candy = Instantiate(candyPrefab);
            candy.GetComponent<Candy>().SetCandyTap(this);
            return candy;
        }
    }

    // Tra dtg candy ve Pool khi k sd
    public void ReturnCandyToPool(GameObject candy)
    {
        candy.SetActive(false);
        candyPool.Enqueue(candy);
    }
}
