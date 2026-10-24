using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public static GameObject _instance;
 
    void Awake() 
    {
        if(_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if(this != _instance)
                Destroy(this.gameObject);
        }
    }
 
    public void Play()
    {
        //Play some audio!
    }
}
