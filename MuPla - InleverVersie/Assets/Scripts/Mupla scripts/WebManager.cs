using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Net;

#region Manager
public class WebManager
{
    List<IWebHandler> webHandlers = new List<IWebHandler>()
    {
        new HttpHandler(),
        new MQTTHandler()
    };

    public void Init(MonoBehaviour monoBehaviour)
    {
        foreach (var handler in webHandlers)
        {
            if (handler.TryConnect())
            {
                handler.Init(monoBehaviour);
                Debug.Log(handler + " connected \n");
                break;
            }
            else
            {
                Debug.Log(handler + " could not connect \n");
            }
        }
    }
}

#endregion

#region Interface

public interface IWebHandler
{
    bool TryConnect();
    void Init(MonoBehaviour monoBehaviour);
}

#endregion

#region HTTP

public class HttpHandler : IWebHandler
{
    private string ip = "http://192.168.1.71/";

    public bool TryConnect()
    {
        WebRequest request = WebRequest.Create("http://192.168.1.71/");
        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode == HttpStatusCode.OK;
        }
        catch(Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public void Init(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(ReadButtonOne());
        monoBehaviour.StartCoroutine(ReadButtonTwo());
    }

    IEnumerator ReadButtonOne()
    {
        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get(ip + "buttonOne");
            yield return www.SendWebRequest();

            if (!(www.isNetworkError || www.isHttpError))
            {
                ArduinoInputs.SetButton("Fire", (www.downloadHandler.text == "true"));
            }
            yield return null;
        }
    }

    IEnumerator ReadButtonTwo()
    {
        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get(ip + "buttonTwo");
            yield return www.SendWebRequest();

            if (!(www.isNetworkError || www.isHttpError))
            {
                ArduinoInputs.SetButton("Jump", (www.downloadHandler.text == "true"));
            }
            yield return null;
        }
    }
}

#endregion

#region MQTT
public class MQTTHandler : IWebHandler
{
    private static MqttClient client;
    private static string username = "hjttovbp";
    private static string password = "A74rU0MJLZ4x";

    public virtual bool TryConnect()
    {
        try
        {
            MqttClient testClient = new MqttClient("m15.cloudmqtt.com", 11979, false, null);
            int i = testClient.Connect(Guid.NewGuid().ToString(), username, password);
            return i == 0;
        }
        catch
        {
            return false;
        }
        
    }

    // Use this for initialization
    public void Init(MonoBehaviour monoBehaviour)
    {
        // create client instance 
        client = new MqttClient("m15.cloudmqtt.com", 11979, false, null);

        // register to message received 
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId, username, password);

        client.Subscribe(new string[] { "buttonOne", "buttonTwo" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    //Function is called when a message is recieved
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = System.Text.Encoding.UTF8.GetString(e.Message);
        switch (e.Topic)
        {
            case "buttonOne":
                ArduinoInputs.SetButton("Fire", (message == "true"));
                break;
            case "buttonTwo":
                ArduinoInputs.SetButton("Jump", (message == "true"));
                break;
            default:
                Debug.Log(message);
                break;
        }
    }
}
#endregion
