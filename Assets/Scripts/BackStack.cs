using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BackStack : MonoBehaviour
{
    [SerializeField] InputActionReference backAction;
    public UnityEvent OnCancel;

    static readonly Stack<Action> Stack = new();

    public static void Push(Action onBack) => Stack.Push(onBack);
    public static void Remove(Action onBack)
    {
        var tmp = new Stack<Action>();
        while (Stack.Count > 0)
        {
            var a = Stack.Pop();
            if (a != onBack) tmp.Push(a);
        }
        while (tmp.Count > 0) Stack.Push(tmp.Pop());
    }

    public static bool TryBack()
    {
        print("try back");
        if (Stack.Count == 0) return false;
        Stack.Pop().Invoke();
        return true;
    }

    void Awake()
    {
        OnCancel.AddListener(() => TryBack());
    }

    void OnEnable()
    {
        var inputAction = backAction.action;
        inputAction.Enable();
        inputAction.performed += OnBackPerformed;
    }

    void OnDisable()
    {
        var inputAction = backAction.action;
        inputAction.performed -= OnBackPerformed;
        inputAction.Disable();
    }

    void OnBackPerformed(InputAction.CallbackContext ctx)
    {
        OnCancel.Invoke();
    }
}
