using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimation : MonoBehaviour
{

    public float speedRotationX;
    public float speedRotationY;
    public float speedRotationZ;

    public float speedTranslationX;
    public float speedTranslationY;
    public float speedTranslationZ;

    public float speedScalingX;
    public float speedScalingY;
    public float speedScalingZ;



    void Update()
    {
        transform.Rotate(new Vector3(speedRotationX, speedRotationY, speedRotationZ) * Time.deltaTime);
        transform.Translate(new Vector3(speedTranslationX / 10, speedTranslationY / 10, speedTranslationZ / 10) * Time.deltaTime);
        transform.localScale += new Vector3(speedScalingX / 10, speedScalingY / 10, speedScalingZ / 10);
    }
}
