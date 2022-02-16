using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItem : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _rend;

    private Material _material;

    private Vector3 _origPos, _origRot;

    private void Start()
    {
        _material = new Material(_rend.material);

        _rend.material = _material;

        _origPos = transform.localPosition;

        _origRot = transform.localRotation.eulerAngles;
    }

    public void Drop()
    {
        StartCoroutine(DropItem());
    }

    private IEnumerator DropItem()
    {
        Rigidbody rig = transform.gameObject.AddComponent<Rigidbody>();

        rig.mass = 0.1f;

        rig.AddForce(new Vector3(Random.Range(-20, 20), 40, Random.Range(-20, 20)));

        StartCoroutine(ColorToTransparent(5, 0.5f));

        Vector3 rotDelta = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

        for (int i = 0; i < 200; i++)
        {
            transform.Rotate(rotDelta, Space.Self);

            yield return null;
        }        
    }

    private IEnumerator ColorToTransparent(int frames, float delay)
    {
        yield return new WaitForSeconds(delay);

        Color c = _material.color;

        float delta = c.a / frames;
        
        for (int i = 0; i < frames; i++)
        {
            c.a -= delta;
            _material.color = c;

            yield return null;
        }        
    }

    public void Refresh()
    {
        StopAllCoroutines();

        Destroy(GetComponent<Rigidbody>());

        transform.localPosition = _origPos;

        transform.localRotation = Quaternion.Euler(_origRot);

        Color c = _material.color;

        c.a = 1;

        _material.color = c;
    }
}
