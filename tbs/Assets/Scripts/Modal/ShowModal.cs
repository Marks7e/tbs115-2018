using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShowModal : MonoBehaviour
{
    private ModalPanel _modalPanel;
    private NoPointsForLevelModal _noPointsModal;
    private DisplayManager _displayManager;

    private UnityAction _yesAction;
    private UnityAction _noAction;

    public void InstanceDeleteModal()
    {
        _modalPanel = ModalPanel.Instance();
    }
    public void InstanceNoPointsModal()
    {
        _noPointsModal = NoPointsForLevelModal.Instance();
    }
    public void SendDeleteModalToView()
    {
        InstanceDeleteModal();
        _modalPanel.Choice("Al eliminar los datos del juego, perderás el progreso obtenido. ¿Estás seguro de proceder?");
    }
    public void SendNoPointsForLevelModalToView()
    {
        InstanceNoPointsModal();
        _noPointsModal.Choice("Necesitas mayor puntaje para desbloquear este nivel.");
    }
}
