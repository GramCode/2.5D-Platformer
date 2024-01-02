using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);

            if (distance < 0.05f)
            {
                Rigidbody boxRigi = other.GetComponent<Rigidbody>();

                if (boxRigi != null)
                    boxRigi.isKinematic = true;

                MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();

                if (mesh != null)
                    mesh.material.color = Color.green;
            }
        }
    }
}
