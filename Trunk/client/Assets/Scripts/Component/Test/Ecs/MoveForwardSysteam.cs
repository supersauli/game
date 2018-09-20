using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

public class MoveForwardSysteam : JobComponentSystem
{
    struct PositionGroup
    {
        public ComponentDataArray<Position> _position;
        public readonly int Length;
    }

    [Inject] PositionGroup _positionGroup;
      protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    var oldPosition = enemies._position[i];
        //    //if (oldPosition.Value.x < 400)
        //    {
        //        enemies._position[i] = new Position
        //        {
        //            Value = new float3(oldPosition.Value.x + 1, oldPosition.Value.y, oldPosition.Value.z)
        //        };
        //    }
        //}
        var movePositon = new MovePosition {_position = _positionGroup._position};
       return movePositon.Schedule(_positionGroup.Length,64,inputDeps);
    }

    [BurstCompile]
    struct MovePosition:IJobParallelFor
    {
        public ComponentDataArray<Position> _position;
        public void Execute(int index)
        {
            var pos = _position[index];
            _position[index] = new Position
            {
                Value = new float3(pos.Value.x + 1,0,pos.Value.z)
            };
        }
    }






}