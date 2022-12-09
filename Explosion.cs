using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Method to destroy the explosion animation after a set amount of time
        Destroy(this.gameObject, 1.8f);
    }
}
