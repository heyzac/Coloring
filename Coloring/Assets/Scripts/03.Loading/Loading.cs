using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public GameObject tumbleArrow;
    private Vector3 vector3 = new Vector3(0f, 0f, -2f);

    void Update()
    {
        tumbleArrow.transform.Rotate(vector3);
    }
}
