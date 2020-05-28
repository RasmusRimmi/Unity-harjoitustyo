using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjects : MonoBehaviour
{
    public int spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        spinSpeed = 150;
    }

    // Update is called once per frame
    void Update()
    {
        //Find tag "Coin" and rotate its y-axis
        if (CompareTag("Coin"))
        {
            transform.Rotate(new Vector3(0, spinSpeed, 0) * Time.deltaTime);
        }

        //Rotate goals z-axis 
        else
        {
            transform.Rotate(new Vector3(0,0, spinSpeed) * Time.deltaTime);
        }
        
    }
}
