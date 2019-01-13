using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServiceManager
{
    bool GetService<T>(out T service) where T : class;
}

public class ServiceManager : IServiceManager
{
    private static ServiceManager instance;
    public static IServiceManager GetServiceManager
    {
        get
        {
            if (instance == null)
            {
                instance = new ServiceManager();
                instance.Instantiate();
            }
            return instance;
        }
    }

    private Dictionary<System.Type, object> services = new Dictionary<System.Type, object>();

    private void Instantiate()
    {
        services.Add(typeof(IInput), InputManager.CreateInput());
    }

    public bool GetService<T>(out T service) where T : class
    {
        if (services.ContainsKey(typeof(T)))
        {
            service = (T)services[typeof(T)];
            return true;
        }
        else
        {
            service = null;
            return false;
        }
    }
}
