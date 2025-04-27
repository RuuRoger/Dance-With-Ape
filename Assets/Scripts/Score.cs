using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _bannanaScore;

    private int _scoreNumber;

    private void Awake()
    {
        _scoreNumber = int.Parse(_bannanaScore.text);
    }

    //AHORA TENGO QUE CREAR UN EVENTO PARA QUE AVISE A ESTE SCRIPT DE QUE TIENE QUE UMENTAR EL VALOR EN 1
    //TAMBIEN HAY QUE HACER QUE SE ACTIVE Y DESACTIVE

}
