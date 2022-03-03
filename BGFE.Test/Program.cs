using NvNodeApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

Client.ServerPort = 53906;
Client.SecurityToken = "63F5815B4D1E08B2B8F0D8D76F89D97B";

// Gets the enable/disable state of Instant Replay
// Returns "{"status":true/false}" for enable/disable
using (var response = await Client.Get("/ShadowPlay/v.1.0/InstantReplay/Enable"))
{
    string resStrContent = await response.Content.ReadAsStringAsync();
    Console.WriteLine(resStrContent);


    // 型が不定の場合
    JObject resContent = JObject.Parse(resStrContent);
    bool isIrEnable = (bool)resContent["status"];


    // 型が特定されている場合
    // MyClass resContent = JsonConvert.DeserializeObject<MyClass>(resStrContent);
    // bool isIrEnable = resContent.status;

    // Enables / disables Instant Replay
    // send "{"status":true/false}" for enable/disable in request body
    Console.WriteLine("IR status: " + isIrEnable);
    JObject toggleIrStateReq = new JObject
    {
        ["status"] = !isIrEnable
    };
    Client.Post("/ShadowPlay/v.1.0/InstantReplay/Enable", toggleIrStateReq);
}


/*
JObject openOscReq = new JObject
{
    ["open"] = true
};
Client.Post("/ShadowPlay/v.1.0/OpenOsc", openOscReq);
*/



class MyClass { public bool status { get; set; } }
