using System.Text.Json;

namespace HangMan.HttpServices;

public class RandomWordHttpClient {
    public async Task<string?> GetRandomWordAsync() {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage = await client.GetAsync(
            "http://api.wordnik.com:80/v4/words.json/randomWords?hasDictionaryDef=true&minCorpusCount=0&minLength=5&maxLength=15&limit=1&api_key=a2a73e7b926c924fad7001ca3111acd55af2ffabf50eb4ae5");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error : {responseMessage.StatusCode} , {content}");
        }
        //Console.WriteLine(content);

        List<WordObj> deserialize = JsonSerializer.Deserialize<List<WordObj>>(content, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        })!;
        return deserialize[0].Word;
    }
}