using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private Vector3 offset;
    public float yoffset;

    public GameObject player;
    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;
        
        transform.position = new Vector3(transform.position.x, yoffset, transform.position.z);
      

    }
}
