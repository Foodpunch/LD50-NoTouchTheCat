using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CatController : MonoBehaviour
{
    public static CatController Instance;
    public Animator[] CatHands;

    public GameObject SwipeEffect;
    public float swipeRate =.1f;     //default is 1 second?
    float nextTimeToSwipe;

    float catTime;

    public Transform RightHand;
    public Transform LeftHand;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && !GameManager.Instance.isGameOver)
        {
            catTime += Time.deltaTime;
            if (Time.time >= nextTimeToSwipe)
            {
                SwipeHands();
                nextTimeToSwipe = Time.time + (1 / swipeRate);
            }
        }
    }
    public void SwipeHands()
    {
        
        if(CatHands!=null)
        {
            for (int i = 0; i < CatHands.Length; ++i)
            {
                CatHands[i].SetTrigger("Swipe");
            }
            AudioManager.Instance.PlayCachedSound(AudioManager.Instance.SwipeSounds, transform.position, .3f,true);
            // Instantiate(SwipeEffect, RightHand.position, RightHand.rotation);
            //Instantiate(SwipeEffect, LeftHand.position, LeftHand.rotation);
        }
        
    }
}
