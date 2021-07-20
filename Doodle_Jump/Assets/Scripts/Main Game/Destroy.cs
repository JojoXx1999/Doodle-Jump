using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject LevelManager;
    public GameObject player;
    public GameObject cam;

    private GameObject[] levels;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name!= "Top" && collision.gameObject.name!= "jiggleboi" && collision.gameObject.name != "Moving")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name == "Top")
        {
            if (collision.transform.parent != null)
                Destroy(collision.transform.parent.gameObject);

            LevelManager.GetComponent<LevelGeneration>().newLevel();
            //Select new level
        }
        else if (collision.gameObject.name == "jiggleboi")
        {
            Debug.Log("Player Died");

            LevelManager.GetComponent<LevelGeneration>().Reset();


            levels = GameObject.FindGameObjectsWithTag("Level");
            foreach (GameObject obj in levels)
            {
                Destroy(obj);
            }

            for (int i = 0; i < 3; i++)
            {
                LevelManager.GetComponent<LevelGeneration>().newLevel();
            }

            player.GetComponent<Player>().Reset();
            cam.GetComponent<CameraFollow>().Reset();

            //Reset game
        }
    }
}
