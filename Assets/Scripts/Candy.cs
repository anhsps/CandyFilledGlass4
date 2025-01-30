using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    private Animator animator;
    private CandyTap candyTap;
    private string[] candyTags = { "Candy1", "Candy2", "Candy3" };
    public int candyID;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        candyID = Array.IndexOf(candyTags, gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap"))
        {
            DestroyCandy();
        }
    }

    public void DestroyCandy()
    {
        animator.SetTrigger("Broken");
        if (SoundManager4.instance.soundEnabled)
            SoundManager4.instance.broken_audio.Play();
        //Destroy(gameObject, 0.25f);

        StartCoroutine(Delay());//
    }
    //**
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);

        if (candyTap != null)
            candyTap.ReturnCandyToPool(gameObject);
        else Destroy(gameObject);
    }

    public void SetCandyTap(CandyTap tap)
    {
        candyTap = tap;
    }
}
