using UnityEngine;
using UnityEngine.SceneManagement;

public class Quaman : MonoBehaviour
{
    public string name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(name);
        }
    }
}
