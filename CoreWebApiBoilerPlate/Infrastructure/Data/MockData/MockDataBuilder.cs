using CoreWebApiBoilerPlate.Entity;
using NETCore.Encrypt.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Data.MockData
{

    /// <summary>
    /// Custom json converter just to hash the password from the JSON data
    /// </summary>
    public class PasswordConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType==typeof(string);
        }

        public override bool CanRead
        {
            get { return true; }
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType==typeof(string) && reader.Value!=null && reader.Path.ToUpper().EndsWith("PASSWORD")) 
            { 

                return reader.Value.ToString().SHA1();
            }
            if(reader.Value!=null && reader.Path.ToUpper().EndsWith("MOBILE"))
            {
                return reader.Value.ToString();
            }
            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Not required because it is just for deserializing.");
        }
    }

    public interface IMockDataGenerator<T>
    {
        List<T> GenerateMockData();
    }

    public abstract class MockDataGeneratorAbstract
    {
        protected readonly string mockDataJsonFilePath;

        public MockDataGeneratorAbstract(string mockDataJsonFileName)
        {
            this.mockDataJsonFilePath = Path.Combine(Environment.CurrentDirectory, "Infrastructure/Data/MockData", mockDataJsonFileName);
        }

        protected List<T> JsonToList<T>(JsonConverter jsonConverter = null)
        {
            var listData = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(mockDataJsonFilePath), jsonConverter);
            return listData;
        }
    }

    public class UserMockDataGenerator : MockDataGeneratorAbstract, IMockDataGenerator<User>
    {
       
        public UserMockDataGenerator() : base("MOCK_DATA_USERS.json")
        {
        }
        public List<User> GenerateMockData()
        {
            
            var users = JsonToList<User>(new PasswordConverter());
            int i = 1;
            users.ForEach(x =>
            {
                x.UserId = i++;
            });
            return users;
        }
    }

    public class PostsMockDataGenerator : MockDataGeneratorAbstract, IMockDataGenerator<Post>
    {

        public PostsMockDataGenerator() : base("MOCK_DATA_POST.json")
        {
        }
        public List<Post> GenerateMockData()
        {
            var posts = JsonToList<Post>(new PasswordConverter());
            return posts;
        }
    }

    public static class MockDataBuilder
    {
        public static IMockDataGenerator<T> Build<T>()
        {
            switch (typeof(T).Name)
            {
                case nameof(User):
                    return new UserMockDataGenerator() as IMockDataGenerator<T>;
                case nameof(Post):
                    return new PostsMockDataGenerator() as IMockDataGenerator<T>;
                default:
                    throw new NotImplementedException("Data generator for this type is not implemented!");
            }
        }
    }
}
