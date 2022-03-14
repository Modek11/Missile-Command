using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameHandler : MonoBehaviour
{
    #region Variables

    public static int castleCounter = 6;
    public static float howMuchEnemies = 5;
    float enemiesOnRound = howMuchEnemies;
    public static int score = 0;
    public static int missilesLeft;
    public static float enemySpeed = 100;
    public static float enemyFastSpeed = enemySpeed * 1.5f;
    public static int whichRoundIsIt = 1;
    public static int playerMovementSpeed = 1000;
    public static float explosionTime = 0.7f;
    public static Vector3 explosionScale = new Vector3(0.5f, 0.5f);
    int[] scoreBoardValues = new int[5];
    bool isScoreAdded = false;
    char saveSeparator = ':';
    bool isGamePaused = false;
    private AudioSource[] allAudioSources;

    #endregion

    #region GameObjects

    public GameObject GameOverScreen;
    public GameObject PauseGameScreen;
    public GameObject Castles;
    public Text ScoreBoard;
    public Text MissilesLeft;
    public Text WhichRoundIsIt;
    public Text HowManyEnemiesLeft;
    public Text ScoreBoardText;

    #endregion

    void Start()
    {
        missilesLeft = Mathf.RoundToInt(howMuchEnemies * 1.25f);
        LoadScoreBoard();
    }

    void Update()
    {
        PlayerLose();

        PlayerWinRound();

        PlayerUI();

        PlayerPauseGame();
    }

    void PlayerWinRound()
    {
        if (howMuchEnemies <= 0 && GameObject.Find("Enemy(Clone)") == null && GameObject.Find("EnemyFast(Clone)") == null)
        {
            enemiesOnRound += 5;
            howMuchEnemies = enemiesOnRound;
            enemySpeed += 50;
            enemyFastSpeed = enemySpeed * 1.25f;
            missilesLeft = Mathf.RoundToInt(howMuchEnemies * 1.25f);
            whichRoundIsIt += 1;
            playerMovementSpeed += 20;
            Destroy(GameObject.Find("Castles(Clone)"));
            GameObject.Instantiate(Castles);
            GameHandler.score += castleCounter * 50;
            castleCounter = 6;
        }
    }

    void PlayerLose()
    {
        if (castleCounter == 0)
        {
            UpdateScoreBoard();
            SaveScoreBoard();
            PauseAllAudio();

            GameOverScreen.GetComponent<AudioSource>().UnPause();
            GameOverScreen.SetActive(true);
            Time.timeScale = 0f;
            Destroy(GameObject.Find("EnemyFast(Clone)"));
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        castleCounter = 6;
        howMuchEnemies = 5;
        score = 0;
        enemySpeed = 100;
        enemyFastSpeed = enemySpeed * 1.5f;
        whichRoundIsIt = 1;
        playerMovementSpeed = 1000;
        explosionTime = 0.7f;
        isScoreAdded = false;
        GameOverScreen.SetActive(false);
        SceneManager.LoadScene("Game");
        UnPauseAllAudio();
    }

    public void PlayerPauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                PauseGameScreen.SetActive(true);
                Time.timeScale = 0f;
                PauseAllAudio();
                isGamePaused = true;
            }
            else
            {
                PauseGameScreen.SetActive(false);
                Time.timeScale = 1f;
                UnPauseAllAudio();
                isGamePaused = false;
            }
        }
        
    }

    void PauseAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Pause();
        }
    }

    void UnPauseAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.UnPause();
        }
    }

    void PlayerUI()
    {
        MissilesLeft.text = missilesLeft.ToString();
        ScoreBoard.text = $"Score: {score}";
        WhichRoundIsIt.text = $"Round: {whichRoundIsIt}";
        HowManyEnemiesLeft.text = $"Enemies Left: {howMuchEnemies}";
    }

    void UpdateScoreBoard()
    {
        //ScoreBoard don't save negative points
        if (isScoreAdded == false)
        {
            for (int i = 0; i < scoreBoardValues.Length; i++)
            {
                if (score > scoreBoardValues[i])
                {
                    scoreBoardValues[4] = score;
                    break;
                }
            }
            System.Array.Sort(scoreBoardValues);
            System.Array.Reverse(scoreBoardValues);
            isScoreAdded = true;
        }

        ScoreBoardText.text = $"1. {scoreBoardValues[0]}\n" +
            $"2. {scoreBoardValues[1]}\n" +
            $"3. {scoreBoardValues[2]}\n" +
            $"4. {scoreBoardValues[3]}\n" +
            $"5. {scoreBoardValues[4]}";
    }

    void SaveScoreBoard()
    {
        string[] scoreBoardValuesString = new string[5];
        for(int i = 0; i<scoreBoardValues.Length; i++)
        {
            scoreBoardValuesString[i] = scoreBoardValues[i].ToString();
        }
        string saveString = string.Join(""+saveSeparator, scoreBoardValuesString);
        File.WriteAllText(Application.dataPath + "/save.txt", saveString);
    }

    void LoadScoreBoard()
    {
        if(File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            string[] scoreBoardValuesString = saveString.Split(saveSeparator);
            for (int i = 0; i < scoreBoardValuesString.Length; i++)
            {
                scoreBoardValues[i] = int.Parse(scoreBoardValuesString[i]);
            }
        }
    }
}
