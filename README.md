# Лабораторная работа №11 по ТРП

Вариант Б. Сетевое взаимодействие приложения ASP.NET Core и PostgreSQL 16.
Основой проекта служит MVC-приложение «Калькулятор дней до события» из ЛР9.

## Проверка в GitHub Actions

Workflow `.github/workflows/docker-network-test.yml` запускается при отправке
изменений в `lab11`, `main` или `master`. В [GitHub Actions](https://github.com/Shelbyyy777/trp-lr9/actions?query=branch%3Alab11)
откройте **Docker Network Test** и раскройте шаг проверки `GET /items`.

- Вариант Б создаёт сеть `app-network`, собирает образ и запускает два контейнера командами Docker.
- Дополнительное задание запускает те же сервисы через Docker Compose.

Оба задания проверяют, что `GET /items` возвращает записи `Notebook` и `Pen`
из таблицы `Items`. Таблица и данные создаются из `database/init.sql` при
первом запуске PostgreSQL. Приложение получает строку подключения из
`DB_CONNECTION` и обращается к базе по имени `db`.

## Запуск через Docker Compose

В каталоге с `docker-compose.yml` выполните:

```sh
docker compose up --build -d
```

Откройте <http://localhost:8080/items>. Ожидаемый ответ:

```json
[{"id":1,"name":"Notebook"},{"id":2,"name":"Pen"}]
```

Для остановки выполните `docker compose down`.

## Публикация из исходников

Для сборки выполните `dotnet publish -c Release -o ./publish`.
