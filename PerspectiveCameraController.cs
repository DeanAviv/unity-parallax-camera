using UnityEngine;

public class PerspectiveCameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private float groundZ = 0;
    private Vector3 dragOrigin;
    float cam_x_pose, cam_y_pose;
    private float cameraZoom = 80f;
    [SerializeField] float zoomAmountMultiplyer = 1;
    [SerializeField] private bool use_keyboard;
    void Update()
    {
        if (use_keyboard)
        {
            PanCamByKeyInput();
        }
        else
        {
            PanCamByMouseInput();
        }

        HandleZoomByMouseWheel();
    }

    /// <summary>Allows the user to pan the perspective camera by draggin the mouse on screen</summary>
    private void PanCamByMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = GetWorldPosition(groundZ);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = dragOrigin - GetWorldPosition(groundZ);
            cam.transform.position += direction;
        }
    }

    /// <summary>Allows the user to pan the perspective camera by using the arrow keys</summary>
    private void PanCamByKeyInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cam_x_pose = cam.transform.position.x;
            cam_x_pose += 0.01f;
            cam.transform.position = new Vector3(cam_x_pose, cam.transform.position.y, cam.transform.position.z);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            cam_x_pose = cam.transform.position.x;
            cam_x_pose -= 0.01f;
            cam.transform.position = new Vector3(cam_x_pose, cam.transform.position.y, cam.transform.position.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cam_y_pose = cam.transform.position.y;
            cam_y_pose += 0.01f;
            cam.transform.position = new Vector3(cam.transform.position.x, cam_y_pose, cam.transform.position.z);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            cam_y_pose = cam.transform.position.y;
            cam_y_pose -= 0.01f;
            cam.transform.position = new Vector3(cam.transform.position.x, cam_y_pose, cam.transform.position.z);
        }
    }

    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }


    /// <summary>Allows the user to change the field of view - zooming in or out using the mouse wheel</summary>
    private void HandleZoomByMouseWheel()
    {

        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            cameraZoom = cam.transform.position.z;
            cameraZoom += (Input.GetAxis("Mouse ScrollWheel") * zoomAmountMultiplyer);
            cam.transform.position = new Vector3(mousePos.x, mousePos.y, cameraZoom);
        }

    }
}
