using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerAnalytics : MonoBehaviour
{
    private static PlayerAnalytics instance;
    public static PlayerAnalytics Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("PlayerAnalytics");
                instance = go.AddComponent<PlayerAnalytics>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    // Структура для хранения данных о событии
    public struct AnalyticsEvent
    {
        public string eventName;
        public Dictionary<string, object> parameters;
        public DateTime timestamp;
    }

    private List<AnalyticsEvent> eventLog = new List<AnalyticsEvent>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Метод для отслеживания событий
    public void TrackEvent(string eventName, Dictionary<string, object> parameters = null)
    {
        AnalyticsEvent analyticsEvent = new AnalyticsEvent
        {
            eventName = eventName,
            parameters = parameters ?? new Dictionary<string, object>(),
            timestamp = DateTime.Now
        };

        eventLog.Add(analyticsEvent);
        Debug.Log($"Event tracked: {eventName} at {analyticsEvent.timestamp}");
        
        // Здесь можно добавить отправку данных на сервер
        SaveEventData(analyticsEvent);
    }

    // Методы для отслеживания конкретных событий
    public void TrackLevelStart(int levelId)
    {
        var parameters = new Dictionary<string, object>
        {
            { "levelId", levelId },
            { "startTime", DateTime.Now.ToString() }
        };
        TrackEvent("level_start", parameters);
    }

    public void TrackLevelComplete(int levelId, float timeSpent, int score)
    {
        var parameters = new Dictionary<string, object>
        {
            { "levelId", levelId },
            { "timeSpent", timeSpent },
            { "score", score }
        };
        TrackEvent("level_complete", parameters);
    }

    public void TrackPlayerDeath(Vector3 position, string causeOfDeath)
    {
        var parameters = new Dictionary<string, object>
        {
            { "positionX", position.x },
            { "positionY", position.y },
            { "positionZ", position.z },
            { "cause", causeOfDeath }
        };
        TrackEvent("player_death", parameters);
    }

    public void TrackItemCollected(string itemId, Vector3 position)
    {
        var parameters = new Dictionary<string, object>
        {
            { "itemId", itemId },
            { "positionX", position.x },
            { "positionY", position.y },
            { "positionZ", position.z }
        };
        TrackEvent("item_collected", parameters);
    }

    private void SaveEventData(AnalyticsEvent analyticsEvent)
    {
        // Здесь можно реализовать сохранение данных локально или отправку на сервер
        // Например, сохранение в JSON файл
        string eventJson = JsonUtility.ToJson(analyticsEvent);
        // TODO: Реализовать сохранение данных
    }

    private void OnApplicationQuit()
    {
        // Сохранение всех накопленных данных перед выходом
        Debug.Log($"Saving {eventLog.Count} analytics events before quitting");
        // TODO: Реализовать финальное сохранение данных
    }
} 