using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance {get; private set;}

    [field: Header("Scream SFX")]
    [field: SerializeField] public EventReference screamSFX {get; private set;} 

    [field: Header("Boing SFX")]
    [field: SerializeField] public EventReference boingSFX {get; private set;} 

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one FMOD Events instance on scene");
        }
        
        instance = this;
    }


}
