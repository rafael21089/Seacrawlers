using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ability1Display : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject abilityCanvas;
    public static Ability1[] equipedAbilities = new Ability1[3];
    public static Ability1[] equipedAbilitiesBuff = new Ability1[3];
    //public static List<Ability1> equipedAbilities = new List<Ability1>(3);

    public Ability1 ability;

    public Text nameText;
    public Text descriptionText;

    public Image img;
    public Image bgImg;

    public Text levelRequiredText;
    public Text priceText;

    public Button priceBtn;
    public Button equipBtn;

    PlayerRewards pR;

    public Image img1;
    public Image img2;
    public Image img3;

    public GameObject panel;
    public GameObject dropdown;

    bool alreadyEquiped = false;
    static int countEquiped = 0;
    static int countEquipedBuff = 0;
    void Start()
    {
        pR = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRewards>();
        abilityCanvas = GameObject.FindGameObjectWithTag("CanvasA");

        nameText.text = ability.abilityName;
        descriptionText.text = ability.abilityDescription;
        img.sprite = ability.abilityImg;
        bgImg.sprite = ability.abilityBgImg;
        ChangeTextColor();
        levelRequiredText.text = ability.abilityLevelRequired.ToString();
        priceText.text = ability.abilityPrice.ToString();

        if (!(pR.playerCurrentLevel >= ability.abilityLevelRequired))
        {
            this.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        priceBtn.onClick.AddListener(BuyItem);

    }

    
    void BuyItem()
    {
        if (pR.playerCurrentGold >= ability.abilityPrice && pR.playerCurrentLevel >= ability.abilityLevelRequired)
        {
            pR.playerCurrentGold -= ability.abilityPrice;
            pR.goldText.text = pR.playerCurrentGold.ToString();

            priceBtn.gameObject.SetActive(false);
        }
    }

    void ChangeTextColor()
    {
        if (pR.playerCurrentLevel >= ability.abilityLevelRequired)
            levelRequiredText.color = Color.green;
        else
            levelRequiredText.color = Color.red;
    }

    void RefreshAbilitiesCanvas(Ability1 ability1)
    {

        if (ability1.skillType == Ability1.SkillType.Damage)
        {
            if (abilityCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Image>().sprite == null)
            {
                abilityCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ability1.abilityImg;
                abilityCanvas.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = ability1.abilityImg;
            }
            else if (abilityCanvas.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite == null)
            {
                abilityCanvas.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ability1.abilityImg;
                abilityCanvas.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = ability1.abilityImg;
            }
            else if (abilityCanvas.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Image>().sprite == null)
            {
                abilityCanvas.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ability1.abilityImg;
                abilityCanvas.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = ability1.abilityImg;
            }
        }
        else
        {
            if (abilityCanvas.transform.GetChild(3).transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Image>().sprite == null)
            {
                abilityCanvas.transform.GetChild(3).transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ability1.abilityImg;
                abilityCanvas.transform.GetChild(3).transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = ability1.abilityImg;
            }
            else if (abilityCanvas.transform.GetChild(3).transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite == null)
            {
                abilityCanvas.transform.GetChild(3).transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ability1.abilityImg;
                abilityCanvas.transform.GetChild(3).transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = ability1.abilityImg;
            }
            else if (abilityCanvas.transform.GetChild(3).transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Image>().sprite == null)
            {
                abilityCanvas.transform.GetChild(3).transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ability1.abilityImg;
                abilityCanvas.transform.GetChild(3).transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = ability1.abilityImg;
            }
        }
       
       
    }

    void RemoveAbility(Ability1 ability1)
    {

        if (ability1.skillType == Ability1.SkillType.Damage)
        {
            for (int i = 0; i < equipedAbilities.Length; i++)
            {
                if (equipedAbilities[i] == ability1)
                {
                    equipedAbilities[i].isAbilityEquiped = false;

                    if (i == 0)
                    {
                        abilityCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
                        abilityCanvas.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = null;
                    }
                    else if (i == 1)
                    {
                        abilityCanvas.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
                        abilityCanvas.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = null;
                    }
                    else if (i == 2)
                    {
                        abilityCanvas.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
                        abilityCanvas.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = null;
                    }

                    equipedAbilities[i] = null;

                    countEquiped -= 1;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < equipedAbilitiesBuff.Length; i++)
            {
                if (equipedAbilitiesBuff[i] == ability1)
                {
                    equipedAbilitiesBuff[i].isAbilityEquiped = false;

                    Debug.Log("i"+i);
                    if (i == 0)
                    {
                        abilityCanvas.transform.GetChild(3).transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
                        abilityCanvas.transform.GetChild(3).transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = null;
                    }
                    else if (i == 1)
                    {
                        abilityCanvas.transform.GetChild(3).transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
                        abilityCanvas.transform.GetChild(3).transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = null;
                    }
                    else if (i == 2)
                    {

                        abilityCanvas.transform.GetChild(3).transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
                        abilityCanvas.transform.GetChild(3).transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = null;
                    }

                    equipedAbilitiesBuff[i] = null;

                    countEquipedBuff -= 1;
                    break;
                }
            }
        }
        


    }

    public void EquipItem(Ability1 ability1)
    {

        if (ability1.skillType == Ability1.SkillType.Damage)
        {
            if (equipedAbilities[0] == null)
            {

                equipedAbilities[0] = ability1;
                RefreshAbilitiesCanvas(ability1);
                ability.isAbilityEquiped = true;
                countEquiped += 1;
            }
            else if (equipedAbilities[1] == null)
            {

                equipedAbilities[1] = ability1;
                RefreshAbilitiesCanvas(ability1);
                ability.isAbilityEquiped = true;
                countEquiped += 1;
            }
            else if (equipedAbilities[2] == null)
            {

                equipedAbilities[2] = ability1;
                RefreshAbilitiesCanvas(ability1);
                ability.isAbilityEquiped = true;
                countEquiped += 1;
            }
        }
        else
        {
            if (equipedAbilitiesBuff[0] == null)
            {

                equipedAbilitiesBuff[0] = ability1;
                RefreshAbilitiesCanvas(ability1);
                ability.isAbilityEquiped = true;
                countEquipedBuff += 1;
            }
            else if (equipedAbilitiesBuff[1] == null)
            {

                equipedAbilitiesBuff[1] = ability1;
                RefreshAbilitiesCanvas(ability1);
                ability.isAbilityEquiped = true;
                countEquipedBuff += 1;
            }
            else if (equipedAbilitiesBuff[2] == null)
            {

                equipedAbilitiesBuff[2] = ability1;
                RefreshAbilitiesCanvas(ability1);
                ability.isAbilityEquiped = true;
                countEquipedBuff += 1;
            }
        }

      
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!this.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
            this.transform.GetChild(7).gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!this.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.activeInHierarchy && !ability.isAbilityEquiped)
            this.transform.GetChild(7).gameObject.SetActive(true);
    }

    public void Selecionar()
    {

        if (ability.skillType == Ability1.SkillType.Damage)
        {
            if ((!priceBtn.gameObject.activeInHierarchy) && (!alreadyEquiped) && (countEquiped < 3))
            {
                EquipItem(ability);
                alreadyEquiped = true;
            }
            else if (!priceBtn.gameObject.activeInHierarchy && alreadyEquiped)
            {
                RemoveAbility(ability);
                alreadyEquiped = false;
            }
        }
        else
        {
            if ((!priceBtn.gameObject.activeInHierarchy) && (!alreadyEquiped) && (countEquipedBuff < 3))
            {
                EquipItem(ability);
                alreadyEquiped = true;
            }
            else if (!priceBtn.gameObject.activeInHierarchy && alreadyEquiped)
            {
                Debug.Log("s");
                RemoveAbility(ability);
                alreadyEquiped = false;
            }
        }
        


    }
}
