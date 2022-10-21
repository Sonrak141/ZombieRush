using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncuentroFinal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Menu;
    [SerializeField] Transform limits;
    Oleada[] oleadas;

    int currentOleada = -1;
    // Start is called before the first frame update

    void Awake()
    {
        oleadas = GetComponentsInChildren<Oleada>();

    }
    void Start()
    {
        foreach (Oleada o in oleadas)
        {
            o.DeactivateEnemys();
        }
        limits.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentOleada < oleadas.Length)
        {
            if ((currentOleada >= 0) && oleadas[currentOleada].AllEnemysDead())
            {
                currentOleada++;
                if (currentOleada < oleadas.Length)
                {
                    oleadas[currentOleada].ActivateEnemys();
                }
                else
                {
                    limits.gameObject.SetActive(false);
                    Menu.SetActive(true);
                    Cursor.visible = true;
                    Time.timeScale = 0;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentOleada < 0)
        {
            currentOleada = 0;
            oleadas[currentOleada].ActivateEnemys();
            limits.gameObject.SetActive(true);
        }
    }
}
