using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float spawnTime;

    void Start()
    {
        // Registra il tempo in cui il nemico è stato istanziato
        spawnTime = Time.time;
    }
}