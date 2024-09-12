using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct EnemyMoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {       
        foreach (var (movement, transform) in SystemAPI.Query<RefRW<EnemyMovement>, RefRW<LocalTransform>>())
        {

            float3 currentPosition = transform.ValueRO.Position;
            float2 direction = movement.ValueRO.Direction;
            float speed = movement.ValueRO.Speed;

            float deltaTime = SystemAPI.Time.DeltaTime;
            float3 newPosition = currentPosition + new float3(direction.x, direction.y, 0) * speed * deltaTime;

            transform.ValueRW.Position = newPosition;
        }
    }
}
