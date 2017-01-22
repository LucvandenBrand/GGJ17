using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameChecker : MonoBehaviour {
    [SerializeField]
    private Canvas winScreen;
    [SerializeField]
    private QuoteMaker quoteMaker;
    private bool gameOver = false;

    // Update is called once per frame
    void Update() {
        Player[] players = FindObjectsOfType<Player>();
        int liveCount = 0;
        foreach (Player player in players)
        {
            if (player.GetLives() != 0) liveCount++;
        }

        // Check for gameover.
        if (liveCount == 0)
        {
            OnButtonStartGame();
            if (!gameOver)
            {
                ShowLeaderBoard();
                gameOver = true;
            } else
            {
                SetLeaderBoardPosition(players);
            }
        }
    }

    // Show the leaderboard.
    private void ShowLeaderBoard()
    {
        Player[] players = FindObjectsOfType<Player>();
        Canvas screen = Instantiate(winScreen);
        screen.transform.Find("Quote").GetComponent<Text>().text = quoteMaker.GetQuote();
        SetScores(screen, players);
    }

    private int GetWinner(Player[] players)
    {
        int winner = 0;
        float maxLiveTime = 0;
        for (int i=0; i<players.Length; ++i)
        {
            if (players[i].GetLiveTime() > maxLiveTime)
            {
                maxLiveTime = players[i].GetLiveTime();
                winner = i;
            }
        }
        return winner;
    }

    private void SetLeaderBoardPosition(Player[] players)
    {
        if (players.Length == 0) {
            Debug.LogWarning("No Players exists on the leader board");
            return;
        }

        int winner = GetWinner(players);
        players[winner].ShowPlayer(true);
        players[winner].GetComponent<Control>().movementSpeed = 0;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.11f, Screen.height * 0.7f));
        newPos.z = 2;
        players[winner].transform.position = newPos;
        players[winner].transform.localScale = new Vector3(5, 5, 5);

        float stepX = 0.11f;
        int stepCount = 1;
        for (int i = 0; i < players.Length; ++i)
        {
            if (i != winner)
            {
                players[i].ShowPlayer(true);
                players[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                newPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * stepCount * stepX, Screen.height * 0.2f));
                newPos.z = 2;
                players[i].transform.position = newPos;
                players[i].transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
                stepCount++;
            }
        }
    }

    private void SetScores(Canvas canvas, Player[] players)
    {
        int winner = GetWinner(players);
        int stepCount = 2;
        for (int i = 0; i < players.Length; ++i)
        {
            string objName = "ScoreP";
            if (i == winner)
            {
                objName += 1;
            } else
            {
                objName += stepCount++;
            }
            string secondsStr = formatSeconds((int)players[i].GetLiveTime());
            canvas.transform.Find(objName).GetComponent<Text>().text = secondsStr;
        }
    }

    private string formatSeconds(int rawSeconds)
    {

        int minutes = rawSeconds / 60;
        int seconds = rawSeconds % 60;
        string secondsStr = "";
        secondsStr += minutes + ":";
        if (seconds < 10)
        {
            secondsStr += "0";
        }
        return secondsStr += seconds;
    }

    // Delete players and reenter lobby.
    private void ReturnToLobby()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            player.transform.SetParent(gameObject.transform);
        }
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }

    void OnButtonStartGame()
    {
        if (Input.GetButtonDown("StartButtonJ1") == true)
            ReturnToLobby();
    }
}
