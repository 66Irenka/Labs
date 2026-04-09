# Lab3
Лабораторна робота 

## Структура
- EncryptApp — шифрування
- DecryptApp — розшифрування

## Файли
- Pavlenko1.txt — вхідний текст
- Pavlenko2.txt — пароль
- Pavlenko3.txt — зашифрований текст
- Pavlenko4.txt — розшифрований текст

## Запуск EncryptApp
```bash
cd Lab3/EncryptApp
dotnet build
dotnet run
```
## 🧪 Кроки:
Обрати файл тексту — Pavlenko1.txt
Обрати файл пароля — Pavlenko2.txt
Обрати файл результату — Pavlenko3.txt
Натиснути "Зашифрувати"

## 🔓 Запуск DecryptApp
```bash
cd Lab3/DecryptApp
dotnet build
dotnet run
```
## 🧪 Кроки:
Обрати файл пароля — Pavlenko2.txt
Обрати файл шифру — Pavlenko3.txt
Обрати файл результату — Pavlenko4.txt
Натиснути "Розшифрувати"

## ✅ Результат
Після виконання програми файл Pavlenko4.txt
повинен повністю співпадати з Pavlenko1.txt.

## 🧠 Особливості
Використано таблицю символів (SymbolTable)
Реалізовано перевірку вхідних даних

## Підтримується:
- український алфавіт
- пробіли
- розділові знаки
- перенос рядка