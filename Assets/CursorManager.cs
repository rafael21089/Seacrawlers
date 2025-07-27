using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorImageBase;
    public Texture2D cursorImageMoving;
    public Texture2D cursorImageAttack;
    public Texture2D cursorImageTalking;
    public Camera cam;

    bool isAttacking = false;
    bool isMoving = false;
    float timeSinceLastAction = 0f;
    public float timeToChangeCursorBack = 2f;

    void Start()
    {
        Cursor.SetCursor(cursorImageBase, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        timeSinceLastAction += Time.deltaTime;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Enemy")
        {
            Cursor.SetCursor(cursorImageAttack, Vector2.zero, CursorMode.Auto);
            isAttacking = true;
        }else if (Physics.Raycast(ray, out hit) && hit.collider.tag == "questGiver")
        {
            Cursor.SetCursor(cursorImageTalking, Vector2.zero, CursorMode.Auto);
            isAttacking = true;
        }
        else if (!isMoving)
        {
            Cursor.SetCursor(cursorImageBase, Vector2.zero, CursorMode.Auto);
            isAttacking = false;
        }

        if ((Input.GetMouseButtonDown(1) || Input.GetMouseButton(1)) && isAttacking == false)
        {
            Cursor.SetCursor(cursorImageMoving, Vector2.zero, CursorMode.Auto);
            isMoving = true;
            timeSinceLastAction = 0f;
        }

        if (isMoving && timeSinceLastAction >= timeToChangeCursorBack)
        {

            Cursor.SetCursor(cursorImageBase, Vector2.zero, CursorMode.Auto);
            isMoving = false;
        }
    }
}
