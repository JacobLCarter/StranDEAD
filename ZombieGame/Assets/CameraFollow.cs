using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(-1.44f,7.66f,-10.1f);
    public float sensitivity = 3f;
    [SerializeField]
    private Camera cam;
    // Update is called once per frame
    void Update()
    {
        float xRotation = Input.GetAxis("Mouse Y");
        Vector3 cameraXRotation = new Vector3 (xRotation, 0, 0) * sensitivity;
        float yRotation = Input.GetAxis("Mouse X");
        Vector3 cameraYRotation = new Vector3 (0, yRotation, 0) * sensitivity;
        cam.transform.Rotate(-cameraXRotation);
        cam.transform.Rotate(cameraYRotation);

        transform.position = player.position + offset;
    }
}
