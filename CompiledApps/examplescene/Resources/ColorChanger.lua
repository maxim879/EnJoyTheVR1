local LKeycode = EVR:LKeyCode()

function Start()
    Cube:GetComponent("Renderer").material.color = CS.UnityEngine.Color.blue
end

function Update()
    if CS.UnityEngine.Input.GetKey(LKeycode) then
        Cube:GetComponent("Renderer").material.color = CS.UnityEngine.Color.red
    else
        Cube:GetComponent("Renderer").material.color = CS.UnityEngine.Color.blue
    end
end