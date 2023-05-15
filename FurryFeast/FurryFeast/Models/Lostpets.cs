using Newtonsoft.Json;

namespace FurryFeast.Models
{
    public class Lostpets
    {
        [JsonProperty("晶片號碼")]
        public string ChipNumber { get; set; }

        [JsonProperty("寵物名")]
        public string PetName { get; set; }

        [JsonProperty("寵物別")]
        public string PetType { get; set; }

        [JsonProperty("性別")]
        public string Gender { get; set; }

        [JsonProperty("品種")]
        public string Breed { get; set; }

        [JsonProperty("毛色")]
        public string Color { get; set; }

        [JsonProperty("遺失時間")]
        public string LostTime { get; set; }

        [JsonProperty("遺失地點")]
        public string LostLocation { get; set; }

        [JsonProperty("飼主姓名")]
        public string OwnerName { get; set; }

        [JsonProperty("連絡電話")]
        public string ContactPhone { get; set; }

        [JsonProperty("EMail")]
        public string Email { get; set; }

        [JsonProperty("PICTURE")]
        public string Picture { get; set; }
    }
}
