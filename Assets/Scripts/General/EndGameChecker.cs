using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameChecker : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        Player[] players = FindObjectsOfType<Player>();
        int liveCount = 0;
        foreach (Player player in players)
        {
            if (player.GetLives() != 0) liveCount++;
        }
        if (liveCount == 0)
            GameOver();
    }

    public void GameOver()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            Destroy(player);
        }
        SceneManager.LoadSceneAsync(1);
    }
}
