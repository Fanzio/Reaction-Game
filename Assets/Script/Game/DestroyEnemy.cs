using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    void Update()
    {
        // Se il gioco è finito, non processiamo ulteriori input
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        // Colpo a segno
                        Enemy enemy = hit.transform.GetComponent<Enemy>();
                        float reactionTime = 0f;
                        if (enemy != null)
                        {
                            reactionTime = Time.time - enemy.spawnTime;
                        }

                        if (GameManager.Instance != null)
                        {
                            GameManager.Instance.AddEnemyDestroyed(reactionTime);
                            GameManager.Instance.AddShot(true); // Aggiungi un colpo a segno
                        }

                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        // Colpo sbagliato
                        if (GameManager.Instance != null)
                        {
                            GameManager.Instance.AddShot(false); // Colpo mancato
                        }
                    }
                }
            }
        }
    }
}
