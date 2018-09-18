
using System;
using Unity.Entities;
// 存储entity
[Serializable]
public struct EnemyTag : IComponentData
{
   

}
///只有添加这个才能将当前脚本挂到prefab上
public class EnemyTagComponent : ComponentDataWrapper<EnemyTag>
{
}

