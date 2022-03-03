//Author: Dominik Dohmeier
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Button button;

    System.Action<string> action;

    public void SetText(string text, System.Action<string> callback)
    {
        buttonText.SetText(text);
        action = callback;
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        action?.Invoke(buttonText.text);
    }
}