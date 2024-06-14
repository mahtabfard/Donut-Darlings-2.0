using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ColorOption
{
    public string Name;
    public Color Value;
}

public class Game_Handler : MonoBehaviour
{
    public List<ColorOption> ColorOptions;
    public TextMeshProUGUI ColorNameDisplay;
    public Color_Butten[] ColorButtons;

    public int targetColorIndex;
    private int score;
    private float timer;
    private bool isGameActive;

    [Header("UI Elements")]
    public TextMeshProUGUI ScoreDisplay;
    public TextMeshProUGUI TimerDisplay;
    public GameObject WinPanel;
    public GameObject LosePanel;

    [Header("Audio")]
    public AudioSource CorrectSound;
    public AudioSource IncorrectSound;

    [Header("Game Settings")]
    public float InitialTime = 5f;
    public float DelayBeforeStart = 3f;
    public int WinningScore = 100;

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        score = 0;
        UpdateScoreDisplay();
        StartCoroutine(StartGameAfterDelay(DelayBeforeStart));
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AssignColorsToButtons();
        isGameActive = true;
        timer = InitialTime;
    }

    private void AssignColorsToButtons()
    {
        List<int> chosenIndices = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, ColorOptions.Count);
            }
            while (chosenIndices.Contains(randomIndex));

            chosenIndices.Add(randomIndex);
            ColorButtons[i].SetColor(ColorOptions[randomIndex]);
            ColorButtons[i].Game_Handler = this;
        }

        targetColorIndex = UnityEngine.Random.Range(0, chosenIndices.Count);
        ColorNameDisplay.text = ColorOptions[chosenIndices[targetColorIndex]].Name;
        targetColorIndex = chosenIndices[targetColorIndex];
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;
        TimerDisplay.text = $"Timer: {timer:F2}";

        if (timer <= 0)
        {
            HandleIncorrectSelection();
        }
    }

    
}
