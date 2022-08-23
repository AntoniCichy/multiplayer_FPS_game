using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public bool team;
    public float health;
    public float maxHealth = 100f;
    public int itemCount = 0;
    public Camera cam1;
    public GameObject cam;
    public MeshRenderer model;

    public void Initialize(int _id, string _username,bool _team)
    {
        cam1 = FindObjectOfType<Camera>();
        cam = cam1.gameObject;
        id = _id;
        username = _username;
        team = _team;
        health = maxHealth;
    }

    public void SetHealth(float _health)
    {
        health = _health;

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        model.enabled = false;
    }

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
    }
}
