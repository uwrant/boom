using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

public abstract class BoomController : Controller
{
    public IActionResult JsonSerialized(object toSerialize)
    {
        var content = JsonConvert.SerializeObject(toSerialize);
        return this.Content(content);
    }
}