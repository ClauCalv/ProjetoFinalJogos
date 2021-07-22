using UnityEngine;
using System.Collections;
using Map;

public class CameraMovement : MonoBehaviour
{
    public const float cameraMinHeight = 12, cameraMaxHeight = 30, cameraAngle = -15;

    Transform cameraPos;
    Transform cameraFocus;
    GridSystem grid;

    public float cameraMoveSpeed = 1;
    public float cameraTurnSpeed = 1;
    public float cameraZoomSpeed = 1;

    float cameraHeight;

    private void Awake()
    {
        cameraPos = transform;
        cameraFocus = transform.parent;
    }

    public void Init(GridSystem grid)
    {
        this.grid = grid;
        
        cameraFocus.position = grid.GetPosFromCoords(grid.size / 2);
        cameraFocus.rotation = Quaternion.Euler(cameraAngle, 0, 0);
        cameraPos.localPosition = Vector3.up * 20;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraMove = Vector3.zero;
        Vector3 cameraTurn = Vector3.zero;
        Vector3 cameraZoom = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) cameraMove += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) cameraMove += Vector3.back;
        if (Input.GetKey(KeyCode.A)) cameraMove += Vector3.left;
        if (Input.GetKey(KeyCode.D)) cameraMove += Vector3.right;
        if (Input.GetKey(KeyCode.Q)) cameraTurn += Vector3.up;
        if (Input.GetKey(KeyCode.E)) cameraTurn += Vector3.down;
        if (Input.GetKey(KeyCode.Z)) cameraZoom += Vector3.down;
        if (Input.GetKey(KeyCode.X)) cameraZoom += Vector3.up;

        cameraMove.Normalize();

        cameraMove *= cameraMoveSpeed * Time.deltaTime;
        cameraTurn *= cameraTurnSpeed * Time.deltaTime;
        cameraZoom *= cameraZoomSpeed * Time.deltaTime;

        Vector3 newFocus = cameraFocus.position + cameraMove;
        Vector3 minFocus = grid.GetPosFromCoords(Vector2Int.zero);
        Vector3 maxFocus = grid.GetPosFromCoords(grid.size);
        cameraFocus.position = newFocus.Clamp(minFocus, maxFocus);

        cameraFocus.rotation = Quaternion.Euler(cameraFocus.rotation.eulerAngles + cameraTurn);

        Vector3 newHeight = cameraPos.localPosition + cameraZoom;
        Vector3 minHeight = Vector3.up * cameraMinHeight;
        Vector3 maxHeight = Vector3.up * cameraMaxHeight;
        cameraPos.localPosition = newHeight.Clamp(minHeight, maxHeight);
    }

}

public static class VectorClamp
{
    public static Vector3 Clamp (this Vector3 value, Vector3 min, Vector3 max)
    {
        return new Vector3(
            Mathf.Clamp(value.x, min.x, max.x),
            Mathf.Clamp(value.y, min.y, max.y),
            Mathf.Clamp(value.z, min.z, max.z) );
    }
}
