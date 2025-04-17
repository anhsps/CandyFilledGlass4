using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    private Animator animator;
    private CandyTap candyTap;
    public int candyID;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap"))
            DestroyCandy();
    }

    public void DestroyCandy()
    {
        animator.SetTrigger("Broken");
        SoundManager4.Instance.PlaySound(4);

        StartCoroutine(Delay());
    }
    
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);// time wait anim

        if (candyTap != null)
            candyTap.ReturnCandyToPool(gameObject);
        else Destroy(gameObject);
    }

    public void SetCandyTap(CandyTap tap) => candyTap = tap;
}
