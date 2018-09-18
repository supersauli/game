using System;
using Unity.Entities;
using UnityEngine;
using UnityEditor;
// 存储entity
[Serializable]
public struct SelfEntity : IComponentData
{
    public Entity _entity;
}
///只有添加这个才能将当前脚本挂到prefab上
public class SelfEntityComponent : ComponentDataWrapper<SelfEntity>
{
}