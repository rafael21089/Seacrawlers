using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Ability", menuName = "Ability1")]

public class Ability1 : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;
    public string abilityType;
    public string abilityClass;

    public int abilityLevelRequired;
    public int abilityPrice;
    
    public float abilityCooldown;

    public bool isAbilityActive = false;
    public bool isAbilityEquiped = false;

    public Sprite abilityImg;
    public Sprite abilityBgImg;

    public float abilityDuration;
    public int abilityValue;

    // Adicionamos um campo para selecionar a função da classe Abilities
    public string functionName;

    public enum SkillType
    {
        Buff,
        Debuff,
        Damage,
        Movement
    }

    public SkillType skillType;
    private void Awake()
    {
        isAbilityActive = false;
        isAbilityEquiped = false;
    }


}



enum AbilityType
{
    Normal,
    Rare,
    Unique
}

