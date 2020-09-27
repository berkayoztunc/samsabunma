using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControle : MonoBehaviour
{
    // Start is called before the first frame update
    

    private Rigidbody rb;
    private int nextUpdate=1;
    private GameObject GregorSamsa;
    public GameObject puff;
    public float enemySpeed = 3.0f;

    public bool mindOut = false;
   void OnEnable(){
        EventManager.OnBlastedMind += DestroyObject;
   }
   void OnDisable(){
        EventManager.OnBlastedMind -= DestroyObject;
    }
    void Start()
    {

        GregorSamsa = GameObject.FindWithTag("Player");
        transform.LookAt(GregorSamsa.transform);
        StartCoroutine(UpdateEverySecond());
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GregorSamsa != null)
        {
            transform.LookAt(GregorSamsa.transform);
        }
        if (GameManager.staticManager.gameStatus)
        {
            transform.position += transform.forward * Time.deltaTime * enemySpeed;
        }
    }

    void DestroyObject(){
        if (mindOut){
            GameManager.countCoconocho();
            Destroy(this.gameObject);
            Instantiate(puff, this.gameObject.transform.position, Quaternion.identity);

        }
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spell")
        {
            //Debug.Log(other.gameObject.tag);
            mindOut = true;
            GameManager.staticManager.touched();
        }
        if (other.gameObject.tag == "Player")
        {

          Destroy(this.gameObject);
          PlayerManager.demage(15);
          GameManager.staticManager.totalCockroachCount--;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "spell")
        {
            StartCoroutine(mided());
        }

    }
    IEnumerator mided()
    {
        yield return new WaitForSeconds(0.05f);
        mindOut = false;
    }
    IEnumerator UpdateEverySecond(){
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
        }
    }

    
}
