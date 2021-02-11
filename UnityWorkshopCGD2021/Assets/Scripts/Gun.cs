using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Gun : MonoBehaviour
{
    [Header("Components")]
    Animator gunAnimator;
    [SerializeField] Transform nozzle;

    [Header("Properties")]
    [SerializeField] float shootRate;
    private float shootCooldown;
    [SerializeField] float reloadRate;
    private float reloadCooldown;
    [SerializeField] float clipSize;
    private float currentClip;
    [SerializeField] float force;



    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = GetComponent<Animator>();
        currentClip = clipSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentClip > 0 && shootCooldown <= 0)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && reloadCooldown <= 0)
        {
            Reload();
        }
        reloadCooldown -= Time.deltaTime;
        shootCooldown -= Time.deltaTime;
        Debug.DrawRay(nozzle.transform.position, nozzle.transform.forward * 15.0f, Color.red);
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(nozzle.transform.position, nozzle.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("Target") && hit.transform.GetComponent<Rigidbody>() != null)
            {
                var rb = hit.transform.GetComponent<Rigidbody>();
                var hitVector = (hit.transform.position - nozzle.transform.position).normalized;
                rb.AddForce(hitVector * force);
                rb.AddTorque(hitVector * force);
            }
        }
        gunAnimator.Play("GunShoot", 0);
        shootCooldown = shootRate;
        currentClip--;
        Debug.Log(currentClip);
    }

    void Reload()
    {
        gunAnimator.Play("GunReload", 0);
        reloadCooldown = reloadRate;
        currentClip = clipSize;
    }
}
