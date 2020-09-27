using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void BlastedMindAction();
    public static event BlastedMindAction OnBlastedMind;
    public static void BlastedMind()
    {
        if (OnBlastedMind != null)
        {
            OnBlastedMind();
        }
    }

    
}
