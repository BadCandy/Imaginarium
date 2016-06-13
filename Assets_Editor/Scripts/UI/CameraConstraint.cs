using UnityEngine;
using System.Collections;

public class CameraConstraint : MonoBehaviour {

    private BoxCollider2D cameraBox;
    private Transform player;
	// Use this for initialization
	void Start () {
        cameraBox = GetComponent<BoxCollider2D>();
        player = Camera.main.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        Constraint();
	}

    void Constraint()
    {
        if (GameObject.Find("Boundary"))
            transform.position = new Vector3(Mathf.Clamp (player.position.x, GameObject.Find("Boundary").GetComponent<BoxCollider2D> ().bounds.min.x + cameraBox.size.x / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),
                                            Mathf.Clamp (player.position.y, GameObject.Find("Boundary").GetComponent<BoxCollider2D> ().bounds.min.y + cameraBox.size.y / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),
                                             transform.position.z);
    }
}
