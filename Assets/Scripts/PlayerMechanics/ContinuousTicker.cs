using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContinuousTicker : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private RectTransform textRect;
    [SerializeField] private TextMeshProUGUI textComponent;

    [Header("Settings")]
    [SerializeField] private float scrollSpeed = 100f;
    [SerializeField] private string separator = "          ";

    [Header("Messages")]
    [TextArea(2, 5)]
    [SerializeField] private List<string> messages = new List<string>();

    private float textWidth;
    private float startX;
    private RectTransform parentRect;

    private void Start()
    {
        parentRect = textRect.parent as RectTransform;

        RebuildTicker();
        SetStartPosition();
    }

    private void Update()
    {
        Vector2 pos = textRect.anchoredPosition;
        pos.x -= scrollSpeed * Time.deltaTime;

        if (pos.x <= -textWidth)
        {
            RebuildTicker();
            SetStartPosition();
            return;
        }

        textRect.anchoredPosition = pos;
    }

    private void SetStartPosition()
    {
        // Ширина родительской панели
        startX = parentRect.rect.width;

        Vector2 pos = textRect.anchoredPosition;
        pos.x = startX;
        textRect.anchoredPosition = pos;
    }

    private void RebuildTicker()
    {
        if (messages.Count == 0)
        {
            textComponent.text = "";
            return;
        }

        Shuffle(messages);

        string fullText = string.Join(separator, messages);
        textComponent.text = fullText;

        // Обновляем layout перед вычислением ширины
        Canvas.ForceUpdateCanvases();
        textWidth = textComponent.preferredWidth;
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public void AddMessage(string newMessage)
    {
        messages.Add(newMessage);
    }
}