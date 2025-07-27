using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public static int slotChoosed = 3;
    public void DropDown(int index)
    {
        switch (index)
        {
            case 0:
                slotChoosed = 0;
                this.gameObject.SetActive(false);
                break;
            case 1:
                slotChoosed = 1;
                this.gameObject.SetActive(false);
                break;
            case 2:
                slotChoosed = 2;
                this.gameObject.SetActive(false);
                break;
            default:
                slotChoosed = 3;
                break;
        }
    }

}
