using System.Collections;
using UnityEngine;

public class DestroyedPlayer : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 20f;
    [SerializeField] private float _radius = 10f;
    [SerializeField] private float _secondsBeforeDestroying = 3;

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);

        for (int i = 0; i < transform.childCount; i++)
        {
            var rigidbody = transform.GetChild(0).GetComponent<Rigidbody>();

            rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius);
        }

        StartCoroutine(DyingAnimation());
    }

    private IEnumerator DyingAnimation()
    {
        yield return new WaitForSeconds(_secondsBeforeDestroying);

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
