using UnityEngine;

public class Diggable : MonoBehaviour
{
    public float digDifficultyMultiplier = 1f; // line 6
    public void Dig()
    {
        Destroy(gameObject);
    }
}
