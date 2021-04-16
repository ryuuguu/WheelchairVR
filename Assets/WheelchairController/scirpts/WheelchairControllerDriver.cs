using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairControllerDriver : MonoBehaviour {
    public List<BoxCollider> colliders;
    private  List<Vector3> _worldHalfExtents;
    private Collider[] _otherColliders;

    public void Awake() {
        foreach (var c in colliders) {
            _worldHalfExtents.Add(c.transform.TransformVector(c.size * 0.5f));
        }
    }

    void FixedUpdate() {
        // check collisions
        var aCollider = colliders[0];
        var worldHalfExtent = _worldHalfExtents[0];
        Vector3 worldCenter = aCollider.transform.TransformPoint(aCollider.center);
        int numOverlaps = Physics.OverlapBoxNonAlloc(worldCenter, worldHalfExtent, _otherColliders,
            aCollider.transform.rotation); 
        for (int i = 0; i < numOverlaps; i++) {
            Vector3 direction;
            float distance;
            if (Physics.ComputePenetration(aCollider, transform.position,
                transform.rotation, _otherColliders[i], _otherColliders[i].transform.position,
                _otherColliders[i].transform.rotation, out direction, out distance))
            {
                Vector3 penetrationVector = direction*distance;
                transform.position = transform.position + penetrationVector;
                Debug.Log("OnCollisionEnter with " + _otherColliders[i].gameObject.name +
                          " penetration vector: " + penetrationVector );
            }
            else
            {
                Debug.Log("OnCollision Enter with " + _otherColliders[i].gameObject.name +
                          " no penetration");
            }
        }
    }
    
    

}
