using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject HandPrefab;
    public GameObject[] itemPrefabs;
    public Transform[] HandSpawnPoints;
    //public Transform[] MouseSpawnPoints;

    public GameObject[] SpeechBubbles;
    public static GameManager Instance;


    float handSpawnRate = 0.25f;
    float nextTimeToSpawnHands =5f;

    float mouseSpawnRate = 0.25f;
    float nextTimeToSpawnMouse =5f;

    public CanvasGroup FadeToWhite;
    [SerializeField]
    Image EndImage;
    public bool isGameOver;

    public TextMeshProUGUI SmackCounter;
    public TextMeshProUGUI ScoreCounter;
    public int smackCount = 0;
    public GameObject MenuButton;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
            if (Time.time > nextTimeToSpawnHands)
            {
                SpawnHands();
                nextTimeToSpawnHands = Time.time + (1 / handSpawnRate);
                if (PercentChance(25))
                {
                    AudioManager.Instance.PlayCachedSound(AudioManager.Instance.CatSounds, transform.position, .2f, false);
                }
            }
            if (Time.time > nextTimeToSpawnMouse)
            {
                SpawnMouse();
                nextTimeToSpawnMouse = Time.time + (1 / Random.Range(0.1f, 0.25f));

            }
            SmackCounter.SetText("SMACK COUNT : " + smackCount.ToString());
        }
        if(isGameOver)
        {
            ScoreCounter.SetText("SCORE\n" + smackCount.ToString());
            
            SmackCounter.gameObject.SetActive(false);
            FadeToWhite.alpha += Time.deltaTime/4.5f;
            if(FadeToWhite.alpha >=1)
            {
                EndImage.gameObject.SetActive(true);
                ScoreCounter.gameObject.SetActive(true);
                MenuButton.SetActive(true);
            }
        }
    
       
    }

    void SpawnHands()
    {
        GameObject ItemClone = Instantiate(HandPrefab,GetRandomSpawnPoint().position,HandPrefab.transform.rotation);
        if(PercentChance(10))
        {
            ItemClone.GetComponent<HumanHand>().SetMitten();
            ItemClone.name = "Mitts";
        }
        if(PercentChance(20))
        {
            handSpawnRate += 0.05f;
        }
    }
    [Button]
    public void SpawnMouse()
    {
        GameObject ItemClone = Instantiate(itemPrefabs[Random.Range(0,itemPrefabs.Length)], GetRandomSpawnPoint().position, Quaternion.identity);
        Vector2 forceDir = CatController.Instance.transform.position - ItemClone.transform.position;
        ItemClone.GetComponent<Rigidbody2D>().AddForce(forceDir.normalized * 10f,ForceMode2D.Impulse);
    }

    Transform GetRandomSpawnPoint()
    {
        return HandSpawnPoints[Random.Range(0, HandSpawnPoints.Length)];
    }

    bool PercentChance(int chance)
    {
        int rand = Random.Range(0, 101);
        return rand < chance;
    }
    public void SpawnText(Vector3 pos)
    {
        float angle = -50f;
        Vector3 randAngle = Vector3.zero;
        if(PercentChance(50))
        {
            randAngle.z = angle;
        }
        GameObject textClone = Instantiate(SpeechBubbles[Random.Range(0, SpeechBubbles.Length)], pos, Quaternion.identity);
        textClone.transform.rotation = Quaternion.Euler(randAngle);
        Destroy(textClone, 0.5f);
    }
    public void BackToMenu()
    {
        StartCoroutine(LoadSceneAsync());        
    }
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation Menu = SceneManager.LoadSceneAsync(0);
        while (!Menu.isDone)
        {
            yield return null;
        }
    }
}
