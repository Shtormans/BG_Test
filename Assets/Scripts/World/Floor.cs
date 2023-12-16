using UnityEngine;

public class Floor : MonoBehaviour
{
    private Vector3 _size;
    private Vector3 _upperLeftCorner;

    public Vector3 Size => _size;
    public Vector3 BottomLeftCorner => _upperLeftCorner;

    private void Awake()
    {
        var renderer = GetComponent<Renderer>();

        _size = renderer.bounds.size;
        _upperLeftCorner = new Vector3(transform.position.x - _size.x / 2, transform.position.y, transform.position.z - _size.z / 2);
    }
}
