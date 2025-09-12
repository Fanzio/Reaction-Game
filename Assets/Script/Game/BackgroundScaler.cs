using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        ResizeBackground();
    }
    void Update()
    {
        // Puoi aggiungere un controllo per verificare se la risoluzione è cambiata
        ResizeBackground();
    }
    void ResizeBackground()
    {
        Camera cam = Camera.main;
        float distance = Mathf.Abs(transform.position.z - cam.transform.position.z);

        float height = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * cam.aspect;

        transform.localScale = new Vector3(width / 10f, 1, height / 10f);
    }
}
