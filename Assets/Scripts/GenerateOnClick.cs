using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GenerateOnClick : MonoBehaviour
//{
//    public GameObject original;  // drag the original gameobject into this field in the inspector
//    public GameObject copy;      // drag the prefab for the copy into this field in the inspector

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        Instantiate a copy of the original gameobject
//       GameObject newCopy = Instantiate(copy, original.transform.position, original.transform.rotation);

//        Apply the same Spring Joint 2D component to the new copy as on the original
//        SpringJoint2D springJoint = original.GetComponent<SpringJoint2D>();
//        newCopy.AddComponent<SpringJoint2D>().connectedAnchor = springJoint.connectedAnchor;
//        newCopy.GetComponent<SpringJoint2D>().distance = springJoint.distance;
//        newCopy.GetComponent<SpringJoint2D>().dampingRatio = springJoint.dampingRatio;
//        newCopy.GetComponent<SpringJoint2D>().frequency = springJoint.frequency;
//    }
//}





{
    public GameObject objectToCopy;
    public Transform generationPoint;

    private SpringJoint2D originalSpringJoint;

    private Rigidbody2D m_rigidbody;                // RigidBody dla kulki
    private SpringJoint2D m_connectedJoint;         // field do kontrolowania polaczenia sprezynowego w kuli
    private Rigidbody2D m_connectedBody;            // RigidBody dla punktu sprezynowego kulki

    private void Start()
    {
        // Get the Spring Joint 2D component on the original GameObject
        originalSpringJoint = objectToCopy.GetComponent<SpringJoint2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                GameObject copy = Instantiate(objectToCopy, generationPoint.position, Quaternion.identity);
                copy.name = objectToCopy.name;



                // Add a Spring Joint 2D component to the copy
                SpringJoint2D copySpringJoint = copy.AddComponent<SpringJoint2D>();

                // Copy the properties of the original Spring Joint 2D to the copy
                copySpringJoint.autoConfigureConnectedAnchor = originalSpringJoint.autoConfigureConnectedAnchor;
                copySpringJoint.anchor = originalSpringJoint.anchor;
                copySpringJoint.connectedAnchor = originalSpringJoint.connectedAnchor;
                copySpringJoint.distance = originalSpringJoint.distance;
                copySpringJoint.dampingRatio = originalSpringJoint.dampingRatio;
                copySpringJoint.frequency = originalSpringJoint.frequency;
                copySpringJoint.enableCollision = originalSpringJoint.enableCollision;
                //copySpringJoint.enablePreprocessing = originalSpringJoint.enablePreprocessing;
                //copySpringJoint.massScale = originalSpringJoint.massScale;
                //copySpringJoint.connectedMassScale = originalSpringJoint.connectedMassScale;
                //copySpringJoint.collideConnected = originalSpringJoint.collideConnected;
            }
        }
    }
}



