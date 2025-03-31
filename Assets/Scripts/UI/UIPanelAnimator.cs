using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPanelAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float scaleDuration = 0.3f;
    [SerializeField] private float scaleAmount = 0.95f;
    
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originalScale;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowPanelAnimation());
    }

    public void HidePanel()
    {
        StartCoroutine(HidePanelAnimation());
    }

    private IEnumerator ShowPanelAnimation()
    {
        // Reset scale
        rectTransform.localScale = originalScale * scaleAmount;
        
        // Fade in
        canvasGroup.alpha = 0f;
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeDuration;
            canvasGroup.alpha = alpha;
            rectTransform.localScale = Vector3.Lerp(originalScale * scaleAmount, originalScale, alpha);
            yield return null;
        }
        
        canvasGroup.alpha = 1f;
        rectTransform.localScale = originalScale;
    }

    private IEnumerator HidePanelAnimation()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - (elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;
            rectTransform.localScale = Vector3.Lerp(originalScale, originalScale * scaleAmount, elapsedTime / scaleDuration);
            yield return null;
        }
        
        canvasGroup.alpha = 0f;
        rectTransform.localScale = originalScale * scaleAmount;
        gameObject.SetActive(false);
    }
} 