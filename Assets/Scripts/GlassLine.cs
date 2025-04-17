using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassLine : MonoBehaviour
{
    public int glassID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Candy candy = collision.GetComponentInParent<Candy>();
        if (CandyCollect.Instance.done[glassID] || candy == null) return;

        if (glassID != candy.candyID)
        {
            candy.DestroyCandy();
            Life.Instance.DecreaseLife();
            return;
        }

        if (collision.transform.parent.tag == "InGlass") return;
        

        collision.transform.parent.tag = "InGlass";
        var rb = collision.GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = 0.3f;
        rb.velocity /= 4;

        CandyCollect.Instance.HandlerHit(glassID);
    }
}
