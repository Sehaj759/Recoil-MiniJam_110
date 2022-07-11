using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image ammoPack;

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            ammoPack.color = Color.HSVToRGB(Mathf.PingPong(Time.time, 1), 1, 1);
        }    
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
