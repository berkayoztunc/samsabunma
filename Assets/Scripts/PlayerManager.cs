using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public Healthbar healthbarScript;
    public static PlayerManager instant;
    public ParticleSystem puffHelloModerF;
    // Start is called before the first frame update
    void Start()
    {
        if (instant == null)
        {
            instant = this;
        }
        puffHelloModerF.gameObject.SetActive(true);
        puffHelloModerF.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (healthbarScript.health <= 0 && GameManager.staticManager.gameStatus) {
           
            GameManager.gameOver();
        };
    }
     void OnCollisionEnter(Collision other)
    {
       
    }
    public static void demage(int count)
    {
        instant.healthbarScript.TakeDamage(count);
    }
}
