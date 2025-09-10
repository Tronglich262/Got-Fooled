using UnityEngine;
using UnityEngine.SceneManagement;

public class FuncButtonMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playergame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Setting()
    {
      
    }
    public void Extras()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
}
