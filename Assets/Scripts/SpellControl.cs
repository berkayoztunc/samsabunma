using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellControl : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool active = false;
    public bool firstTab = true;
    public GameObject cameraPossObject;
    public Vector3 startCamerePos;
    public Animator SamsaAniamtor;
    public Text startText;
    private bool firstLevel = true;
    void Start()
    {
        startCamerePos = cameraPossObject.transform.position;
        startText.text = "";

    }
    public void fire(){
        startText.text = "";
        Time.timeScale = 1;
        if (firstTab)
        {
            firstTab = false;
            
        }
        if (PrefManager.GetInt("last_level") < 2 && !GameManager.staticManager.readyToPlay)
        {
            startText.text = "Büyü atmak için tıkla";
        }
        if (GameManager.staticManager.readyToPlay)
        {
            
            if (active)
            {

                boom();
                SamsaAniamtor.SetBool("isSpell", false);
            }
            else
            {
                GameManager.staticManager.comboCount = 0;
                SamsaAniamtor.SetBool("isSpell", true);
                active = true;
                gameObject.SetActive(true);
                StartCoroutine(MindArea());
                StartCoroutine(camUpper());
                if (firstLevel && PrefManager.GetInt("last_level") < 2)
                {
                    firstLevel = false;
                    StartCoroutine(pasueGame());
                }
            }
        }
        
    }
    public void Update()
    {
        
    }
    public void boom()
    {
        EventManager.BlastedMind();
        active = false;
       
        //cameObject.transform.position = startCamerePos;
        StartCoroutine(camDown());
    }
    IEnumerator pasueGame()
    {
        yield return new WaitForSeconds(0.4f);
        startText.text = "Halka onlara ulaştığında tekrar bas ve düşünce halkasına değenleri dönüştür.";
        Time.timeScale = 0;
    }
    IEnumerator camUpper() 
    {
        int handler = 0;
        while (active && handler < 30)
        {
            handler++;
            cameraPossObject.transform.position = Vector3.Lerp(cameraPossObject.transform.position, cameraPossObject.transform.position + (Vector3.up * 0.05f), 20f);
            yield return new WaitForSeconds(0.01f);
        }

    }
    IEnumerator camDown()
    {
        
        int handler = 0;
        while (handler < 3)
        {
            handler++;
            cameraPossObject.transform.position = Vector3.Lerp(cameraPossObject.transform.position, cameraPossObject.transform.position + (Vector3.down * 0.5f),1);
            yield return new WaitForSeconds(0.01f);
        }
        cameraPossObject.transform.position = startCamerePos;
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject.SetActive(false);
    }
    IEnumerator MindArea()
    {
        //yield return new WaitForSeconds(0.5f);
        while (active)
        {
            float duration = 10;
            Vector3 startScale = transform.localScale;
            Vector3 endScale = new Vector3(startScale.x * 1.1f, 0.1f, startScale.z * 1.1f);
            gameObject.transform.localScale = Vector3.Lerp(startScale, endScale, duration);
           
            yield return new WaitForSeconds(0.01f);
        }
       

    }
}
