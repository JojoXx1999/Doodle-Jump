using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementScore : MonoBehaviour
{
    public static IncrementScore Instance;

    LevelGeneration lvl;

    //Create UI text for the scores
    [SerializeField]
    private Text currentScore, highScore;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void UpdateScore(int newScore)
    {
        currentScore.text = newScore.ToString();
    }

    public void UpdateHighScore(int newScore)
    {
        highScore.text = "HighScore: " + newScore.ToString();
    }
}
