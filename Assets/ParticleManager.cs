using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {


    public class ParticleEffect
    {
        public GameObject particleSystem;
        public bool rainbow;
        public bool colorsSet;
        public ParticleEffect(GameObject part)
        {
            particleSystem = part;
            rainbow = false;
            colorsSet = false;
        }
    }



    List<ParticleEffect> activeSystems = new List<ParticleEffect>();



    public void SpawnParticleSystem(GameObject system, Vector2 position, Color color)
    {
        if (system.GetComponent<ParticleSystem>() != null)
        {
            ParticleSystem.MinMaxGradient gradient;
            GameObject currentSystem = Instantiate(system, position, Quaternion.identity) as GameObject;
            ParticleSystem particles = currentSystem.GetComponent<ParticleSystem>();
            var main = particles.main;
            gradient = main.startColor;


            ParticleEffect partEffect = new ParticleEffect(currentSystem);
            if (color != Color.black) main.startColor = color;
            else { main.startColor = main.startColor.Evaluate(Random.Range(0f, 1f)); partEffect.rainbow = true; }
            particles.Play();



            //if (color == Color.black)
            //{
            //    ParticleSystem.Particle[] parts = new ParticleSystem.Particle[particles.particleCount];
            //    int num = particles.GetParticles(parts);
            //    Debug.Log(parts.Length);

            //    // Note: In theory and debug it changes the colors randomly but it is not visible in the game
            //    for(int i = 0;  i < parts.Length; i++)
            //    {
            //        Debug.Log("I'm doing it");
            //        float rand = Random.Range(0f, 1f);
            //        parts[i].startColor = main.startColor.Evaluate(Random.Range(0f, 1f));
            //    }
            //    Debug.Log("I can change the system!");
            //    currentSystem.GetComponent<ParticleSystem>().SetParticles(parts, parts.Length);
            //}
            activeSystems.Add(partEffect);
        }
    }

    private void Update()
    {
        for (int i = 0; i < activeSystems.Count; i++)
        {
            if(!activeSystems[i].particleSystem.GetComponent<ParticleSystem>().isPlaying)
            {
                Destroy(activeSystems[i].particleSystem);
                activeSystems.RemoveAt(i);
            }
        }
    }

    private void LateUpdate()
    {

        for (int i = 0; i < activeSystems.Count; i++)
        {
            if (activeSystems[i].rainbow && !activeSystems[i].colorsSet)
            {
                ParticleSystem particles = activeSystems[i].particleSystem.GetComponent<ParticleSystem>();
                var main = particles.main;
                ParticleSystem.Particle[] parts = new ParticleSystem.Particle[particles.particleCount];
                int num = particles.GetParticles(parts);
                Debug.Log(parts.Length);

                // Note: In theory and debug it changes the colors randomly but it is not visible in the game
                for (int j = 0; j < num; j++)
                {
                    Debug.Log("I'm doing it");
                    float rand = Random.Range(0f, 1f);
                    parts[j].startColor = particles.customData.GetColor(0).Evaluate(rand);
                }
                Debug.Log("I can change the system!");
                activeSystems[i].particleSystem.GetComponent<ParticleSystem>().SetParticles(parts, parts.Length);
                activeSystems[i].colorsSet = true;
            }
        }
    }
}
