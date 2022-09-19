# ProductivityInsideTestTask
Top-down view 3D pacman-stylized game, where you must defeat all enemies!

Technical task (in russian):
Примечания к тестовому заданию: 
• Необязательно выполнять абсолютно все пункты, разрабатывается столько сколько возможно. 
• Использование код стайла будет плюсом. 
Список задач: 
• Подготовить Unity сцену где будет располагаться лабиринт, состоящий из примитивов Unity (сube / plane).
• Реализовать случайное перестроение лабиринта в редакторе перед runtime (необязательный пункт, но будет плюсом) 
• Настроить играбельного персонажа (на выбор - FPS / TPS / Вид сверху). 
• Управление персонажем опционально либо клавиатура и мышь, либо джойстики на Canvas. (Будет плюсом реализация обоих пунктов с переключением в редакторе) 
• Реализовать масштабируемую дату персонажа (здоровье, атака, скорость передвижения). При этом список конфигураций персонажа можно было бы легко менять, удалять и добавлять новые. 
• Если у игрока кончилось здоровье - появляется экран Game Over. 
• Если у врага кончилось здоровье - он удаляется из Unity сцены или заносится в пул (опционально) 
• Реализовать врагов, которые бы ходили по локации, используя Nav Mesh модуль. Враги должны находить игрока исходя из дистанции до него и следовать за ним, если дистанция меньше указанной, к примеру, 5 юниметров. 
• Если враг подошел к игроку и зацепил его, у игрока убавляется количество жизней. Далее враг отходит от персонажа и снова подходит к нему на критическое расстояние, чтобы нанести повторный урон. Цикл происходит до тех пор, пока либо игрок не уйдет на безопасную дистанцию, либо не погибнет. 
• Реализовать масштабируемую структуру врагов. Создать базовый класс врага и на его основе реализовать несколько других, добавив им уникальные характеристики. 
• Реализовать подбираемый бонус, подбирая который игрок становится охотником, и враги убегают по nav mesh от него, при столкновении с ним получают урон. 
• Создать несколько UI окон (Start, Gameplay, Pause, End) в Canvas и сделать переключатель состояний между ними. В случае, если фокус приложения утерян - вызывать паузу и останавливать происходящее на экране. 
Дополнительные пункты (по желанию / возможности): 
• При разработке UI Manager не использовать переключение окон в инспекторе по SetActive(true/false) напрямую и не использовать StateMachine паттерн. 
• Попробовать реализовать в проекте паттерны (декоратор, абстрактная фабрика, слушатель) на выбор. 
• Не использовать Update/FixedUpdate нигде кроме обработки физики и Input контроллера. 
Результат работ: 
• Отправить HR ссылку на исходный код проекта в github / gitlab / bitbucket (на выбор) 
• Срок разработки до 3 дней.
