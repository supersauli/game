﻿using Entitas;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

public class EcsSysteam : MonoBehaviour {

    [Header("Mesh")]
    public Mesh blockMesh;
    [Header("block type")]
    public Material blockMaterial;

    public GameObject _prefab;
    // Use this for initialization
    private EntityArchetype _blockArchetype; 
	void Start () {
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        _blockArchetype = entityManager.CreateArchetype(
            typeof(Position)
            );
        var entities = entityManager.CreateEntity(_blockArchetype);
        entityManager.SetComponentData(entities,new Position { Value = new int3(1,1,1)}); ;
        entityManager.AddSharedComponentData(entities,
            new MeshInstanceRenderer {
                mesh = blockMesh,
                material = blockMaterial,
            });

	    var entityArray = new NativeArray<Unity.Entities.Entity>(10000,Allocator.Temp);
	    entityManager.Instantiate(_prefab,entityArray);
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                entityManager.SetComponentData(entityArray[i * j], new Position
                {
                    Value = new float3(i, 0, j)
                });
            }
        }
        //  entityArray.Dispose();


    }

    // Update is called once per frame
    void Update () {
		
	}
}