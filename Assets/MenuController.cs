using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuality()
    {
        SetScreenRes();
    }

    void SetScreenRes()
    {
        string index = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch(index)
        {
            case "1920x1080":
                Screen.SetResolution(1920, 1080, true);
                break;
            case "1280x1024":
                Screen.SetResolution(1280, 1024, true);
                break;

        }
    }
}
