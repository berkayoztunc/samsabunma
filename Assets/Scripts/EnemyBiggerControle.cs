using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBiggerControle : MonoBehaviour
{
    // Start is called before the first frame update
    

    private Rigidbody rb;
    private int nextUpdate=1;
    private GameObject GregorSamsa;
    public GameObject puff;
    private float enemySpeed = 2.0f;
    private int helt = 3;

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
        transform.position += transform.forward * Time.deltaTime * enemySpeed; 
    }

    void DestroyObject(){
        if (mindOut){
            helt--;
            if(helt <= 0)
            {
                GameManager.countCoconocho();
                Destroy(this.gameObject);
                Instantiate(puff, this.gameObject.transform.position, Quaternion.identity);
            }
           
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
          PlayerManager.demage(100);
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
