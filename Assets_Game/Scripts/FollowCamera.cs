using UnityEngine;
using System.Collections;
using CnControls;


public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    public float positionT;
    public float rotationT;
    public bool clamp;

    public MeshRenderer leftBorderRenderer;
    public MeshRenderer rightBorderRenderer;
    public MeshRenderer groundBorderRenderer;

    private FreeParallax[] parallaxes;
    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        //if (target != null)
        //{
        //    Vector3 newPosition = target.transform.position;
        //    newPosition.z = transform.position.z;
        //    transform.position = newPosition;
        //}
    }

    void OnEnable()
    {
        parallaxes = transform.parent.GetComponentsInChildren<FreeParallax>(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.transform.position;
        
        if (clamp)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);

            if (GeometryUtility.TestPlanesAABB(planes, leftBorderRenderer.bounds) && targetPosition.x < transform.position.x)
                targetPosition.x = transform.position.x;
            else if (GeometryUtility.TestPlanesAABB(planes, rightBorderRenderer.bounds) && targetPosition.x > transform.position.x)
                targetPosition.x = transform.position.x;

            if (GeometryUtility.TestPlanesAABB(planes, groundBorderRenderer.bounds) && targetPosition.y < transform.position.y)
                targetPosition.y = transform.position.y;
        }

        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, positionT * Time.deltaTime);
        foreach(var parallax in parallaxes)
            parallax.Speed = (transform.position.x - newPosition.x) * 100;
        newPosition.z = transform.position.z;
        transform.position = newPosition;

        //Vector3 targetDir = (targetPosition - transform.position);
        //targetDir.y = 0;
        //Vector3 newDir = Vector3.Slerp(transform.forward, targetDir, rotationT * Time.deltaTime);
        ////Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationT * Time.deltaTime, 0.0F);
        //Debug.DrawRay(transform.position, newDir, Color.red);
        //transform.rotation = Quaternion.LookRotation(newDir);
    }
}
