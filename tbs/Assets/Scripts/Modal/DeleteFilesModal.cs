using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeleteFilesModal : MonoBehaviour
{

    private ModalPanel modalPanel;
    private DisplayManager displayManager;

    private UnityAction yesAction;
    private UnityAction noAction;

    void Start()
    {
        modalPanel = ModalPanel.Instance();
    }

    public void SendModalToView()
    {
        Start();
        modalPanel.Choice("Al eliminar los datos del juego, perderás el progreso obtenido. ¿Estás seguro de proceder?");
    }



}
