using System;
using System.Reflection;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Dx29.Services;

namespace Dx29.Data
{
    public class BlobDb : IDisposable
    {
        static BlobDb()
        {
            Storage = new BlobStorage();
        }

        public BlobDb(string container, string path)
        {
            Container = container;
            Path = path;
            Initialize();
        }

        [JsonIgnore]
        static public BlobStorage Storage { get; }

        [JsonIgnore]
        protected string Container { get; }

        [JsonIgnore]
        protected string Path { get; }

        [JsonIgnore]
        protected Formatting Formatting { get; set; } = Formatting.Indented;

        private void Initialize()
        {
            if (!Deserialize())
            {
                var properties = this.GetType().GetTypeInfo().DeclaredProperties;
                foreach (var property in properties)
                {
                    if (property.PropertyType.GetConstructor(Type.EmptyTypes) != null)
                    {
                        property.SetValue(this, Activator.CreateInstance(property.PropertyType));
                    }
                }
            }
        }

        public void SaveChanges()
        {
            Serialize();
        }
        public async Task SaveChangesAsync()
        {
            await SerializeAsync();
        }

        private void Serialize()
        {
            string json = JsonConvert.SerializeObject(this, Formatting);
            Storage.UploadString(Container, Path, json);
        }
        private async Task SerializeAsync()
        {
            string json = JsonConvert.SerializeObject(this, Formatting);
            await Storage.UploadStringAsync(Container, Path, json);
        }

        private bool Deserialize()
        {
            string json = Storage.DownloadString(Container, Path);
            if (json != null)
            {
                Deserialize(json);
                return true;
            }
            return false;
        }

        protected void Deserialize(string json)
        {
            var jObject = JObject.Parse(json);

            var properties = this.GetType().GetTypeInfo().DeclaredProperties;
            foreach (var property in properties)
            {
                if (jObject.TryGetValue(property.Name, out JToken token))
                {
                    var value = token.ToObject(property.PropertyType);
                    property.SetValue(this, value);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
