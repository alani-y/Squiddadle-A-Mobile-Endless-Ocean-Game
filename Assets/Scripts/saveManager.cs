using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

// manages saving the user's high score
public class saveManager : MonoBehaviour
{
    private GameData currentData; // data save
    public squidScript squid;
    private string filePath;
    private float highScore; // stores the highest score
    private float timer;
    // Start is called before the first frame update
    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "squidSave.json");

        try {
            // Reads the game's score in the squidSave.json file
            using (StreamReader reader = new StreamReader(filePath)){
                string dataToLoad = reader.ReadToEnd();
                currentData = JsonUtility.FromJson<GameData>(dataToLoad);
                highScore = currentData.score;
                Debug.Log("loaded high score: " + highScore);
                Debug.Log("has alternate squid: " + currentData.hasAlternateSquid);
            }
        }
        catch (Exception e)
        {
            // creates a new save if there's a file problem
            Debug.Log("Start Exception error: " + e.Message);
            currentData = new GameData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateScore(squid.score);

        if(timer >= 3){ // Saves the game once every 3 seconds
            // checks if the score is higher than the user's highest score
            if(highScore < currentData.score){
                saveGame();
                // sets the highest score to the user's current score
                highScore = currentData.score;
            }
            timer = 0;
        }
    }

    public void UpdateScore(float newScore){
        currentData.score = newScore;
    }

    public float GetScore(){
        return currentData.score;
    }

    public void addAlternateSquid(){
        currentData.hasAlternateSquid = true;
        saveGame();
    }

    private void saveGame(){
        try
        {
            string dataString = JsonUtility.ToJson(currentData);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(dataString);
                Debug.Log("saved score: " + currentData.score);
            }
        }
        catch(IOException e)
        {
            Debug.Log("file reading or writing error: " + e.Message);
        }
    }
}

// the save data for the game
[System.Serializable]
public class GameData{
    public float score;
    public bool hasAlternateSquid = false;
}