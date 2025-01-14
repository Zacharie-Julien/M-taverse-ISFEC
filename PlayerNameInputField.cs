using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;



//Permet d'indiquer à unity l'obligation pour le script, 
//d'être attaché à un composant disposant d'un InputField

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    const string playerNameRefKey = "PlayerName";

    void Start(){

        //Permet de déclaré une chaînes de caractères vide, 
        //équivaux à ""

        string defaultName = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField != null)
        {
            //PlayPrefs permet de stocker de petite variable en locale
            //coté client de manière persistante

            if (PlayerPrefs.HasKey(playerNameRefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNameRefKey);
                _inputField.text = defaultName;
            }
        }


        //Ici on enregistre dans les paramètre photon le NickName de l'utilisateur
        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value){

        if (string.IsNullOrEmpty(value))
        {
            Debug.Log("Player Name is null or empty; ");    
        }


        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNameRefKey, value);
    }
}
