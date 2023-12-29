using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillInfoData
{
    [field: SerializeField] public string SkillName { get; private set; }
    [field: SerializeField] [field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField] [field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField] [field: Range(0f, 3f)] public float Dealing_End_TransitionTime { get; private set; }
    [field: SerializeField] [field: Range(-10f, 10f)] public float Force { get; private set; }

    [field: SerializeField] public int SkillDamage { get; private set; }
    [field: SerializeField] public float SkillCoolTime { get; private set; }
}
