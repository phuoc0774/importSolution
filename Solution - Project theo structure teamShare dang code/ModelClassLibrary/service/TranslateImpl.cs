using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ModelClassLibrary.service
{
    public interface ITranslate
    {
        Item LoadJson(string lang);
        ItemLogin loadJsonLogin(string lang);
    }

    public class TranslateImpl : ITranslate
    {
        public Item LoadJson(string lang)
        {
            if (lang == null || lang == "")
            {
                lang = "vn";
            }
            using (StreamReader r = new StreamReader("lang/" + lang + ".json"))
            {
                string json = r.ReadToEnd();
                Item items = JsonConvert.DeserializeObject<Item>(json);
                return items;
            }
        }
        public ItemLogin loadJsonLogin(string lang)
        {
            if (lang == null || lang == "")
            {
                lang = "vn";
            }
            using (StreamReader r = new StreamReader("lang/" + lang + ".json"))
            {
                string json = r.ReadToEnd();
                ItemLogin items = JsonConvert.DeserializeObject<ItemLogin>(json);
                return items;
            }
        }
    }
    public class Item
    {
        //user service
        public string error;
        public string success;
        public string createfirstuser;
        public string usercraeted;
        public string userexist;
        public string usernotexist;
        public string passwordnotcorrect;
    }
    public class ItemLogin
    {
        public string usernotexist;
        public string loginsuccess;
        public string passwordnotcorrect;
    }
}
