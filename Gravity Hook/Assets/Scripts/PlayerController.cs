using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask layerHookeable;

    private Camera _camera;
    private TargetJoint2D _targetJoint2D;
    private LineRenderer _lineRenderer;

    private Enemy _currentEnemy;

    private void Awake()
    {
        _camera = Camera.main;
        _targetJoint2D = GetComponent<TargetJoint2D>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        _targetJoint2D.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _lineRenderer.SetPosition(0, transform.position);
        }

        if (Input.GetMouseButtonDown(0) && _currentEnemy == null)
        {
            _currentEnemy = DetectEnemy(_camera.ScreenPointToRay(Input.mousePosition));

            if (_currentEnemy != null)
            {
                _targetJoint2D.enabled = true;
                _targetJoint2D.target = _currentEnemy.transform.position;
                _currentEnemy.ChangeState(true);

                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(1, _currentEnemy.transform.position);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_currentEnemy != null)
            {
                _currentEnemy.ChangeState(false);
                _currentEnemy = null;
            }

            _lineRenderer.enabled = false;
            _targetJoint2D.enabled = false;
        }
    }

    private Enemy DetectEnemy(Ray ray)
    {
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, layerHookeable);

        if (hit.collider != null)
        {
            return hit.transform.gameObject.GetComponent<Enemy>();
        }

        return null;
    }
}