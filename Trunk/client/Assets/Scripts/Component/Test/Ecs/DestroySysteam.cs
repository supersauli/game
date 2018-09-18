
using Unity.Collections;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
[UpdateBefore(typeof(MoveForwardSysteam))]
//public class DestroySysteam : JobComponentSystem
public class DestroySysteam : ComponentSystem
{
    struct DestroyGroup
    {
        [ReadOnly] public ComponentDataArray<Position> _postion;
        [ReadOnly] public ComponentDataArray<SelfEntity> _selfEntity;
        public readonly int Length;
    }
    [Inject] DestroyGroup _destroyGroup;

    //struct OtherGroup
    //{
    //    ......
    //}
    //[Inject] OtherGroup _otherGroup

    protected override void OnUpdate()
    {
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        for (int i = 0; i < _destroyGroup.Length; i++)
        {
            if (_destroyGroup._postion[i].Value.x > 300)
            {
                entityManager.DestroyEntity(_destroyGroup._selfEntity[i]._entity);
            }
        }


    }


    //protected override JobHandle OnUpdate(JobHandle inputDeps)
    //{
    //    int boidCount = _destroyGropu.Length;
    //    var position = new NativeArray<Position>(boidCount, Unity.Collections.Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
    //    var selfEntity = new NativeArray<SelfEntity>(boidCount, Unity.Collections.Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

    //    var job = new Destroy
    //    {
    //       _postion = position,
    //        _selfEntity = selfEntity
    //    };
    //    return job.Schedule(boidCount, 64, inputDeps);
    //}


    //[BurstCompile]
    //struct Destroy : IJobParallelFor
    //{
    //   [ReadOnly] public NativeArray<Position> _postion;
    //   [ReadOnly] public NativeArray<SelfEntity> _selfEntity;

    //    public void Execute(int index)
    //    {
    //        var entityManager = World.Active.GetOrCreateManager<EntityManager>();

    //            var postion = _postion[index];
    //            if (postion.Value.x > 120)
    //            {
    //                entityManager.DestroyEntity(_selfEntity[index]._entity);
    //            }
    //        }
    //    }

};