using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Header("Visual Tembakan Inti")]
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private Transform gunBarrel;
    [SerializeField] private float laserDuration = 0.05f;

    [Header("Efek Tambahan (Suara & Flash)")]
    [SerializeField] private ParticleSystem efekMuzzleFlash;
    [SerializeField] private AudioSource suaraTembakan;

    private void Start()
    {
        if (laserLine != null)
        {
            laserLine.enabled = false;
            laserLine.positionCount = 2;
            laserLine.useWorldSpace = true;
        }
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (cam == null)
        {
            Debug.LogWarning("Camera belum di-assign di TargetController.");
            return;
        }

        if (gunBarrel == null)
        {
            Debug.LogWarning("GunBarrel belum di-assign di TargetController.");
            return;
        }

        if (efekMuzzleFlash != null)
        {
        efekMuzzleFlash.Play();
        }

        if (suaraTembakan != null)
        {
            suaraTembakan.Play();
        }

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 endPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            endPoint = hit.point;

            Target target = hit.collider.GetComponent<Target>();
            if (target != null)
            {
                target.Hit();
            }
        }
        else
        {
            endPoint = ray.origin + ray.direction * 100f;
        }

        if (laserLine != null)
        {
            StartCoroutine(ShowLaser(gunBarrel.position, endPoint));
        }
    }

    private IEnumerator ShowLaser(Vector3 startPoint, Vector3 endPoint)
    {
        laserLine.enabled = true;
        laserLine.SetPosition(0, startPoint);
        laserLine.SetPosition(1, endPoint);

        yield return new WaitForSeconds(laserDuration);

        laserLine.enabled = false;
    }
}