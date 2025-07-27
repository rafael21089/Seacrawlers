using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShopSpreader : MonoBehaviour
{
    public List<Ability1> Abilities = new List<Ability1>();
    public GameObject player;
    public string classePlayer;
    public GameObject prefab;
    public string checker;
    // Crie uma lista para armazenar as habilidades já instanciadas
    List<Ability1> instantiatedAbilities = new List<Ability1>();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //classePlayer = player.GetComponent<movement>().classeEscolhida.ToString();
        classePlayer = CharacterSelection.selectedClass;
        Debug.Log("class" + classePlayer);
    }

    // Update is called once per frame
    void Update()
    {
        //if (checker != classePlayer)
        {
            
            foreach (Ability1 ability in Abilities)
            {
                if (instantiatedAbilities.Contains(ability))
                {
                    continue; // Habilidade já instanciada, passe para a próxima
                }
                
                if (ability.abilityClass == player.GetComponent<movement>().classeEscolhida.ToString() || ability.abilityType == "Normal")
                {
                    GameObject d = Instantiate(prefab, this.transform);
                    d.GetComponent<Ability1Display>().ability = ability;

                    instantiatedAbilities.Add(ability); // Adicione a habilidade à lista de habilidades instanciadas
                }
            }
        }
        //checker = classePlayer;
    }
}
