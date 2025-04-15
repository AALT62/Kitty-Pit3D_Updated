using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    public float fallTime = 0.5f;  // Time before fading starts
    public float fadeDuration = 2.0f;  // Time it takes to fully fade out

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(FadeOutAndDestroy(fallTime, fadeDuration));
            }
        }
    }

    IEnumerator FadeOutAndDestroy(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);  // Wait for fallTime before starting fade

        Color originalColor = objectRenderer.material.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);  // Linearly interpolate alpha value
            objectRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);  // Update color with new alpha
            yield return null;  // Wait for the next frame
        }

        Destroy(gameObject);  // Destroy the object after the fade-out is complete
    }
}
