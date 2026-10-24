using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public GameObject _instance;
 
    void Start() 
    {
        _instance = this.gameObject;
            
       DontDestroyOnLoad(this.gameObject);
       
    }
 
    public void Play()
    {
        //Play some audio!
    }
}
