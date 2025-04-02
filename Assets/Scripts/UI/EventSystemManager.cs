using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoBehaviour
{
    private static EventSystemManager instance;
    public static EventSystemManager Instance => instance;

    private EventSystem eventSystem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            eventSystem = GetComponent<EventSystem>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (eventSystem != null)
        {
            eventSystem.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (eventSystem != null)
        {
            eventSystem.enabled = false;
        }
    }
} 