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

    [Header("Retro Style Settings")]
    [SerializeField] private float outlineWidth = 4f;
    [SerializeField] private Color outlineColor = Color.black;
    [SerializeField] private Vector2 shadowOffset = new Vector2(4f, -4f);
    [SerializeField] private Color shadowColor = new Color(0f, 0f, 0f, 0.5f);
    
    private RectTransform rectTransform;
    private Image buttonImage;
    private Color originalColor;
    private Vector3 originalScale;
    private bool isHovered = false;
    private Outline outline;
    private Image shadowImage;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
        originalScale = rectTransform.localScale;

        // Add outline component
        outline = gameObject.AddComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.effectDistance = new Vector2(outlineWidth, outlineWidth);

        // Create shadow
        CreateShadow();
    }

    private void CreateShadow()
    {
        // Create a new GameObject for the shadow
        GameObject shadowObj = new GameObject("ButtonShadow");
        shadowObj.transform.SetParent(transform.parent);
        shadowObj.transform.SetSiblingIndex(transform.GetSiblingIndex());
        
        // Copy the RectTransform settings
        RectTransform shadowRect = shadowObj.AddComponent<RectTransform>();
        shadowRect.anchorMin = rectTransform.anchorMin;
        shadowRect.anchorMax = rectTransform.anchorMax;
        shadowRect.anchoredPosition = rectTransform.anchoredPosition + shadowOffset;
        shadowRect.sizeDelta = rectTransform.sizeDelta;
        
        // Add Image component for shadow
        shadowImage = shadowObj.AddComponent<Image>();
        shadowImage.sprite = buttonImage.sprite;
        shadowImage.color = shadowColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        AnimateScale(originalScale * hoverScale, hoverDuration);
        AnimateColor(hoverColor, hoverDuration);
        AnimateShadow(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        AnimateScale(originalScale, hoverDuration);
        AnimateColor(originalColor, hoverDuration);
        AnimateShadow(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AnimateScale(originalScale * clickScale, clickDuration);
        AnimateShadowOffset(shadowOffset * 0.5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AnimateScale(isHovered ? originalScale * hoverScale : originalScale, clickDuration);
        AnimateShadowOffset(shadowOffset);
    }

    private void AnimateScale(Vector3 targetScale, float duration)
    {
        StartCoroutine(ScaleAnimation(targetScale, duration));
    }

    private void AnimateColor(Color targetColor, float duration)
    {
        StartCoroutine(ColorAnimation(targetColor, duration));
    }

    private void AnimateShadow(bool isHovered)
    {
        StartCoroutine(ShadowAnimation(isHovered ? shadowColor : new Color(shadowColor.r, shadowColor.g, shadowColor.b, 0.3f)));
    }

    private void AnimateShadowOffset(Vector2 targetOffset)
    {
        StartCoroutine(ShadowOffsetAnimation(targetOffset));
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

    private System.Collections.IEnumerator ShadowAnimation(Color targetColor)
    {
        Color startColor = shadowImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < hoverDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / hoverDuration;
            shadowImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        shadowImage.color = targetColor;
    }

    private System.Collections.IEnumerator ShadowOffsetAnimation(Vector2 targetOffset)
    {
        Vector2 startOffset = shadowImage.rectTransform.anchoredPosition - rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < clickDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / clickDuration;
            Vector2 currentOffset = Vector2.Lerp(startOffset, targetOffset, t);
            shadowImage.rectTransform.anchoredPosition = rectTransform.anchoredPosition + currentOffset;
            yield return null;
        }

        shadowImage.rectTransform.anchoredPosition = rectTransform.anchoredPosition + targetOffset;
    }
} 