using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {get; private set;}
    private EventInstance bgmEventInstance;

    [field: Header("Scene BGM")]
    [field: SerializeField] public EventReference sceneBgm {get; private set;} 

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one Audio Manager instance on scene");
        }
        
        instance = this;
    }

    private void Start()
    {
        InitializeBackgroundMusic(sceneBgm);
    }

    private void InitializeBackgroundMusic(EventReference bgm)
    {
        bgmEventInstance = CreateInstance(sceneBgm);
        bgmEventInstance.start();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
