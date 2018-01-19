using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/* Listens for key presses and assigns players to the individual pressing the key. */
public class LobbySpawn : MonoBehaviour
{
    private const string KEY_TRIGGER = "TriggerP";
    private const string KEY_START = "StartButtonJ1";
    private const int MAX_PLAYERS = 10;
    private const float DRIFT_THRESHOLD = 0.75f;

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject playerSpawn;

    private List<int> spawnedPlayers = new List<int>();

    public void Update()
    {
        CheckPlayerJoin();
        OnButtonStartGame();
    }

    /* For every player, check if their trigger is pressed.
     * If this is the case, and they are not spawned, spawn them. */
    private void CheckPlayerJoin()
    {
        for (int player = 0; player < MAX_PLAYERS; player++)
            if (Input.GetAxisRaw(KEY_TRIGGER + player) > DRIFT_THRESHOLD)
                if (!spawnedPlayers.Contains(player))
                {
                    spawnedPlayers.Add(player);
                    // Create new player.
                    GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                    go.GetComponent<Control>().SetPlayerNumber(player);
                }
    }

    /* Checks if the player wants to start the game and leave the lobby.
     * If so, we load the Game Scene whilst keeping the players loaded. */
    private void OnButtonStartGame()
    {
        if (Input.GetButtonDown(KEY_START) || Input.GetKeyDown(KeyCode.Return))
            if (spawnedPlayers.Count > 0) 
            {
                Object.DontDestroyOnLoad(playerSpawn);
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
    }

    private void LateUpdate()
    {
        ClampPlayers();
    }

    /* Players are disallowed to move outside of the screen. Preventing death. */
    private void ClampPlayers()
    {
        for (int index = 0; index < playerSpawn.transform.childCount; ++index)
        {
            var pos = Camera.main.WorldToViewportPoint(playerSpawn.transform.GetChild(index).transform.position);
            pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
            pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
            playerSpawn.transform.GetChild(index).transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }
}