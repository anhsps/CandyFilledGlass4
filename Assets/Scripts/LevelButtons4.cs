using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtons4 : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            Button button = levelButtons[i];
            Image buttonImage = button.GetComponent<Image>();
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

            if (levelIndex <= unlockedLevel)
            {
                // Level da mo khoa
                button.interactable = true;
                button.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(true);

                button.onClick.AddListener(() => LoadLevel(levelIndex));
            }
            else
            {
                // Level chua mo khoa
                button.interactable = false;
                button.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
            }

            if (buttonText)
                buttonText.text = levelIndex < 10 ? "0" + levelIndex : levelIndex.ToString();
        }
    }

    void Update()
    {

    }

    private void LoadLevel(int levelIndex) => GameManager4.instance.SetCurrentLV(levelIndex);
}
