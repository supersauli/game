
using Unity.Collections;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
//[UpdateBefore(typeof(MoveForwardSysteam))]
public class DestroySysteam :JobComponentSystem
//public class DestroySysteam : ComponentSystem
{

    struct DestroyGroup
    {
        [ReadOnly] public ComponentDataArray<Position> _position;
        [ReadOnly] public ComponentDataArray<SelfEntity> _selfEntity;
        public readonly int Length;
    }
    [Inject] DestroyGroup _destroyGroup;

    //struct OtherGroup
    //{
    //    ......
    //}
    //[Inject] OtherGroup _otherGroup

    //protected override void OnUpdate()
    //{
    //    var entityManager = World.Active.GetOrCreateManager<EntityManager>();
    //    for (int i = 0; i < _destroyGroup.Length; i++)
    //    {
    //        if (_destroyGroup._postion[i].Value.x > 300)
    //        {
    //            entityManager.DestroyEntity(_destroyGroup._selfEntity[i]._entity);
    //        }
    //    }


    //}
    public class RemoveDeadBarrier : BarrierSystem
    {
    }
    [Inject] private RemoveDeadBarrier m_RemoveDeadBarrier;
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        int boidCount = _destroyGroup.Length;
        //var position = new NativeArray<Position>(boidCount, Unity.Collections.Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        //var selfEntity = new NativeArray<SelfEntity>(boidCount, Unity.Collections.Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
       
        var job = new Destroy
        {
            _position = _destroyGroup._position,
            _selfEntity = _destroyGroup._selfEntity,
            _commands = m_RemoveDeadBarrier.CreateCommandBuffer()
        };
        return job.Schedule(boidCount, 64, inputDeps);
    }
   

    [BurstCompile]
    struct Destroy : IJobParallelFor
    {
        //  [ReadOnly]public NativeArray<Position> _position;
        //[ReadOnly] public NativeArray<SelfEntity> _selfEntity;
        [ReadOnly] public ComponentDataArray<Position> _position;
        [ReadOnly] public ComponentDataArray<SelfEntity> _selfEntity;
        [ReadOnly] public EntityCommandBuffer _commands;
        private GameObjectArray gameObjects;
       
        public void Execute(int index)
        {
            
            var postion = _position[index];
            if (postion.Value.x > 120)
               {
                
                _commands.DestroyEntity(_selfEntity[index]._entity);
                //Debug.Log("destroy" + index);
                //_position[index] = new Position
                //{
                //   Value = new float3(postion.Value.x + 1, postion.Value.y, postion.Value.z)
                //};
            }
        }
    }

   

};