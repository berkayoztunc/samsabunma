using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        StartCoroutine(dei());
    }

    IEnumerator dei()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
