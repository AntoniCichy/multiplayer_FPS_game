using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        // Now that we have the client's id, connect UDP
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        bool _team = _packet.ReadBool();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username,_team,_position, _rotation);

        Debug.Log("PLAYER SPAWNED");
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        
        if (Client.instance.myId != _id)
        {
            GameManager.players[_id].transform.position = _position;
            
        }
        //Debug.Log("position" + _position);


    }
    public static void CheckPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Debug.Log(_position);
        int _tick = _packet.ReadInt();
        Debug.Log(_tick);

        GameManager.players[_id].GetComponent<PlayerLikeServer>().Check(_position, _tick);
        
        

        //Debug.Log("position"+_position);
    }
    
    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        if (Client.instance.myId != _id)
        {
            Quaternion _rotation = _packet.ReadQuaternion();

            GameManager.players[_id].transform.rotation = _rotation;
        }
       
    }
    public static void CamRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        if (Client.instance.myId != _id)
        {
            Quaternion _Camrotation = _packet.ReadQuaternion();
            GameManager.players[_id].transform.GetChild(1).transform.rotation = _Camrotation;
        }
        
    }
    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }
    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].SetHealth(_health);
    }
    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();
        GameManager.players[_id].Respawn();
    }
}
