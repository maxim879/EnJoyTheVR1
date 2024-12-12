[![Typing SVG](https://readme-typing-svg.herokuapp.com?font=Oswald&weight=700&size=25&duration=1500&pause=1&color=F70000&background=100BFF00&multiline=true&repeat=false&width=435&height=80&lines=EnJoyTheVR;%D0%A8%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD+%D0%BF%D1%80%D0%B8%D0%BB%D0%BE%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F)](https://git.io/typing-svg)

---

## Как создать своё приложение для EnJoyTheVR

1. **Загрузите проект Unity.**

2. **Создайте новую сцену.** Если это первоначальная сцена, назовите её тем же именем, что и ваша игра. *(Можно использовать только строчные буквы)*.

3. **Заполните название бандла в инспекторе.** Это и есть название вашей игры. *(В дальнейшем во всех сценах необходимо указывать данный бандл.)*

   ![AssetBundle](https://github.com/user-attachments/assets/3a467aa2-a898-4c05-b24b-b7e9dc85ab12)

4. **Создайте пустой объект, назовите его “spawn”.** На его месте будет появляться игрок.

---

## Как писать скрипты?

Создание игры для EnJoyTheVR схоже с созданием обычной игры, однако в связи с ограничением бандлов Unity скрипты должны быть написаны на Lua.

### Использование CustomLuaLoader

Чтобы использовать Lua-скрипты, необходимо воспользоваться `CustomLuaLoader.cs`. Добавьте этот скрипт на нужный игровой объект и заполните поля:

- **CustomModeDirectory** — название вашей игры
- **LuaScriptName** — название скрипта, который будет использоваться
- **Injections** — объекты, которые необходимо передать в скрипт для дальнейшего использования

   ![CustomLuaLoader](https://github.com/user-attachments/assets/5c0f25b7-eebc-4c83-b907-6d89a4d29edd)

   - **Name** — название объекта, которое будет использоваться в вашем скрипте
   - **Value** — объект, который необходимо передать

Lua-скрипты размещаются по следующему пути: `Application.persistentDataPath, "CustomModes", customModeDirectory, "Resources", luaScriptName`. *(Данный путь будет выведен в Debug log при старте.)*

Чтобы вызвать функцию извне, активируйте метод `CustomLuaLoader.InvokeLuaFunction` и впишите название функции:

![InvokeFunction](https://github.com/user-attachments/assets/38d1bcf6-5a2d-4a48-a9a6-9edea968f751)

---

## Как «собрать» игру?

1. Нажмите на кнопку **Build AssetBundles**:

   ![BuildAssetBundles](https://github.com/user-attachments/assets/539cf9e4-2b59-4e57-9f1d-a64bd3af27e6)

---

## Формат игры

Игра должна быть в следующем формате:

- Папка с названием игры
  - Файлы бандла
  - Обложка в формате `logo.png`
  - Папка `Resources`
    - Скрипты Lua

![GameFormat](https://github.com/user-attachments/assets/2f58c45f-68f5-4fc6-be04-ac58d0542865)

---

## Тестирование на устройствах

### Android
Переместите игру по следующему пути:
`Android/data/com.Zhes.EnJoyTheVR/files/CustomModes/`

### iOS
Переместите игру по следующему пути:
`На iPhone/EnJoyTheVR/CustomModes/`

---

## EnJoyTheVR API: EVR.API

Для использования API необходимо поместить префаб с названием **EVR.API** на первую сцену вашего приложения.

![Prefab1](https://github.com/user-attachments/assets/ee0e6c3a-678a-4d28-b80b-565fdeced2fb)
![Prefab2](https://github.com/user-attachments/assets/6c91332b-34d9-4226-a16e-683ac758dd3c)

### Описание API

#### EVR.Player
Возвращает GameObject игрока.

**Пример:**
```lua
local player = EVR.Player
function Start()
    player:GetComponent("Transform").position = Vector3(0, 0, 0)
end
```

#### EVR:Save(string, value)
Сохраняет значение в системе PlayerPrefs по указанному ключу.

**Пример:**
```lua
function Start()
    EVR:Save("Name", "John")
    EVR:Save("Score", 100)
    EVR:Save("IsAlive", true)
end
```

#### EVR:Load(string, string)
Загружает сохранённое значение из PlayerPrefs по указанному ключу.

**Пример:**
```lua
local playerName = EVR:Load("Name", "string")
local score = EVR:Load("Score", "int")
local isAlive = EVR:Load("IsAlive", "bool")
```

#### EVR:SetTime(float)
Устанавливает масштаб времени в игре, изменяя скорость его течения.

**Пример:**
```lua
function Start()
    EVR:SetTime(0.2)
end
```

#### EVR:CloseApp()
Закрывает приложение, возвращая игрока в главное лобби.

**Пример:**
```lua
function Update()
    if CS.UnityEngine.Input.GetKey(CS.UnityEngine.KeyCode.Return) then
        EVR:CloseApp()
        CS.UnityEngine.Debug.Log("AppWasClosed")
    end
end
```

#### EVR:LoadScene(string, bool)
Загружает сцену из AssetBundle. Аргументы:
- `string` — название сцены.
- `bool` — оставить ли предыдущую сцену загруженной.

**Пример:**
```lua
function Start()
    EVR:LoadScene("TestScene", true)
    CS.UnityEngine.Debug.Log("TestScene Loaded")
end
```

#### EVR:SetNewHandL(GameObject)
Заменяет модель левой руки. Если объект не передан, отключает модель руки.

**Пример:**
```lua
local object = someGameObject
function Start()
    EVR:SetNewHandL(object)
end
```

#### EVR:SetNewHandR(GameObject)
Заменяет модель правой руки. Если объект не передан, отключает модель руки.

**Пример:**
```lua
local object = someGameObject
function Start()
    EVR:SetNewHandR(object)
end
```

---


