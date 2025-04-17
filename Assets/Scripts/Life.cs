using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Life : Singleton<Life>
{
    [HideInInspector] public int lifeCount = 5;
    [SerializeField] private TextMeshProUGUI lifeText;

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = lifeCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseLife()
    {
        lifeCount = Mathf.Max(0, lifeCount - 1);
        lifeText.text = lifeCount.ToString();
        if (lifeCount == 0)
            GameManager4.Instance.GameLose();
    }
}
