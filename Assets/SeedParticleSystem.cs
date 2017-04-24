using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedParticleSystem : MonoBehaviour {

    float particleTimer = 0;
    ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;
    System.Random rngNumber;

    int seedSpawn = 0;
    public int maxSeedSpawn = 70;
    public int SpawnRate = 20;
    //GameObject ob;
    // Use this for initialization
    void Start () {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    private void LateUpdate()
    {
        if (particles == null || particles.Length < particleSystem.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        }
        int count = particleSystem.GetParticles(particles);

        particleTimer += Time.fixedDeltaTime;
        if(particleTimer > 3.0f)
        {
            int rngSeed = Guid.NewGuid().GetHashCode();
            rngNumber = new System.Random(rngSeed);
            for (int i = 0; i < count; i++)
            {
                GameObject ob;
                if((rngNumber.Next(0,100) < SpawnRate) && seedSpawn <= maxSeedSpawn)
                {
                    seedSpawn++;
                    ob = GameObject.Instantiate(Resources.Load("zarodek") as GameObject, particles[i].position, Quaternion.identity);
                    ob.GetComponent<Rigidbody>().velocity = particles[i].velocity;
                }
            }
            particleTimer = 0;
        }
        particleSystem.SetParticles(particles, count);
        
    }

    public int GetSpawnAmount()
    {
        return seedSpawn;
    }

    public bool IsDone()
    {
        if (seedSpawn <= maxSeedSpawn)
            return false;
        return true;
    }
}
