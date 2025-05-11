using UnityEngine;

public class LuaTriggerInvoker : MonoBehaviour
{
    [Header("Использовать голову игрока, как targetCollider")]
    public bool UseHead = false;

    [Header("Целевой коллайдер")]
    public Collider targetCollider;

    [Header("Lua интеграция")]
    public CustomLuaLoader luaLoader;

    [Header("Вызываемые события Lua")]
    public bool callOnEnter = true;
    public string onEnterFunction = "OnTriggerEnter";

    public bool callOnExit = false;
    public string onExitFunction = "OnTriggerExit";

    public bool callOnStay = false;
    public string onStayFunction = "OnTriggerStay";

    private Collider triggerCollider;

    void Start()
    {
        triggerCollider = GetComponent<Collider>();
        if (triggerCollider == null || !triggerCollider.isTrigger)
        {
            Debug.LogError("На объекте должен быть коллайдер с включённым Is Trigger.");
        }

        if (UseHead)
        {
            var head = GameObject.Find("HeadCollision");
            if (head != null)
            {
                targetCollider = head.GetComponent<Collider>();
            }
            else
            {
                Debug.LogError("HeadCollision не найден.");
            }
        }
    }

    void Update()
    {
        if (UseHead && targetCollider == null)
        {
            var head = GameObject.Find("HeadCollision");
            if (head != null)
            {
                targetCollider = head.GetComponent<Collider>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled || other != targetCollider || luaLoader == null) return;

        if (callOnEnter)
        {
            luaLoader.InvokeLuaFunction(onEnterFunction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!enabled || other != targetCollider || luaLoader == null) return;

        if (callOnExit)
        {
            luaLoader.InvokeLuaFunction(onExitFunction);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!enabled || other != targetCollider || luaLoader == null) return;

        if (callOnStay)
        {
            luaLoader.InvokeLuaFunction(onStayFunction);
        }
    }
}
