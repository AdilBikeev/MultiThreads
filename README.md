# Тестовое задание.
Разработать программу-сервис обрабатывающую документы в TXT формате.

Программе в качестве параметров передаются:
1.	Путь к папке с исходными документами в формате TXT
2.	Путь к папке с результатами

Программа следит за появлением файлов в папке с исходными документами. Программа должна использовать 4 рабочих потока для обработки документов. Суть обработки сводится к подсчету количества букв в документе. Для каждого файла из папки с исходными документами программа должна создать  в папке с результатами одноименный текстовый файл с расширением txt, в котором должно быть записано посчитанное число.

Исходные документы копируются в папку для обработки внешними средствами уже во время работы программы. 

Программа должна завершаться только по запросу пользователя. При этом если в момент получения события о завершении в рабочих потоках идет обработка документов, то надо подождать завершения обработки этих документов и завершить работу. Обработку оставшихся документов не начинать.

# Запуск

1. Открываем консоль в папке с файлом *.csproj
2. Запускаем команду ```dotnet run {pathDocs} {pathToSave}```

* PathDocs - Путь к папке с исходными документами в формате TXT
* PathDocs - Путь к папке с результатами

# Примечание
1. Обновления самих документов, которые уже были обработаны текущим сеансом программы. При перезаупске - данные обновятся. Сделано с предположением, что программа запускается 1 раз и документы в папке pathDocs появляются 1 раз
2. Регулировка кол-во одновременно потоков для обработки - вместо 4-ех выделенных Thread использовал ThreadPool с ограничением 4 потока. Регулируется в ```Settings.cs```
