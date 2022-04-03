using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType
    {
        HAND,
        MISC
    }
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = CatController.Instance.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
