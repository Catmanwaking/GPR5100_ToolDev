//Author: Dominik Dohmeier
using Assets.Scripts.Tiles;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private const float CAM_WIDTH = 15f;
    private const float CAM_HEIGHT = 10f;

    [SerializeField] private Transform followPoint;
    [SerializeField] private MapDataSO map;

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;

    private Vector3 position;

    private void Start()
    {
        int width = map.MapTiles.GetLength(0);
        int height = map.MapTiles.GetLength(1);

        minX = CAM_WIDTH * 0.5f - 0.5f;
        minY = CAM_HEIGHT * 0.5f - 0.5f;

        maxX = (width - CAM_WIDTH) + minX;
        maxY = (height - CAM_HEIGHT) + minY;

        position = transform.position;
    }

    public void UpdateCamera()
    {
        position.x = Mathf.Clamp(followPoint.position.x, minX, maxX);
        position.y = Mathf.Clamp(followPoint.position.y, minY, maxY);
        transform.position = position;
    }
}