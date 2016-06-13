using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    public float t;
    private GameObject player;
    void Awake()
    {
        Vector3 newPosition = target.transform.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 newPlayerPosition = Vector3.Lerp(Camera.main.transform.position, player.transform.position, t);
            newPlayerPosition.z = Camera.main.transform.position.z;
            Camera.main.transform.position = newPlayerPosition;
        }
        else
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, target.transform.position, t);
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
    }

    public void FollowCameraToPlayer()
    {
        player = GameObject.Find("Player(Clone)");
        if (player != null)
        {
            Vector3 newPlayerPosition = player.transform.position;
            newPlayerPosition.z = Camera.main.transform.position.z;
            Camera.main.transform.position = newPlayerPosition;
        }
    }
}
