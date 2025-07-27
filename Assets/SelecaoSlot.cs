using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SelecaoSlot : EventTrigger
{
    public int slotIndex;

    private Image slotImage;

    public Action<int> onSelecionarSlot;

    public void Start()
    {
        // Obter a referência à imagem do slot
        slotImage = GetComponent<Image>();

        // Verificar se o campo onClick está disponível na Image
        if (slotImage.GetComponent<Button>() == null)
        {
            // Se o campo onClick não estiver disponível, adicionar o listener de clique usando EventTrigger
            EventTrigger trigger = slotImage.gameObject.AddComponent<EventTrigger>();
            trigger.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => OnClickSelecionarSlot(slotIndex));

            trigger.triggers.Add(entry);
        }
        else
        {
            // Se o campo onClick estiver disponível, adicionar o listener de clique normalmente
            slotImage.GetComponent<Button>().onClick.AddListener(() => OnClickSelecionarSlot(slotIndex));
        }
    }

    private void OnClickSelecionarSlot(int index)
    {
        if (onSelecionarSlot != null)
        {
            onSelecionarSlot(index);
        }
    }
}
