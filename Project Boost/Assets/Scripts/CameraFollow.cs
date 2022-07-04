using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    //how many second to smooth the animation
    public float smoothSpeed = 0f;
    public Vector3 offset;

    //by using late update, the camera will follow target after it moved, so no jittering will appeared (hopefully)
    void FixedUpdate() 
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //using this, unity will take care of the rotational caculation for us
        transform.LookAt(target);
    }
}
