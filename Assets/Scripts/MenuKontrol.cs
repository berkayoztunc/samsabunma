using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuKontrol : MonoBehaviour
{
    // Start is called before the first frame update


    public string[] maps ={ "med","hard","semp"};
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play()
    {
        PrefManager.SetInt("last_level", 1);
        PrefManager.SetInt("GameType", 1);
        entrance();
    }
    public void med()
    {
        PrefManager.SetInt("last_level", 1);
        PrefManager.SetInt("GameType", 3);
        entrance();

    }
    public void hard()
    {
        PrefManager.SetInt("last_level", 1);
        PrefManager.SetInt("GameType", 5);
        entrance();


    }
    public void entrance()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("entrance");
    }
    public void next()
    {
        rndMap();
    }
    public void rndMap()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(maps[Random.Range(0,maps.Length)]);
    }
    public void menu()
    {
        
       SceneManager.LoadScene("menu");
    }
    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
