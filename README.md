Рекламные площадки

Для локального запуска проекта в системе(windows) требуется наличие Docker или dotnet.
Порядок действий:
1. Скачать в любое место данный репозиторий. Можно при помощи команды:
	- git clone https://github.com/RyuuKun-00/advertising-platforms.git
2. После загрузки в папке ./advertising-platfarms запустить исполняемый файл DockerDeploy.bat для поднятия проекта в Docker(Docker должен быть запущен) или запустить Start.bat он запустит проект локально.
3. После поднятия контейнеров откроется ссылка в браузере с проектом по адресу: http://localhost:5000

У проекта есть старновые настройки, влияющие на его работу:
- Параметры валидации файла, указывать строго тип и контент файла.
FileValidationParameters:

   AllowedExtensions: допустимые расширения файла, по умолчанию [ ".txt" ],
   
   AllowedMimeTypes: допустимый контент файла, по умолчанию [ "text/plain" ],
   
   MaxSize: максимальный размер файла, по умолчанию 104857600 байт.


- Параметры валидации данных рекламных площадок.
AdvertisingPlatformValidationParameters:

   AllowingTheUseOfCapitalLetters: разрешение на использование символов верхнего регистра.
   Если параметр AllowingTheUseOfCapitalLetters = true и CapitaLetterSensitivity = false,
   то следующие локации одинаковые: /ru /Ru /rU /RU
   Если параметр AllowingTheUseOfCapitalLetters = false и CapitaLetterSensitivity = false
   то файл с локациями в верхнем регистром не валиден: /Ru /rU /RU -> Ошибки
   Значение по умолчанию: false

   CapitaLetterSensitivity: параметр чувствительности к символам верхнего регистра.
   Если параметр CapitaLetterSensitivity = true, то следующие локации разные: /ru /Ru /rU /RU
  Значение по умолчанию: false

  LocationsWithTheSameName: разрешение на использование локаций с одинаковыми названиями.
  Если параметр LocationsWithTheSameName = true,
  то следующая локация типа: /ru/ru - валидна.
  Значение по умолчанию: false
    
