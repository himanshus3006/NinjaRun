using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Cache Component References
    Player myPlayer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<Player>();    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newCameraPosition = new Vector2(myPlayer.transform.position.x, myPlayer.transform.position.y); // gets position of player
        this.transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y,this.transform.position.z); // assigns player position to camera
    }
}
