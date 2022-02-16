using Interfaces;
using UnityEngine;

public class Food : MonoBehaviour,IDamagable
{
    [SerializeField] private ParticleSystem _foodDestroy;
    private FoodKit _foodKit;
    private bool _isDestroy;
    private Vector3 _startPosition;
    private SphereCollider _sphereCollider;
    private MeshRenderer _mesh;
    private void Start()
    {
        _startPosition = transform.position;
        _sphereCollider = GetComponent<SphereCollider>();
        _mesh = GetComponentInChildren<MeshRenderer>();
        _foodKit=FindObjectOfType<FoodKit>();
    }

    public Transform GetTransform()
    {
        return _isDestroy ? null : transform;
    }

    public void ReturnFood()
    {
        transform.position = _startPosition;
        _foodKit.ReturnFood(this);
    }

    public void DeleteFood()
    {
        if (_isDestroy) return;
        gameObject.SetActive(false);
        _foodKit.RemoveFood(this);
    }
    public void TakeDamage()
    {
        _isDestroy = true;
        _foodDestroy.Play();
        _mesh.enabled = false;
        _sphereCollider.enabled = false;
        _foodKit.RemoveFood(this);
    }
}
