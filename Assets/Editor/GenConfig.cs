using System;
using System.Collections.Generic;
using UnityEngine;

public static class GenConfig
{
    [XLua.LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>()
    {
        //typeof(EVR.API), // Регистрируем класс API для вызова в Lua
        typeof(GameObject), // Регистрируем GameObject
        typeof(Component), // Регистрируем Component
        typeof(Transform), // Регистрируем Transform
        // Добавляйте другие классы по необходимости
    };

    [XLua.CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>()
    {
        // Здесь указываются интерфейсы или делегаты, которые C# вызывает в Lua
    };

    [XLua.BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()
    {
        // Добавляйте методы или свойства, которые не нужно открывать для Lua
        new List<string>() {"UnityEngine.WWW", "movie"},
    };
}
