using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShowModal : MonoBehaviour
{
    private ModalPanel modalPanel;
    private NoPointsForLevelModal noPointsModal;
    private DisplayManager displayManager;

    private UnityAction yesAction;
    private UnityAction noAction;

    public void InstanceDeleteModal()
    {
        modalPanel = ModalPanel.Instance();
    }
    public void InstanceNoPointsModal()
    {
        noPointsModal = NoPointsForLevelModal.Instance();
    }


    public void SendDeleteModalToView()
    {
        InstanceDeleteModal();
        modalPanel.Choice("Al eliminar los datos del juego, perderás el progreso obtenido. ¿Estás seguro de proceder?");
    }
    public void SendNoPointsForLevelModalToView()
    {
        InstanceNoPointsModal();
        noPointsModal.Choice("Necesitas mayor puntaje para desbloquear este nivel.");
    }



}
