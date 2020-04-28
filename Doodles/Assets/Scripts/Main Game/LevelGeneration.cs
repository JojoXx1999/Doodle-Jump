using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] EasyLevels, MediumLevels, HardLevels;
    public GameObject destroyer;
    private GameObject currentLevel;
    private Camera mainCamera;
    public Text highScore, currentScore;

    Player player;

    public enum difficulty { EASY, MEDIUM, HARD };
    private difficulty currentDifficulty;
    private int currentHeight;

    private float halfHeight, halfWidth;
    private Vector2 currentLocation;

    // Start is called before the first frame update
    void Start()
    {
        currentDifficulty = difficulty.EASY;

        mainCamera = Camera.main;
        halfHeight = mainCamera.orthographicSize;
        halfWidth = mainCamera.aspect * halfHeight;
        Vector3 viewPos = mainCamera.WorldToViewportPoint(this.transform.position);
        currentLocation = new Vector2(2.11389f, -0.7697411f);
       
        for (int i = 0; i < 3; i++)
        {
            newLevel();
        }

        highScore.transform.position = new Vector2(0f, halfHeight - (highScore.transform.localScale.y/2));
        currentScore.transform.position = new Vector2(-halfWidth + (currentScore.transform.localScale.x), -halfHeight + (currentScore.transform.localScale.y / 2));

        destroyer.transform.localScale = new Vector2(halfWidth*2, destroyer.transform.localScale.y);
        destroyer.transform.position = new Vector3(0, -halfHeight - (destroyer.transform.localScale.y/2), 0);

    }

    public void newLevel()
    {
        switch (currentDifficulty) 
        {
            case difficulty.EASY:
            currentLevel = EasyLevels[Random.Range(0, EasyLevels.Length)];
                break;
            case difficulty.MEDIUM:
                currentLevel = MediumLevels[Random.Range(0, MediumLevels.Length)];
                break;
            case difficulty.HARD:
                currentLevel = HardLevels[Random.Range(0, HardLevels.Length)];
                break;
            default:
                break;
        }
        
            currentLevel = initObj(currentLevel, currentLocation);
            currentLocation.y += (halfHeight * 2);
            OffsetX(currentLevel);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        destroyer.transform.position = new Vector3(0, mainCamera.transform.position.y -halfHeight - (destroyer.transform.localScale.y / 2), 0);

        currentHeight = int.Parse(highScore.text);
        if (currentHeight >= 50 && currentDifficulty == difficulty.EASY)
        {
            currentDifficulty = difficulty.MEDIUM;
            Debug.Log("Medium");
        }
        else if (currentHeight >= 150 && currentDifficulty == difficulty.MEDIUM)
        {
            
            currentDifficulty = difficulty.HARD;
            Debug.Log("HARD");
            
        }
    }

    private void OffsetX(GameObject Level)
    {
        foreach (Transform child in Level.transform)
        {
           if (child.name != "Bottom")
            {
                child.position = new Vector2(Random.Range(-halfWidth, halfWidth), child.position.y);
            }

        }

    }

    private GameObject initObj(GameObject obj, Vector2 location)
    {
        obj = Instantiate(currentLevel, location, Quaternion.identity);
        return obj;
    }

    public void Reset()
    {
        currentLocation = new Vector2(2.11389f, -0.7697411f);
        currentDifficulty = difficulty.EASY;
    }
}
