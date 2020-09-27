using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTyperScript : MonoBehaviour
{

    private float letterPause = 0.09f;
    public AudioClip typeSound1;
    public AudioClip typeSound2;

    string message;
    Text textComp;
    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        StartCoroutine(TypeText());
    }
    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
    }

   
}
