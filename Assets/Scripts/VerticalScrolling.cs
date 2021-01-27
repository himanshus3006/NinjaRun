using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrolling : MonoBehaviour
{
    //Config
    [SerializeField] float scrollRate = 0.5f;
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0.0f, scrollRate * Time.deltaTime)); // water rises vertically as per scroll rate

    }
}
