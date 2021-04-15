using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairControllerDriver : MonoBehaviour
{
    /*
    void FixedUpdate() {
        // check collisions
        int numOverlaps = Physics.OverlapBoxNonAlloc(transform.position, m_halfExtents, m_colliders,
            m_rigidBody.rotation, layerMask, QueryTriggerInteraction.UseGlobal);
        for (int i = 0; i < numOverlaps; i++) {
            Vector3 direction;
            float distance;
            if (Physics.ComputePenetration(m_boxCollider, transform.position,
                transform.rotation, m_colliders[i], m_colliders[i].transform.position,
                m_colliders[i].transform.rotation, out direction, out distance))
            {
                Vector3 penetrationVector = direction*distance;
                transform.position = transform.position + penetrationVector;
                Debug.Log("OnCollisionEnter with " + m_colliders[i].gameObject.name +
                          " penetration vector: " + penetrationVector + " projected vector: " 
                          + velocityProjected);
            }
            else
            {
                Debug.Log("OnCollision Enter with " + m_colliders[i].gameObject.name +
                          " no penetration");
            }
        }
    }
    */
    

}
