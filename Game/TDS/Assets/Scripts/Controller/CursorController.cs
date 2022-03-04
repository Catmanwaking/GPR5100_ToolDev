//Author: Dominik Dohmeier
using Assets.Scripts.Tiles;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    [SerializeField] private MapDataSO map;
    [Header("Cursor Data")]
    [SerializeField] private Transform cursor;
    [SerializeField] private float repeatHoldThreshold;
    [SerializeField] private float repeatDelay;
    [SerializeField] private UnityEvent cursorMoved;

    private float maxX;
    private float maxY;

    private Vector2 currentInput;
    private Vector2 pos;

    private Coroutine cursorRoutine;

    private void Start()
    {
        maxX = map.MapTiles.GetLength(0) - 1;
        maxY = map.MapTiles.GetLength(1) - 1;
    }

    private void Move(Vector2 input)
    {
        pos.x = Mathf.Clamp(pos.x + input.x, 0, maxX);
        pos.y = Mathf.Clamp(pos.y + input.y, 0, maxY);
        cursor.position = pos;
        cursorMoved.Invoke();
    }

    private IEnumerator RepeatMove()
    {
        Vector2 coroutineInput = currentInput;
        yield return new WaitForSeconds(repeatHoldThreshold);
        while (true)
        {
            Move(currentInput);
            yield return new WaitForSeconds(repeatDelay);
        }
    }

    private void OnMove(InputValue input)
    {
        currentInput = input.Get<Vector2>();
        if(currentInput == Vector2.zero)
        {
            StopCoroutine(cursorRoutine);
            return;
        }
        Move(currentInput);
        cursorRoutine = StartCoroutine(RepeatMove());
    }

    private void OnReturn()
    {
        SceneManager.LoadScene(0);
    }
}