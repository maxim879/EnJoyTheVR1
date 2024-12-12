using System;
using System.IO;
using UnityEngine;
using XLua;
using System.Collections.Generic;

[System.Serializable]
public class Injection
{
    public string name;
    public GameObject value;
}

public class CustomLuaLoader : MonoBehaviour
{
    public string customModeDirectory;
    public string luaScriptName;
    public List<Injection> injections;

    private LuaEnv luaEnv;
    private LuaTable scriptEnv;

    void Start()
    {
        string scriptPath = Path.Combine(Application.persistentDataPath, "CustomModes", customModeDirectory, "Resources", luaScriptName);
        if (Application.isEditor)
        {
            Debug.Log(scriptPath);
        }

        luaEnv = new LuaEnv();
        scriptEnv = luaEnv.NewTable();

        EVR.API apiInstance = FindObjectOfType<EVR.API>();
        if (apiInstance != null)
        {
            scriptEnv.Set("EVR", apiInstance);
        }
        else
        {
            Debug.LogError("EVR.API объект не найден в сцене.");
        }

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);

        foreach (var injection in injections)
        {
            if (injection.value != null)
            {
                scriptEnv.Set(injection.name, injection.value);
            }
            else
            {
                Debug.LogWarning($"Инъекция {injection.name} пропущена, объект null.");
            }
        }

        if (File.Exists(scriptPath))
        {
            try
            {
                string luaCode = File.ReadAllText(scriptPath);
                luaEnv.DoString(luaCode, "CustomLuaLoader", scriptEnv);
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка выполнения Lua скрипта: {e.Message}\n{e.StackTrace}");
            }
        }
        else
        {
            Debug.LogError($"Lua скрипт не найден по пути: {scriptPath}");
        }

        var luaStart = scriptEnv.Get<Action>("Start");
        luaStart?.Invoke();
    }

    void Update()
    {
        if (scriptEnv != null)
        {
            var luaUpdate = scriptEnv.Get<Action>("Update");
            luaUpdate?.Invoke();
        }
    }

    void OnDestroy()
    {
        if (scriptEnv != null)
        {
            var luaOnDestroy = scriptEnv.Get<Action>("OnDestroy");
            luaOnDestroy?.Invoke();
        }

        scriptEnv.Dispose();
        luaEnv.Dispose();
    }
    public void InvokeLuaFunction(string functionName)
    {
        if (scriptEnv != null)
        {
            var luaFunction = scriptEnv.Get<Action>(functionName);
            if (luaFunction != null)
            {
                luaFunction.Invoke();
                Debug.Log($"Вызвана Lua функция: {functionName}");
            }
            else
            {
                Debug.LogError($"Функция {functionName} не найдена в Lua!");
            }
        }
    }
}
