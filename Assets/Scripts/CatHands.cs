using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHands : MonoBehaviour
{
    Vector3 mouseInput;
    Vector3 mouseDir;

    public GameObject[] SecondaryObject;
    public float lookDelay;
    public enum TRANSFORM
    {
        UP,
        RIGHT,
        LEFT,
        DOWN
    }
    public TRANSFORM LookDirection = TRANSFORM.UP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isGameOver)
        {
            SetDirectionToMouse();
            if (SecondaryObject != null)
            {
                for (int i = 0; i < SecondaryObject.Length; ++i)
                {
                    //SecondaryObject[i].transform.rotation = gameObject.transform.rotation;        //causes some weird glitches when pointing down
                    SecondaryObject[i].transform.rotation = Quaternion.Slerp(SecondaryObject[i].transform.rotation, gameObject.transform.rotation, Time.deltaTime * lookDelay);
                }
            }
        }
       
    }

    void SetDirectionToMouse()
    {
        mouseInput = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mouseInput);
        mousePosWorld.z = 0;
        mouseDir = mousePosWorld - transform.position;
        switch(LookDirection)
        {
            case TRANSFORM.RIGHT:
                transform.right = mouseDir;
                break;
            case TRANSFORM.UP:
                transform.up = mouseDir;
                break;
            case TRANSFORM.LEFT:
                transform.right = -mouseDir;
                break;
            case TRANSFORM.DOWN:
                transform.up = -mouseDir;
                break;

        }

    }
}
