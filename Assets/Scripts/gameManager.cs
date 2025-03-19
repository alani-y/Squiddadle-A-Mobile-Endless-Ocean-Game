using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public float score = 0;
    public squidScript squid;
    private saveManager saveManager;
    public Text scoreLabel;
    public Text highScoreLabel;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = GetComponent<saveManager>();
        // Prints the highest score
        highScoreLabel.text = "High Score: " + saveManager.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        saveManager.UpdateScore(score);
        scoreLabel.text = "Score: " + squid.score.ToString();
    }

    public void GameOver(){
        gameOverScreen.gameObject.SetActive(true);
    }
}
