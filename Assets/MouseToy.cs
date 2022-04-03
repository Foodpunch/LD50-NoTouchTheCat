using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class MouseToy : MonoBehaviour
{
    public GameObject[] Gibs;
    public SpriteRenderer _sr;
    float gameTime;
    CapsuleCollider2D _col;
    // Start is called before the first frame update
    void Start()
    {
        _sr = _sr.GetComponent<SpriteRenderer>();
        _col = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if(gameTime >= 5f)
        {
            Destroy(gameObject, 1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Paws"))
        {
            _sr.enabled = false;
            Explode();
            StartCoroutine(SlowTime());
        }
    }
    [Button]
    public void Explode()
    {
        _sr.enabled = false;
        _col.enabled = false;
        if (Gibs != null)
        {
            for (int i = 0; i < Gibs.Length; ++i)
            {
                Gibs[i].SetActive(true);
                Gibs[i].GetComponent<Rigidbody2D>().AddForceAtPosition(Gibs[i].transform.position - transform.position*15f, transform.position);
            }
        }
    }
    IEnumerator SlowTime()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.03f);
        Time.timeScale = 1f;
    }
}
