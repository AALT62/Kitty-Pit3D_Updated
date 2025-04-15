using UnityEngine;

public class DiggableBlock : MonoBehaviour
{
    public int tier = 1;
    public float baseDigTime => 1f + (tier - 1) * 0.5f;
}


