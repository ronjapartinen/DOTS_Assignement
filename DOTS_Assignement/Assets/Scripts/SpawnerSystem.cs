using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct SpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }
    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state)
    {
        float spawnRadius = 10;

        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if(spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Debug.Log("Spawn");

                float randomAngle = UnityEngine.Random.Range(0, math.PI * 2);

                float2 spawnPosition = new float2(
                   math.cos(randomAngle) * spawnRadius,
                   math.sin(randomAngle) * spawnRadius
               );

                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);

                ecb.SetComponent(newEntity, LocalTransform.FromPosition(new float3(spawnPosition.x, spawnPosition.y, 0)));
                float2 direction = math.normalize(-spawnPosition);
                float randomSpeed = UnityEngine.Random.Range(1, 5);           

                ecb.AddComponent(newEntity, new EnemyMovement
                {
                    Direction = direction,
                    Speed = randomSpeed
                });

                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
