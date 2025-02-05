using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager4 : MonoBehaviour
{
    public static GameManager4 instance { get; private set; }
    public static int level = 1;

    [SerializeField] private GameObject winMenu, loseMenu;
    [SerializeField] private TextMeshProUGUI lvText;
    [SerializeField] private AudioSource win_audio, lose_audio;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else instance = this;

        if (lvText)
            lvText.text = "LEVEL " + (level < 10 ? "0" + level : level);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame() => SceneManager.LoadScene("StartGame");
    public void PauseGame() => Time.timeScale = 0f;
    public void ResumeGame() => Time.timeScale = 1f;
    public void Retry() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void NextLV()
    {
        level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameWin()
    {
        UnlockNextLevel(level);
        StartCoroutine(GameDeplay(winMenu, win_audio));
    }

    public void GameLose()
    {
        StartCoroutine(GameDeplay(loseMenu, lose_audio));
    }

    public IEnumerator GameDeplay(GameObject gameObject, AudioSource audio)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.SetActive(true);
        if (SoundManager4.instance.soundEnabled)
            audio.Play();
    }

    public void UnlockNextLevel(int currentLevel)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }
    }

    public void SetCurrentLV(int levelIndex)
    {
        level = levelIndex;
        SceneManager.LoadScene(levelIndex.ToString());
    }
}
