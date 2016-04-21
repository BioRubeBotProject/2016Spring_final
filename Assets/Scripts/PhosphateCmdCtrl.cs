using UnityEngine;
using System.Collections;

public class PhosphateCmdCtrl : MonoBehaviour
{
    public ParticleSystem destructionEffect;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Roam.Roaming(this.gameObject);
        StartCoroutine(DestroyPHOS());
    }

    public IEnumerator DestroyPHOS()     // phosphate explodes after 4 seconds
    {
        yield return new WaitForSeconds(4f);
        ParticleSystem explosionEffect = Instantiate(destructionEffect) as ParticleSystem;
        explosionEffect.transform.position = transform.position;
        explosionEffect.loop = false;
        explosionEffect.Play();
        Destroy(explosionEffect.gameObject, explosionEffect.duration);
        Destroy(gameObject);
    }
}

