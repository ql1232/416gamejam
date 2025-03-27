using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Hover Settings")]
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float hoverDuration = 0.2f;
    [SerializeField] private Color hoverColor = new Color(1f, 1f, 1f, 1f);
    
    [Header("Click Settings")]
    [SerializeField] private float clickScale = 0.95f;
    [SerializeField] private float clickDuration = 0.1f;
    
    private RectTransform rectTransform;
    private Image buttonImage;
    private Color originalColor;
    private Vector3 originalScale;
    private bool isHovered = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
        originalScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        AnimateScale(originalScale * hoverScale, hoverDuration);
        AnimateColor(hoverColor, hoverDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        AnimateScale(originalScale, hoverDuration);
        AnimateColor(originalColor, hoverDuration);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AnimateScale(originalScale * clickScale, clickDuration);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AnimateScale(isHovered ? originalScale * hoverScale : originalScale, clickDuration);
    }

    private void AnimateScale(Vector3 targetScale, float duration)
    {
        StartCoroutine(ScaleAnimation(targetScale, duration));
    }

    private void AnimateColor(Color targetColor, float duration)
    {
        StartCoroutine(ColorAnimation(targetColor, duration));
    }

    private System.Collections.IEnumerator ScaleAnimation(Vector3 targetScale, float duration)
    {
        Vector3 startScale = rectTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        rectTransform.localScale = targetScale;
    }

    private System.Collections.IEnumerator ColorAnimation(Color targetColor, float duration)
    {
        Color startColor = buttonImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            buttonImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        buttonImage.color = targetColor;
    }
} 