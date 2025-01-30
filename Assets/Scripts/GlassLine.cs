using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlassLine : MonoBehaviour
{
    [SerializeField] private Image[] images = new Image[3];
    [SerializeField] private Color[] colors = new Color[3];
    public int[] hitCounts = new int[3];
    private string[] glassTags = { "Glass1", "Glass2", "Glass3" };
    //private string[] candyTags = { "Candy1", "Candy2", "Candy3" };

    /*private Dictionary<string, string> glassVsCandy = new Dictionary<string, string>
    {
        { "Glass1", "Candy1" },
        { "Glass2", "Candy2" },
        { "Glass3", "Candy3" }
    };*/

    private int glassID;
    private bool[] done = new bool[3];

    private void Start()
    {
        glassID = Array.IndexOf(glassTags, gameObject.tag);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.transform.parent.CompareTag("InGlass") || collision.CompareTag("Trap"))
            return;*/

        #region for
        /*bool isMatched = false;
        for (int i = 0; i < glassTags.Length; i++)
        {
            if (gameObject.CompareTag(glassTags[i]) && collision.CompareTag(candyTags[i]))
            {
                collision.tag = "InGlass";
                collision.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
                collision.GetComponent<Rigidbody2D>().velocity /= 4;
                hitCounts[i]++;

                if (hitCounts[i] >= 25)
                {
                    images[i].color = colors[i];
                    if (CheckWin()) GameManager4.instance.GameWin();
                }
                isMatched = true;
                break;
            }
        }

        if (!isMatched)
        {
            Candy candy = collision.GetComponent<Candy>();
            if (candy != null)
            {
                candy.DestroyCandy();
                Life.instance.DecreaseLife();
            }
        }*/
        #endregion

        #region Dictionary
        /*if (glassVsCandy.TryGetValue(gameObject.tag, out string candyTag) && parentTag == candyTag)
        {
            collision.transform.parent.tag = "InGlass";
            var rb = collision.GetComponentInParent<Rigidbody2D>();
            rb.gravityScale = 0.3f;
            rb.velocity /= 4;

            //int index = Array.IndexOf(glassTags, gameObject.tag);
            hitCounts[index]++;

            if (hitCounts[index] >= 25)
            {
                images[index].color = colors[index];
                if (CheckWin()) GameManager4.instance.GameWin();
            }
        }
        else
        {
            //Candy candy = collision.GetComponentInParent<Candy>();
            if (candy != null)
            {
                candy.DestroyCandy();
                Life.instance.DecreaseLife();
            }
        }*/
        #endregion

        Candy candy = collision.GetComponentInParent<Candy>();
        if (done[glassID] || candy == null) return;

        if (glassID != candy.candyID)
        {
            candy.DestroyCandy();
            Life.instance.DecreaseLife();
            return;
        }

        if (collision.transform.parent.tag == "InGlass") return;
        // else 
        collision.transform.parent.tag = "InGlass";
        var rb = collision.GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = 0.3f;
        rb.velocity /= 4;

        hitCounts[glassID]++;
        if (hitCounts[glassID] >= 35)
        {
            images[glassID].color = colors[glassID];
            if (CheckWin()) GameManager4.instance.GameWin();
            done[glassID] = true;
        }
    }

    private bool CheckWin()
    {
        for (int i = 0; i < hitCounts.Length; i++)
            if (images[i].color != colors[i]) return false;
        return true;
    }
}
