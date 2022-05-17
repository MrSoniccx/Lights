using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSpawner : MonoBehaviour

{


public int numberOfColumns;
public float speed;
public Sprite texture;
public Color color;
public float lifetime;
public float firerate;
public float size;
public float angle;
public Material material;
public float spin_speed;
private float time;
    
    
    public ParticleSystem system;

    private void Awake()
    {
        Summon();
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, time * spin_speed);
    }


    void Summon()
    {

        angle = 360f / numberOfColumns;


        for (int i = 0; i < numberOfColumns; i++)
        {
        // A simple particle material with no texture.
        Material particleMaterial = material;

        // Create a green Particle System.
        var go = new GameObject("Particle System");
        go.transform.Rotate(angle * i, 90, 0); // Rotate so the system emits upwards.
        go.transform.parent = this.transform;
        go.transform.position = this.transform.position;
        system = go.AddComponent<ParticleSystem>();
        system.Stop();
        go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
        var mainModule = system.main;
        mainModule.startColor = Color.green;
        mainModule.startSize = 0.5f;
        mainModule.startSpeed = speed;
        mainModule.maxParticles = 10000;
        mainModule.duration = 0f;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

        var emission = system.emission;
        emission.enabled = false;

        var form = system.shape;
        form.enabled = true;
        form.shapeType = ParticleSystemShapeType.Sprite;
        form.sprite = null;

        var text = system.textureSheetAnimation;
        text.mode = ParticleSystemAnimationMode.Sprites;
        text.AddSprite(texture);
        }
        
        // Every 2 secs we will emit.
        InvokeRepeating("DoEmit", 2.0f, 2.0f);
    }

    void DoEmit()
    {
        foreach(Transform child in transform)
        {

            system = child.GetComponent<ParticleSystem>();
        // Any parameters we assign in emitParams will override the current system's when we call Emit.
        // Here we will override the start color and size.
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.startColor = color;
        emitParams.startSize = size;
        emitParams.startLifetime = lifetime;
        system.Emit(emitParams, 10);
        system.Play(); // Continue normal emissions
        }
    }
}
