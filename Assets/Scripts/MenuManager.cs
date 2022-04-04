using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public CanvasGroup CreditsPanel;
    bool isShowingCredits;
    public AudioSource MeowSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isShowingCredits)
        {
            //CreditsPanel.alpha += Time.deltaTime;
            CreditsPanel.alpha = 1;
            if(CreditsPanel.alpha >= 1)
            {
                CreditsPanel.interactable = true;
                CreditsPanel.blocksRaycasts = true;
            }
        }    
        else if(!isShowingCredits)
        {
            //CreditsPanel.alpha -= Time.deltaTime;
            CreditsPanel.alpha = 0;
            if (CreditsPanel.alpha <=0)
            {
                CreditsPanel.interactable = false;
                CreditsPanel.blocksRaycasts = false;
            }
        }
    }
    public void StartGame()
    {
        MeowSound.Play();
        StartCoroutine(DelayedStart());
    }
    public void Credits()
    {
        isShowingCredits = true;
        
    }
    public void Back()
    {
        isShowingCredits = false;
    }
    IEnumerator DelayedStart()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        AsyncOperation Game = SceneManager.LoadSceneAsync(1);
        while (!Game.isDone)
        {
            yield return null;
        }
    }
}
