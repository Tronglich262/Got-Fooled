using UnityEngine;

public class DontdestroyLoadSceen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public DontdestroyLoadSceen instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
