using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEditor;

public class RotateSysteam : JobComponentSystem
{

    struct RotateGroup
    {
        public ComponentDataArray<Rotation> _rotate;
        public readonly int Length;
    }
    //protected override void OnCreateManager()
    //{
    //    base.OnCreateManager();
    //}
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var rotateJob = new RotateJob
        {
            _rotation = _rotateGroup._rotate
        };
        return rotateJob.Schedule(_rotateGroup.Length,64,inputDeps);
    }


    [Inject] RotateGroup _rotateGroup;

    [BurstCompile]
    struct RotateJob : IJobParallelFor
    {
        private static int global = 0;
        public ComponentDataArray<Rotation> _rotation;
        public void Execute(int index)
        {
            //var rotation= _rotation[index];

            //_rotation[index] = new Unity.Transforms.Rotation
            //{ Value = new quaternion(0, rotation.Value.value.y*10+1, 0.0f, 0)};
        }
    }



}