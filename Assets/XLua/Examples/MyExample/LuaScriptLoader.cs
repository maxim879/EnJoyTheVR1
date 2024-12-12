using UnityEngine;
using XLua;

public class LuaScriptLoader : MonoBehaviour
{
    public TextAsset luaScript; // Поле для Lua-скрипта, назначаемого в инспекторе
    private LuaEnv luaEnv;      // Среда выполнения Lua

    void Start()
    {
        // Инициализируем Lua среду
        luaEnv = new LuaEnv();

        // Передаем текущий объект в Lua под именем "self"
        luaEnv.Global.Set("self", this);

        // Проверяем, что luaScript не пустой и выполняем его содержимое
        if (luaScript != null)
        {
            luaEnv.DoString(luaScript.text);
        }
        else
        {
            Debug.LogError("Lua script is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // Вызываем Update функцию Lua каждый кадр, если она определена
        luaEnv?.DoString("if Update then Update() end");
    }

    void OnDestroy()
    {
        // Освобождаем Lua среду, когда объект уничтожается
        luaEnv.Dispose();
    }
}
