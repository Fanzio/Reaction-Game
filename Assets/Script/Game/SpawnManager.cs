using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("Prefab da istanziare")]
    public GameObject prefabToSpawn;

    [Tooltip("Intervallo in secondi tra gli spawn")]
    public float spawnInterval = 1f;

    [Tooltip("Telecamera utilizzata per determinare il range di spawn. Se vuota, verrà usata la Camera.main")]
    public Camera spawnCamera;

    [Tooltip("Distanza dalla telecamera in cui far apparire il prefab")]
    public float spawnDistance = 10f;

    [Header("Margini per lo Spawn (valori normalizzati 0-1)")]
    [Tooltip("Margine orizzontale per evitare i bordi e la UI")]
    public float spawnMarginX = 0.1f;
    [Tooltip("Margine per la parte inferiore (basso)")]
    public float spawnMarginYBottom = 0.0f;
    [Tooltip("Margine per la parte superiore (alto)")]
    public float spawnMarginYTop = 0.3f; // Esclude il 30% della parte superiore

    [Header("Distanza minima tra i nemici")]
    [Tooltip("Distanza minima richiesta tra i nemici spawnati")]
    public float minDistanceBetweenEnemies = 2f;

    void Start()
    {
        if (spawnCamera == null)
        {
            spawnCamera = Camera.main;
        }
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnPrefab();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnPrefab()
    {
        Vector3 spawnPos = Vector3.zero;
        bool validPos = false;
        int attempts = 0;
        int maxAttempts = 10; // Numero massimo di tentativi per trovare una posizione valida

        while (!validPos && attempts < maxAttempts)
        {
            // Genera coordinate casuali rispettando i margini definiti:
            float randomX = Random.Range(spawnMarginX, 1f - spawnMarginX);
            float randomY = Random.Range(spawnMarginYBottom, 1f - spawnMarginYTop);
            Vector3 viewportPos = new Vector3(randomX, randomY, spawnDistance);

            // Converte le coordinate dalla viewport a quelle del mondo
            spawnPos = spawnCamera.ViewportToWorldPoint(viewportPos);

            // Controlla se la posizione è sufficientemente distante da tutti gli enemy già presenti
            bool tooClose = false;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, spawnPos) < minDistanceBetweenEnemies)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                validPos = true;
            }
            attempts++;
        }

        // Se dopo maxAttempts non troviamo una posizione valida, usiamo comunque l'ultima posizione generata.
        Instantiate(prefabToSpawn, spawnPos, Quaternion.Euler(0, 180, 0));

    }
}