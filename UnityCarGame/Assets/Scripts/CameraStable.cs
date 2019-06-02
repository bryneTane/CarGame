using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStable : MonoBehaviour
{
    public GameObject car;
    public float CarX;
    public float CarY;
    public float CarZ;


    // Update is called once per frame
    void Update()
    {
        CarX = car.transform.eulerAngles.x;
        CarY = car.transform.eulerAngles.y;
        CarZ = car.transform.eulerAngles.z;

        transform.eulerAngles = new Vector3(CarX - CarX, CarY, CarZ - CarZ);
    }
}
