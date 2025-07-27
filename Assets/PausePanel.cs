using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] Canvas panelPause;

    static bool escPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !escPressed)
        {
            
            if (!panelPause.gameObject.activeSelf)
                PauseGame();
        }
    }

    void PauseGame()
    {
        panelPause.gameObject.SetActive(true);
        Time.timeScale = 0;
        escPressed = true;
    }

    public void ContinueGame()
    {
        escPressed = false;
        panelPause.gameObject.SetActive(false);
        Time.timeScale = 1;
        
    }
}
