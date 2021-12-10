# bsuir-patreon
Проект Виталия Пресного и Никиты Яскевича на технологиях C# + React

[Официальная документация по схеме работы C# + React](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/react?view=aspnetcore-5.0&tabs=visual-studio)

## Общие команды

1. Как создать проект:
```bash
dotnet new reactredux
```

2. Install npm packages
```bash
cd ClientApp
npm install --save <package_name>
```

3. Как запустить фронт:
```bash
cd ClientApp
npm install (если локальные зависимости отличаются от удаленных)
npm start
```
[Официальная документация](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/react?view=aspnetcore-5.0&tabs=visual-studio#run-the-cra-server-independently)

## Схема работы с проектом

1. Комиты в мастер запрещены
2. Для добавления любой новой функциональности необходимо сделать следующие шаги:
   - Подтянуть последнюю версию мастера
   - Сделать новую ветку от мастера с названием, отражающим суть изменений
   - Провести необходимые изменения/добавления на созданной ветке
   - Закомить изменения в новую ветку
   - Создать PullRequest из новой ветки в Master.
   - Ожидать Review, а затем мерджа
   - После мерджа, новая ветка удаляется
3. Вот так вот
3. После мерджа, новая ветка удаляется
