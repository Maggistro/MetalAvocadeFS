using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    [SerializeField] BoxCollider left;
    [SerializeField] BoxCollider right;
    [SerializeField] BoxCollider front;
    [SerializeField] BoxCollider back;
    [SerializeField] BoxCollider up;

    private void AdjustColliders()
    {
        Bounds bounds = GetComponent<Mesh>().bounds;
        Vector3 extents = bounds.extents;//     The extents of the Bounding Box. This is always half of the size of the Bounds.
        //up set center z
    }
}
