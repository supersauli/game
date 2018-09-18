using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEditor;

public class MoveForwardSysteam : ComponentSystem
{
    struct MoveGroup
    {
        public ComponentDataArray<Position> _position;
        public readonly int Length;
    }

    [Inject] MoveGroup enemies;
    protected override void OnUpdate()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            var oldPosition = enemies._position[i];
            if (oldPosition.Value.x < 400)
            {
                enemies._position[i] = new Position
                {
                    Value = new float3(oldPosition.Value.x + 1, oldPosition.Value.y, oldPosition.Value.z)
                };
            }
        }
         
    }
}