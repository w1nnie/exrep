using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerCameraPos = new Vector3(
            player.transform.position.x,
            this.transform.position.y,
            this.transform.position.z
        );
        transform.position = Vector3.Lerp(transform.position, playerCameraPos, 7.0f * Time.deltaTime);
    }
}
