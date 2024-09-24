using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] public int burstAmount = 3;
    [SerializeField] public float burstDelay = 0.1f;
    [SerializeField] public float reloadDelay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Fire()
    {
        if (canFire)
        {
            StartCoroutine(BurstFire());
        }
    }

    private IEnumerator BurstFire()
    {
        for (int i = 0; i < burstAmount; i++)
        {
            if (currentSpareAmmo > 0)
            {
                currentSpareAmmo -= 1;

                RaycastHit hit;

                if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 500))
                {
                    Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);

                    if (hit.transform.CompareTag("Zombie"))
                    {
                        hit.transform.GetComponent<Enemy>().TakeDamage(1);
                    }
                }
            }
            else
            {
                break; // stop firing if out of ammo
            }

            // burst cooldown
            canFire = false;
            yield return new WaitForSeconds(burstDelay);
            canFire = true;
        }
    }

    protected override void Reload()
    {
        base.Reload();

        StartCoroutine(ReloadCooldown()); // cant fire for a few
    }

    private IEnumerator ReloadCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(reloadDelay);
        canFire = true;

    }
}
