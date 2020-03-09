using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System;

public class Synthesizer
{
    private string userId = null;
    private string token = null;
    private string lang = null;

    private string apiURLFile = "http://api.azreco.az/synthesize";
    private string apiURLText = "http://api.azreco.az/synthesize/text";
    private string apiURLVoices = "http://api.azreco.az/voices";

    public Synthesizer(string userId, string token, string lang) {
        this.userId = userId;
        this.token = token;
        this.lang = lang;
    }

    public async Task<byte[]> Synthesize(string textFile, string ttsId) {
        HttpContent idContent = new StringContent(userId);
        HttpContent tokenContent = new StringContent(token); 
        HttpContent langContent = new StringContent(lang);
        FileStream fs = null;
        try 
        {
            fs = File.OpenRead(textFile);
        }
        catch(IOException ex) 
        {
            Console.WriteLine("File IO error: " + ex.Message);
            return null;
        }
        string fileName = Path.GetFileName(textFile);
        HttpContent streamContent = new StreamContent(fs); 

        // Making multipart form content and posting it to the server
        using(var client = new HttpClient())
        using(var formData = new MultipartFormDataContent()) 
        {
            formData.Add(idContent, "api_id");
            formData.Add(tokenContent, "api_token");
            formData.Add(langContent, "lang");
            formData.Add(streamContent, "file", fileName);
            if (!(ttsId == null || ttsId.Length == 0))
            {
                formData.Add(new StringContent(ttsId), "tts_id");
            }
            var response = await client.PostAsync(apiURLFile, formData);
            if(!response.IsSuccessStatusCode) {
				if (response.StatusCode == HttpStatusCode.BadRequest)
				{
					string error = await response.Content.ReadAsStringAsync();
					Console.WriteLine("Text-to-speech process failed: " + error);
				}
				else
				{
					Console.WriteLine(response.ReasonPhrase);
				}
                return null;
            }
            return await response.Content.ReadAsByteArrayAsync();
        }
    }

    public async Task<byte[]> SynthesizeText(string text, string ttsId)
    {
        HttpContent idContent = new StringContent(userId);
        HttpContent tokenContent = new StringContent(token);
        HttpContent langContent = new StringContent(lang);
        HttpContent textContent = new StringContent(text);
        // Making multipart form content and posting it to the server
        using (var client = new HttpClient())
        using (var formData = new MultipartFormDataContent())
        {
            formData.Add(idContent, "api_id");
            formData.Add(tokenContent, "api_token");
            formData.Add(langContent, "lang");
            formData.Add(textContent, "text");
            if(!(ttsId == null || ttsId.Length == 0))
            {
                formData.Add(new StringContent(ttsId), "tts_id");
            }
            var response = await client.PostAsync(apiURLText, formData);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Text-to-speech process failed: " + error);
                }
                else
                {
                    Console.WriteLine(response.ReasonPhrase);
                }
                return null;
            }
            return await response.Content.ReadAsByteArrayAsync();
        }
    }

    public async Task<string> GetVoices()
    {
        using (var client = new HttpClient()) { 
            var response = await client.GetAsync(apiURLVoices + "?api_id=" + userId + "&api_token=" + token);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Getting voices failed: " + error);
                }
                else
                {
                    Console.WriteLine(response.ReasonPhrase);
                }
                return null;
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
