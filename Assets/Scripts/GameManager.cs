using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject camera1;
    public GameObject mainCamera;
    public GameObject playerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    /// <summary>Spawns a player.</summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_name">The player's name.</param>
    /// <param name="_team">The player's team.</param>
    /// <param name="_position">The player's starting position.</param>
    /// <param name="_rotation">The player's starting rotation.</param>
    public void SpawnPlayer(int _id, string _username,bool _team,Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            camera1.SetActive(true);
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
            
            mainCamera.SetActive(false);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
            
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username,_team);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }
}
