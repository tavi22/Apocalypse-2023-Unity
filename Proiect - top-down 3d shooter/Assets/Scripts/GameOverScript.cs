using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript: MonoBehaviour
{

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    private ScoreManager playerScoreManager;
    private Canvas playerCanvas;
    public static string menu = "Menu";
    public GameObject gameOverUI;
    public TMP_InputField inputName;
    private int score;
    public string path = "";
    public string persistentPath = "";
    private JSONSavingScript playerData;

    void Start()
    {
        gameOverUI.SetActive(false);
        SetPaths();
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().currentHealth <= 0)
        {
            gameOverUI.SetActive(true);
            inputName.ActivateInputField();

            playerCanvas = GameObject.FindGameObjectWithTag("Score").GetComponent<Canvas>();
            playerScoreManager = playerCanvas.GetComponent<ScoreManager>();

            Time.timeScale = 0f;

            score = playerScoreManager.score;
            highScoreText.text = PlayerPrefs.GetInt("highscore").ToString();
            scoreText.text = score.ToString();

            //Debug.Log(inputName.text);


        }
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        playerData = new JSONSavingScript(inputName.text, score);
        SaveData();
        Time.timeScale = 1;
        SceneManager.LoadScene(menu);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
        //SceneManager.LoadScene(menu);
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";

        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    public void SaveData()
    {
        string savePath = path;
        Debug.Log("Saving data at " + savePath);
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.WriteLine(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        JSONSavingScript data = JsonUtility.FromJson<JSONSavingScript>(json);
        Debug.Log(data.ToString());
    }
}
