using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Initial Menu")]
    [SerializeField] private Menu defaultMenu;

    [Header("Optional Runtime Prefab Instantiation")]
    [SerializeField] private MenuBootstrapConfig bootstrapConfig;

    [Header("Runtime References")]
    [SerializeField] private List<EventListener> eventListeners = new List<EventListener>();
    [SerializeField] private List<View> views = new List<View>();

    public Menu currentMenu;
    private Menu prevMenu;

    public enum Menu
    {
        Section1,
        Section2,
        Section3,
        Section4,
        Section5,
        Section6,
        None
    }

    public enum ViewType
    {
        Settings
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (bootstrapConfig != null)
        {
            eventListeners = bootstrapConfig.CreateEventListeners(transform);
            views = bootstrapConfig.CreateViews(transform);
        }

        ChangeMenu(defaultMenu);
    }

    public void ChangeMenu(Menu newMenu)
    {
        if (TryGetListener(currentMenu, out var oldListener))
            oldListener.gameObject.SetActive(false);

        prevMenu = currentMenu;
        currentMenu = newMenu;

        if (TryGetListener(currentMenu, out var newListener))
            newListener.gameObject.SetActive(true);
    }

    public void EnableView(ViewType viewType, bool enable)
    {
        if (TryGetView(viewType, out var view))
            view.gameObject.SetActive(enable);
    }

    public void ShowOnlyView(ViewType viewType)
    {
        foreach (var v in views)
            v.gameObject.SetActive(false);

        EnableView(viewType, true);
    }

    public T GetMenu<T>(Menu menu) where T : EventListener
    {
        TryGetListener(menu, out var listener);
        return listener as T;
    }

    public T GetView<T>(ViewType viewType) where T : View
    {
        TryGetView(viewType, out var view);
        return view as T;
    }

    private bool TryGetListener(Menu menu, out EventListener listener)
    {
        listener = eventListeners.Find(el => el.MenuType == menu);
        return listener != null;
    }

    private bool TryGetView(ViewType viewType, out View view)
    {
        view = views.Find(v => v.ViewType == viewType);
        return view != null;
    }
}
