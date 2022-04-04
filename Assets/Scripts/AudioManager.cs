using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioSource> ListofAudioSources = new List<AudioSource>();

    [SerializeField]
    AudioSource source;     //base source for sound fx

    public AudioSource BGMSource;
    public AudioSource GameOverSource;


    float fadeTime = 0f;

    public AudioClip[] OuchSounds;
    public AudioClip[] BGMSongs;
    public AudioClip[] SmackSounds;
    public AudioClip[] CatSounds;
    public AudioClip[] SwipeSounds;
    public AudioClip GameOverMeow;


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
        if(GameManager.Instance.isGameOver)
        {
            fadeTime += Time.deltaTime;
            if (fadeTime < 1f)
            {
                BGMSource.volume = Mathf.Lerp(0.2f, 0f, fadeTime);
                GameOverSource.volume = Mathf.Lerp(0, 0.2f, fadeTime);
                GameOverSource.Play();
            }
        }
    }  //default volume is 0.3f
    public void PlaySoundAtLocation(AudioClip clip, Vector2 pos)
    {
        AudioSource temp = Instantiate(source, pos, Quaternion.identity);
        ListofAudioSources.Add(temp);
        temp.clip = clip;
        temp.Play();
        RemoveUnusedAudioSource();
    }
    public void PlaySoundAtLocation(AudioClip clip, float volume, Vector2 pos)
    {
        AudioSource temp = Instantiate(source, pos, Quaternion.identity);
        ListofAudioSources.Add(temp);
        temp.volume = volume;
        temp.clip = clip;
        temp.Play();
        RemoveUnusedAudioSource();
    }
    public void PlaySoundAtLocation(AudioClip clip, float volume, Vector2 pos, bool randPitch = false)
    {
        AudioSource temp = Instantiate(source, pos, Quaternion.identity);
        ListofAudioSources.Add(temp);
        if (randPitch) temp.pitch *= Random.Range(0.8f, 2.5f);   //below 0 can't hear anything, 0.5f sounds slowww 2.5f sounds fast
        temp.volume = volume;
        temp.clip = clip;
        temp.Play();
        RemoveUnusedAudioSource();
    }
    public void PlayCachedSound(AudioClip[] clips, Vector2 pos, float volume, bool randPitch = false)
    {
        AudioSource temp = Instantiate(source, pos, Quaternion.identity);
        ListofAudioSources.Add(temp);
        if (randPitch) temp.pitch *= Random.Range(0.8f, 1.5f);
        temp.volume = volume;
        temp.clip = clips[Random.Range(0, clips.Length)];
        temp.Play();
        RemoveUnusedAudioSource();
    }


    void RemoveUnusedAudioSource()
    {
        if (ListofAudioSources != null)
        {
            for (int i = 0; i < ListofAudioSources.Count; i++)
            {
                if (!ListofAudioSources[i].isPlaying)
                {
                    Destroy(ListofAudioSources[i].gameObject);
                    ListofAudioSources.RemoveAt(i);
                }
            }
        }
    }

}
