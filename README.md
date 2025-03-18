# PlayerAnalytics

1) Синглтон паттерн: Система реализована как синглтон, что обеспечивает единую точку доступа к аналитике из любого места в игре.
   
2)Структура данных:
AnalyticsEvent - структура для хранения информации о событиях
eventLog - список для хранения всех событий

3) Основные методы отслеживания:
TrackLevelStart - начало уровня
TrackLevelComplete - завершение уровня
TrackPlayerDeath - смерть игрока
TrackItemCollected - сбор предметов

4) Использование системы:
  // Пример использования в других скриптах
  PlayerAnalytics.Instance.TrackLevelStart(1);
  PlayerAnalytics.Instance.TrackPlayerDeath(player.position, "fall_damage");
  PlayerAnalytics.Instance.TrackItemCollected("coin", transform.position);

5) Сохранение данных:
Система подготовлена для сохранения данных
Можно расширить метод SaveEventData для отправки данных на сервер или сохранения локально

Для использования этой системы:
Создайте папку Scripts/Analytics в вашем проекте
Поместите туда созданный файл PlayerAnalytics.cs
Добавьте компонент на пустой GameObject в вашей начальной сцене
Система автоматически сохранится между сценами благодаря DontDestroyOnLoad.
