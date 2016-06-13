using UnityEngine;
using System.Collections;
 
public class AutoDestroyParticle : MonoBehaviour
{

    //private IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);
    //    Destroy(gameObject); 
    //}

    ParticleSystem particleSystem;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}