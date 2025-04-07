using System;
using UnityEngine;

public enum Dimension
{
    X,
    Y,
    Z
}

public class WorldSettings : MonoBehaviour
{
    private static WorldSettings instance;
    public static WorldSettings Instance => instance;

    [Header("Settings")]
    public int flockSize = 20;
    public float avoidWallsScale = 3f;
    public float neighborRadarRadius = 10f;
    public float avoidNeighborsScale = 0.8f;
    public float alignWithNeighborsScale = 1.5f;
    public float cohesionScale = 0.25f;

    [Header("Dimension")]
    public Vector3 moveableSpace;
    public Vector3 centerPosition;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public float GetMinPosition(Dimension dimension)
    {
        switch (dimension)
        {
            case Dimension.X : return centerPosition.x - moveableSpace.x / 2f;
            case Dimension.Y : return centerPosition.y - moveableSpace.y / 2f;
            case Dimension.Z : return centerPosition.z - moveableSpace.z / 2f;
        }
        return 0f;
    }

    public float GetMaxPosition(Dimension dimension)
    {
        switch (dimension)
        {
            case Dimension.X : return centerPosition.x + moveableSpace.x / 2f;
            case Dimension.Y : return centerPosition.y + moveableSpace.y / 2f;
            case Dimension.Z : return centerPosition.z + moveableSpace.z / 2f;
        }
        return 0f;
    }
}