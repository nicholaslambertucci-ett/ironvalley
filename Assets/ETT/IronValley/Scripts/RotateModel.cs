using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ett.IronValley.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class RotateModel : MonoBehaviour
    {
        public float Speed = 0.05f;

        Rigidbody rb;
        Vector3 mousePosRef;

        // Start is called before the first frame update
        void Start()
        {
            rb = this.GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
                mousePosRef = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                var mousePos = Input.mousePosition;
                Ray mouseCast = Camera.main.ScreenPointToRay(mousePos);
                var offset = mousePos - mousePosRef;
                RaycastHit hit;

                if (Physics.Raycast(mouseCast, out hit, 100))
                {
                    //Debug.Log("hit: " + hit.transform.name);
                    rb.AddTorque(transform.up * Speed * offset.x);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                mousePosRef = Vector3.zero;
            }
        }
    }
}