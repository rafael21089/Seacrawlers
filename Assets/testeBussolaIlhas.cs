using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testeBussolaIlhas : MonoBehaviour
{
    GameObject player;
    [SerializeField] Image miniMapImgO;
    [SerializeField] Image miniMapImgE;
    [SerializeField] Image miniMapImgN;
    [SerializeField] Image miniMapImgS;
    [SerializeField] Image miniMapImgNE;
    [SerializeField] Image miniMapImgNO;
    [SerializeField] Image miniMapImgSE;
    [SerializeField] Image miniMapImgSO;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void VerificaIlhas(Vector3 ilhaPosicao)
    {
        // determinar em que direção a ilha está em relação à posição do jogador
        float diferencaX = ilhaPosicao.x - player.transform.position.x;
        float diferencaZ = ilhaPosicao.z - player.transform.position.z;
        string direcao = "";

        if (Mathf.Abs(diferencaX) > Mathf.Abs(diferencaZ))
        {
            // ilha está na direção leste ou oeste
            if (diferencaX > 0)
            {
                direcao = "leste";
                miniMapImgE.color = Color.yellow;
            }
            else
            {
                direcao = "oeste";
                miniMapImgO.color = Color.yellow;

            }

            // verificar se a ilha está em uma posição diagonal
            if (Mathf.Abs(diferencaZ) > Mathf.Abs(diferencaX) * 0.5f)
            {
                if (diferencaZ > 0)
                {
                    direcao += "noroeste";
                    miniMapImgNO.color = Color.yellow;
                }
                else
                {
                    direcao += "sudoeste";
                    miniMapImgSO.color = Color.yellow;
                }
            }
        }
        else
        {
            // ilha está na direção norte ou sul
            if (diferencaZ > 0)
            {
                direcao = "norte";
                miniMapImgN.color = Color.yellow;
            }
            else
            {
                direcao = "sul";
                miniMapImgS.color = Color.yellow;
            }

            // verificar se a ilha está em uma posição diagonal
            if (Mathf.Abs(diferencaX) > Mathf.Abs(diferencaZ) * 0.5f)
            {
                if (diferencaX > 0)
                {
                    direcao += "nordeste";
                    miniMapImgNE.color = Color.yellow;
                }
                else
                {
                    direcao += "sudeste";
                    miniMapImgSE.color = Color.yellow;
                }
            }
        }
        Debug.Log("DIRECAO " + direcao);
    }
    void VerificaIlhas2(Vector3 ilhaPosicao)
    {
        // determinar em que direção a ilha está em relação à posição do jogador
        float diferencaX = ilhaPosicao.x - player.transform.position.x;
        float diferencaZ = ilhaPosicao.z - player.transform.position.z;
        string direcao = "";

        if (Mathf.Abs(diferencaX) > Mathf.Abs(diferencaZ))
        {
            // ilha está na direção leste ou oeste
            if (diferencaX > 0)
            {
                direcao = "leste";
                miniMapImgE.color = Color.blue;
            }
            else
            {
                direcao = "oeste";
                miniMapImgO.color = Color.blue;

            }

            // verificar se a ilha está em uma posição diagonal
            if (Mathf.Abs(diferencaZ) > Mathf.Abs(diferencaX) * 0.5f)
            {
                if (diferencaZ > 0)
                {
                    direcao += "noroeste";
                    miniMapImgNO.color = Color.blue;
                }
                else
                {
                    direcao += "sudoeste";
                    miniMapImgSO.color = Color.blue;
                }
            }
        }
        else
        {
            // ilha está na direção norte ou sul
            if (diferencaZ > 0)
            {
                direcao = "norte";
                miniMapImgN.color = Color.blue;
            }
            else
            {
                direcao = "sul";
                miniMapImgS.color = Color.blue;
            }

            // verificar se a ilha está em uma posição diagonal
            if (Mathf.Abs(diferencaX) > Mathf.Abs(diferencaZ) * 0.5f)
            {
                if (diferencaX > 0)
                {
                    direcao += "nordeste";
                    miniMapImgNE.color = Color.blue;
                }
                else
                {
                    direcao += "sudeste";
                    miniMapImgSE.color = Color.blue;
                }
            }
        }
        Debug.Log("DIRECAO " + direcao);
    }

    void VerificaIlhas3(Vector3 ilhaPosicao)
    {
        // determinar em que direção a ilha está em relação à posição do jogador
        float diferencaX = ilhaPosicao.x - player.transform.position.x;
        float diferencaZ = ilhaPosicao.z - player.transform.position.z;
        string direcao = "";

        if (Mathf.Abs(diferencaX) > Mathf.Abs(diferencaZ))
        {
            // ilha está na direção leste ou oeste
            if (diferencaX > 0)
            {
                direcao = "leste";
                miniMapImgE.color = Color.green;
            }
            else
            {
                direcao = "oeste";
                miniMapImgO.color = Color.green;

            }

            // verificar se a ilha está em uma posição diagonal
            if (Mathf.Abs(diferencaZ) > Mathf.Abs(diferencaX) * 0.5f)
            {
                if (diferencaZ > 0)
                {
                    direcao += "noroeste";
                    miniMapImgNO.color = Color.green;
                }
                else
                {
                    direcao += "sudoeste";
                    miniMapImgSO.color = Color.green;
                }
            }
        }
        else
        {
            // ilha está na direção norte ou sul
            if (diferencaZ > 0)
            {
                direcao = "norte";
                miniMapImgN.color = Color.green;
            }
            else
            {
                direcao = "sul";
                miniMapImgS.color = Color.green;
            }

            // verificar se a ilha está em uma posição diagonal
            if (Mathf.Abs(diferencaX) > Mathf.Abs(diferencaZ) * 0.5f)
            {
                if (diferencaX > 0)
                {
                    direcao += "nordeste";
                    miniMapImgNE.color = Color.green;
                }
                else
                {
                    direcao += "sudeste";
                    miniMapImgSE.color = Color.green;
                }
            }
        }
        Debug.Log("DIRECAO " + direcao);
    }

    void VerificaIlhas4(Vector3 ilhaPosicao)
    {
        // determinar em que direção a ilha está em relação à posição do jogador
        float diferencaX = ilhaPosicao.x - player.transform.position.x;
        float diferencaZ = ilhaPosicao.z - player.transform.position.z;
        string direcao = "";

        if (Mathf.Abs(diferencaX) > Mathf.Abs(diferencaZ))
        {
            // ilha está na direção leste ou oeste
            if (diferencaX > 0)
            {
                direcao = "leste";
                miniMapImgE.color = Color.red;
            }
            else
            {
                direcao = "oeste";
                miniMapImgO.color = Color.red;

            }

            // verificar se a ilha está em uma posição diagonal
            if (Mathf.Abs(diferencaZ) > Mathf.Abs(diferencaX) * 0.5f)
            {
                if (diferencaZ > 0)
                {
                    direcao += "noroeste";
                    miniMapImgNO.color = Color.red;
                }
                else
                {
                    direcao += "sudoeste";
                    miniMapImgSO.color = Color.red;
                }
            }
        }
        else
        {
            // ilha está na direção norte ou sul
            if (diferencaZ > 0)
            {
                direcao = "norte";
                miniMapImgN.color = Color.red;
            }
            else
            {
                direcao = "sul";
                miniMapImgS.color = Color.red;
            }

            // verificar se a ilha está em uma posição diagonal
            if (Mathf.Abs(diferencaX) > Mathf.Abs(diferencaZ) * 0.5f)
            {
                if (diferencaX > 0)
                {
                    direcao += "nordeste";
                    miniMapImgNE.color = Color.red;
                }
                else
                {
                    direcao += "sudeste";
                    miniMapImgSE.color = Color.red;
                }
            }
        }
        Debug.Log("DIRECAO " + direcao);
    }

    // Update is called once per frame
    void Update()
    {
        // posição da ilha que você deseja verificar
        Vector3 ilhaPosicao = ObterPosicaoIlha("MiniSandItemSpreaderOverlap(Clone)"); // substitua "Ilha do Deserto" pelo nome da ilha que você deseja verificar
        Vector3 ilhaPosicao2 = ObterPosicaoIlha("MiniIceItemSpreaderOverlap(Clone)"); // substitua "Ilha do Deserto" pelo nome da ilha que você deseja verificar
        Vector3 ilhaPosicao3 = ObterPosicaoIlha("MiniForestItemSpreaderOverlap(Clone)"); // substitua "Ilha do Deserto" pelo nome da ilha que você deseja verificar
        Vector3 ilhaPosicao4 = ObterPosicaoIlha("MiniLavaItemSpreaderOverlap(Clone)"); // substitua "Ilha do Deserto" pelo nome da ilha que você deseja verificar
        VerificaIlhas(ilhaPosicao);
        VerificaIlhas2(ilhaPosicao2);
        VerificaIlhas3(ilhaPosicao3);
        VerificaIlhas4(ilhaPosicao4);



    }

    private Vector3 ObterPosicaoIlha(string nomeIlha)
    {
        GameObject ilha = GameObject.Find(nomeIlha);
        if (ilha != null)
        {
            return ilha.transform.position;
        }
        else
        {
            //Debug.LogError("Ilha não encontrada: " + nomeIlha);
            return Vector3.zero;
        }
    }

    
}
