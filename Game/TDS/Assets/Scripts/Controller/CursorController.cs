using Assets.Scripts.Tiles;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    [SerializeField] private MapDataSO map;
    [SerializeField] private Transform cursor;

    Vector2 pos;

    private float maxX;
    private float maxY;

    private void Start()
    {
        maxX = map.MapTiles.GetLength(0) - 1;
        maxY = map.MapTiles.GetLength(1) - 1;
    }

    private void Move(float x, float y)
    {
        pos.x = Mathf.Clamp(pos.x + x, 0, maxX);
        pos.y = Mathf.Clamp(pos.y + y, 0, maxY);
        cursor.position = pos;
    }

    private void OnMove(InputValue input)
    {
        Vector2 value = input.Get<Vector2>();
        Move(value.x, value.y);
    }

    private void OnReturn()
    {
        SceneManager.LoadScene(0);
    }
}
