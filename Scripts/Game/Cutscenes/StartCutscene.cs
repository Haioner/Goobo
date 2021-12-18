using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class StartCutscene : MonoBehaviour
{
    [SerializeField]private Animator _animatorJogador;
    public Animator playerAnimator;
    private PlayableDirector _director;
    private RuntimeAnimatorController _controlador;
    private bool _Consertado;

    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        if (Manager.Instance.checkpoint == 0)
        {
            playerAnimator.enabled = false;
            _director.enabled = true;
        }
    }

    private void OnEnable()
    {
        _controlador = _animatorJogador.runtimeAnimatorController;
        _animatorJogador.runtimeAnimatorController = null;
    }

    private void Update()
    {
        if(_director.state != PlayState.Playing && !_Consertado)
        {
            _animatorJogador.runtimeAnimatorController = _controlador;
            playerAnimator.enabled = true;
            _Consertado = true;
        }
    }
}
