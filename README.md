# 🎵 MusicStore API
**MusicStore API** - это RESTful сервис для управления каталогом музыкальных альбомов.
Проект разработан как демонстрация использования NoSQL базы данных MongoDB в среде ASP.NET Core.

Позволяет выполнить полный цикл CRUD-операций над коллекцией альбомов.

---

## 🛠 Технологический стек

* **Фреймворк**: ASP.NET Core 8.0
* **База данных**: MongoDB
* **ORM/Драйвер**: MongoDB.Driver
* **Документация**: Swagger

---

## ⚙️ Начало работы

Для запуска проекта на локальной машине требуется:

* [.NET SDK 8.0](https://dotnet.microsoft.com/download)
* [MongoDB](https://www.mongodb.com/try/download/community) (установленная локально)
* Любой IDE (Visual Studio, VS Code, Rider)

### Установка и запуск

1. **Клонируйте данный репозиторий:**
	
	```bash
    git clone [https://github.com/Lungenberg/asp-core-store-web-api.git](https://github.com/Lungenberg/asp-core-store-web-api.git)
    cd music-store-api
    ```

2. **Настройка для базы данных:**

    Откройте файл 'appsettings.json' и настройте строку подключения к MongoDB.

    *Пример для локальной MongoDB:*
    ```json
        {
          "MusicStoreDatabase": {
            "ConnectionString": "mongodb://localhost:27017",
            "DatabaseName": "MusicStore",
            "AlbumsCollectionName": "categories"
          },
          "Logging": {
            "LogLevel": {
              "Default": "Information",
              "Microsoft.AspNetCore": "Warning"
            }
          },
          "AllowedHosts": "*"
        }
    ```

3. **Проверка работы:**
    После запуска перейдите по адресу `https://localhost:7001/swagger` (порт может отличаться), чтобы увидеть интерфейс Swagger UI и протестировать эндпоинты.

    ---

### Пример JSON объекта (Album)

```json
{
    "Id": "6921eb8ad779a82d925eff3a",
    "albumName": "Absolution",
    "price": 450,
    "genre": "Indie Rock",
    "author": "Muse"
}
