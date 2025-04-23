using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

public class gameManager : MonoBehaviour
{
    private static gameManager instance;
    public float score = 0;
    public squidScript squid;
    private saveManager saveManager;
    public Text scoreLabel;
    public Text highScoreLabel;
    public GameObject gameOverScreen;

    public Button inkblotButton;

    [Header("Ink Event")]
    public UnityEvent onInkUsed;

    [Header("Ink Ability Active")]
    public UnityEvent onInkActive;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        saveManager = GetComponent<saveManager>();
        // Prints the highest score
        highScoreLabel.text = "High Score: " + saveManager.GetScore().ToString();
        onInkUsed.AddListener(disableInkblot);
        onInkActive.AddListener(enableInkblot);
    }

    // Update is called once per frame
    void Update()
    {
        saveManager.UpdateScore(score);
        scoreLabel.text = "Score: " + squid.score.ToString();

        if(squid.inkTimer > 0){
            onInkUsed.Invoke();
        }

        if(squid.inkTimer <= 0){
            onInkActive.Invoke();
        }

    }

    public void gameOver(){
        gameOverScreen.gameObject.SetActive(true);
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        squid.isAlive = true; // sets that the squid is alive
    }

    public void disableInkblot(){
        inkblotButton.interactable = false;
    }

    public void enableInkblot(){
        inkblotButton.interactable = true;
    }

    public static void addAlternateSquid(){
        instance.saveManager.addAlternateSquid();
    }
}
