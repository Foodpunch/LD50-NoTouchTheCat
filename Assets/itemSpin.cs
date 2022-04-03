using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]


public class itemSpin : MonoBehaviour
{

    public bool isAntiClockwise = false;
    Rigidbody2D _rb;
    public SpriteRenderer _sr;
    public float torque = 1f;
    float alpha = 1f;
    float scale = 1f;

    public bool isFading;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = _sr.GetComponent<SpriteRenderer>();

        if (!isAntiClockwise) _rb.AddTorque(-torque, ForceMode2D.Impulse);
        else if (isAntiClockwise) _rb.AddTorque(torque, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFading)
        {
            alpha -= Time.deltaTime / 1.8f;
            _sr.color = new Color(1, 1, 1, alpha);
            scale += Time.deltaTime/2f;
            transform.localScale = new Vector3(scale,scale,1);
            if (alpha <= 0) gameObject.SetActive(false);
        }
    }
}
