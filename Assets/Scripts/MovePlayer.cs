using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody2D _rg;
    public float speed;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        _rg = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // MovePlayer
        var horizontal = Input.GetAxis("Horizontal") * speed;
        var vertical = Input.GetAxis("Vertical") * speed;
        
        _rg.velocity = new Vector2(horizontal, vertical);
        
        // RotatePlayer
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = GetRotationTo(mousePosition);
    }

    private Quaternion GetRotationTo(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
