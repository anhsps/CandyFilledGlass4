using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyCollect : Singleton<CandyCollect>
{
    private Image[] images = new Image[3];
    [SerializeField] private Color[] colors = new Color[3];
    [SerializeField] private int[] hitCounts = new int[3];
    [HideInInspector] public bool[] done = new bool[3];

    // Start is called before the first frame update
    void Start()
    {
        images = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandlerHit(int glassID)
    {
        hitCounts[glassID]++;
        if (hitCounts[glassID] >= 35)
        {
            images[glassID].color = colors[glassID];
            if (CheckWin()) GameManager4.Instance.GameWin();
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
