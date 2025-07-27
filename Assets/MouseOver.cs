using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /* public GameObject hoverImage;
     public GameObject hoverImage2;
     public Image img;*/
    TextMeshProUGUI txt;
    Color color; 
    private void Start()
    {
        /* hoverImage.gameObject.SetActive(false);
         hoverImage2.gameObject.SetActive(false);
         SetImageAlphaToZero();*/

        txt = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*hoverImage.gameObject.SetActive(true);
        hoverImage2.gameObject.SetActive(true);
        SetImageAlphaToFifty();*/
        txt.color = new Color(226f / 255f, 170f / 255f, 12f / 255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        /* hoverImage.gameObject.SetActive(false);
         hoverImage2.gameObject.SetActive(false);
         SetImageAlphaToZero();*/
        txt.color = Color.black;
    }

    public void SetImageAlphaToZero()
    {
        /*if (img != null)
        {
            Color imageColor = img.color;
            imageColor.a = 0;
            img.color = imageColor;
        }*/
    }

    public void SetImageAlphaToFifty()
    {
        /*if (img != null)
        {
            Color imageColor = img.color;
            imageColor.a = 0.2f;
            img.color = imageColor;
        }*/
    }
}
