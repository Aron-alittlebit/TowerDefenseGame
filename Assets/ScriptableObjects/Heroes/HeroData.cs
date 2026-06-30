using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

[CreateAssetMenu(fileName = "HeroData", menuName = "Scriptable Objects/HeroData")]
public class HeroData : ScriptableObject
{
    [SerializeField] private List<TowerData> towers;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private RuntimeAnimatorController animatorController;
    [SerializeField] private GameObject modelPrefab;

    public List<TowerData> Towers => towers;
    public int Health => health;
    public float Speed => speed;
    public RuntimeAnimatorController AnimatorController => animatorController;
    public GameObject ModelPrefab => modelPrefab;

    public int CurrentHealth { get; private set; }
    public void SetHealth(int health)
    {
        CurrentHealth = health;
    }
    private void OnEnable()
    {
        CurrentHealth = health;
    }


}
