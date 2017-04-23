using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedParticleSystem : MonoBehaviour {

    float particleTimer = 0;
    ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;
    System.Random rngNumber;
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
                if(rngNumber.Next(0,100) < 20)
                    Instantiate(Resources.Load("Seed"), particles[i].position, Quaternion.identity);
            }
            particleTimer = 0;
        }
        particleSystem.SetParticles(particles, count);
        
    }
}
