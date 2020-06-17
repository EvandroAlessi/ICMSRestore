using System;
using System.Xml.Serialization;

namespace CrossCutting.Serializable
{
    public class NFeSerialization : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public T GetObjectFromFile<T>(string file) where T : class
        {
            var serialize = new XmlSerializer(typeof(T));

            try
            {
                using (var xml = System.Xml.XmlReader.Create(file))
                    return (T)serialize.Deserialize(xml);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
