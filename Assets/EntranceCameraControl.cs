using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceCameraControl : MonoBehaviour
{
    public GameObject camHolder;
    public float elapsedTime= 0;
    public float waitTime = 6;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(camUpper());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator camUpper()
    {
        while (elapsedTime < waitTime)
        {
            transform.position = Vector3.Lerp(transform.position, camHolder.transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        GameManager.staticManager.readyToPlay = true;

    }
}
