using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HumanHand : MonoBehaviour
{
    public bool hasMitten;
    Transform playerTransform;

    public float handSpeed = 2f;
    Vector3 handDir;
    Rigidbody2D _rb;

    bool isScratched = false;
    public Sprite scratchedHand;
    public Sprite brokenMitts;

    public SpriteRenderer handSr;
    public SpriteRenderer mittsSr;
    public BoxCollider2D _col;
    public GameObject[] Fingers;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = CatController.Instance.gameObject.transform;
        _rb = GetComponent<Rigidbody2D>();
        handSr = handSr.GetComponent<SpriteRenderer>();
        mittsSr = mittsSr.GetComponent<SpriteRenderer>();
        _col = _col.GetComponent<BoxCollider2D>();

       
    }

    // Update is called once per frame
    void Update()
    {
        handDir = playerTransform.position - transform.position;
        transform.up = -handDir;
        if (hasMitten) handSpeed = .5f;
        if(!isScratched)
        {
            _rb.velocity = -transform.up * handSpeed;

        }
        else if(isScratched)
        {
            _rb.velocity = transform.up * 4f;
            if (Vector2.Distance(playerTransform.position, transform.position) > 15)
            {
                gameObject.SetActive(false);
                Destroy(gameObject, 1f);
            }
        }
           


        FlipX();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gameover!");
        }
        if(collision.gameObject.CompareTag("Paws"))
        {
            if(hasMitten)
            {
                mittsSr.sprite = brokenMitts;
                StartCoroutine(Delay());
            }
            if(!hasMitten)
            {
                mittsSr.gameObject.SetActive(false);
                isScratched = true;
                handSr.sprite = scratchedHand;
                DisableFingers();
                _col.enabled = false;
            }
        }

        //gameObject.SetActive(false);
    }
    void FlipX()
    {
        float angle = transform.rotation.eulerAngles.z;
        if (angle > 0f && angle < 180f) transform.localScale = new Vector3(-1, 1, 1);
    }

    void DisableFingers()
    {
        if(Fingers!=null)
        {
            for(int i =0; i< Fingers.Length; ++i)
            {
                Fingers[i].SetActive(false);
            }
        }
    }
    public void SetMitten()
    {
        hasMitten = true;
        DisableFingers();
        mittsSr.gameObject.SetActive(true);
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        hasMitten = false;
    }
}
