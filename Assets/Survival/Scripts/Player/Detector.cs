using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Detector : MonoBehaviour
{
    public event Action<GameObject> OnInteractableObjectFound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    Plane plane = new Plane(Vector3.up, Vector3.zero);
    float rayLength;

    void Update()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
           
            transform.forward = new Vector3(pointToLook.x - transform.position.x, 0f, pointToLook.z - transform.position.z);
        }
    }

   
}
