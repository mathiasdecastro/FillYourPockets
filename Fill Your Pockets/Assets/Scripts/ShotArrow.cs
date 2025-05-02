using UnityEngine;

public class ShotArrow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float shootForce = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.linearVelocity = shootPoint.forward * shootForce;

        Destroy(arrow, 5f);
    }
}
