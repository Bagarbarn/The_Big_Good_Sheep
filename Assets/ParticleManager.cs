using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {


    List<GameObject> activeSystems = new List<GameObject>();

    public void SpawnParticleSystem(GameObject system, Vector2 position, Color color)
    {
        if (system.GetComponent<ParticleSystem>() != null)
        {
            ParticleSystem.MinMaxGradient gradient;
            GameObject currentSystem = Instantiate(system, position, Quaternion.identity) as GameObject;
            ParticleSystem particles = currentSystem.GetComponent<ParticleSystem>();
            var main = particles.main;
            gradient = main.startColor;
            if (color != Color.black) main.startColor = color;
            else main.startColor = main.startColor.Evaluate(Random.Range(0f, 1f));
            particles.Play();
            if (color == Color.black)
            {
                ParticleSystem.Particle[] parts = new ParticleSystem.Particle[30];
                particles.GetParticles(parts);

                // Note: In theory and debug it changes the colors randomly but it is not visible in the game
                for(int i = 0;  i < parts.Length; i++)
                {
                    ParticleSystem.Particle psp = parts[i];
                    float rand = Random.Range(0f, 1f);
                    psp.startColor = gradient.Evaluate(rand);
                }
            }
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
