function OnTriggerEnter()
    OnEnterBox:SetActive(true)
end

function OnTriggerStay()
    OnEnterBox:SetActive(false)
    OnStayBox:SetActive(true)
end

function OnTriggerExit()
    OnStayBox:SetActive(false)
end