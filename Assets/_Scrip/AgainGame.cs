using UnityEngine;

public class AgainGame : MonoBehaviour
{
    public void again()
    {
        if (Movement.instance != null)
        {
            Movement.instance.ResetGame();
        }
    }
}
