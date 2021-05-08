// author: Ridge Diffine
//  This file is responisbile for creating pointer to be sued with menus,
//  this script only creates the pointer does not handle events/
//
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PointerSteam : MonoBehaviour
{

    public float m_defualtLength = 5.0f;
    public GameObject m_Dot;
    //public VRInputModule m_InputModule;

    private LineRenderer m_LineRender = null;



    private void Awake()
    {
        m_LineRender = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    //used to update the point distance while pointing it at different objects that also have colliders
    // has small dot at end of pointer that has collider and is used to trigger the UI for menus
    private void UpdateLine()
    {
        float targetLength = m_defualtLength;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if(hit.collider != null)
        {
            endPosition = hit.point;
        }

        m_Dot.transform.position = endPosition;

        m_LineRender.SetPosition(0, transform.position);
        m_LineRender.SetPosition(1, endPosition);
    }

    //creats the ray cast for the pointer
    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_defualtLength);

        return hit;
    }
}
