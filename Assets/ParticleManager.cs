using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {


    List<GameObject> activeSystems = new List<GameObject>();

    public void SpawnParticleSystem(GameObject system, Vector2 position, Color color)
    {
        if (system.GetComponent<ParticleSystem>() != null)
        {
            GameObject currentSystem = Instantiate(system, position, Quaternion.identity) as GameObject;
            ParticleSystem particles = currentSystem.GetComponent<ParticleSystem>();
            var main = particles.main;
            main.startColor = color;
            particles.Play();
            activeSystems.Add(currentSystem);
        }
    }

    private void Update()
    {
        for (int i = 0; i < activeSystems.Count; i++)
        {
            if(!activeSystems[i].GetComponent<ParticleSystem>().isPlaying)
            {
                Destroy(activeSystems[i]);
                activeSystems.RemoveAt(i);
            }
        }
    }
}
