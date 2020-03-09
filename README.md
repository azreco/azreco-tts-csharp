# AzReco Text To Speech API C# example
Sample project in c# .Net to help you integrate with our text-to-speech API.

This is a sample c# .Net project for uploading text file and saving the audio into a .wav file.

# Supported languages
Azerbaijani (az-AZ)

Turkish  (tr-TR)

Russian  (ru-Ru)

# Requirements

You will need to have the CommandLineParser and Microsoft.Net.Http module installed in your .Net environment.

For Windows please run commands below in terminal:

Install-Package Microsoft.Net.Http -Version 2.2.29

Install-Package CommandLineParser -Version 2.4.3
 
 
For .Net Core please run commands below in terminal:

dotnet add package Microsoft.Net.Http --version 2.2.29

dotnet add package CommandLineParser --version 2.4.3

# Usage example:
In Windows OS native .Net environment:

```sh
client.exe --input-type file -t text\\example-tr.txt -l tr-TR -i api_user_id -k api_token --tts-id tts_id -o example-tr.wav
```

or

```sh
client.exe --input-type text -t "any text" -l tr-TR -i api_user_id -k api_token --tts-id tts_id -o example-tr.wav
``` 

In .Net Core environment:

```sh
dotnet client.dll --input-type file -t text\\example-tr.txt -l tr-TR -i api_user_id -k api_token --tts-id tts_id -o example-tr.wav
```

or

```sh
dotnet client.dll --input-type text -t "any text" -l tr-TR -i api_user_id -k api_token --tts-id tts_id -o example-tr.wav
``` 

In both environment the above command with input type 'file' the script uploads 'example-tr.txt', synthesizes speech using our tr-TR text-to-speech and saves the resulting audio as 'example-tr.wav' when the synthesizing process finished. The above command with input type 'text' the script sends text to the server, synthesizes speech using our tr-TR text-to-speech and saves the resulting audio as 'example-tr.wav' when the synthesizing process finished. 

# What is --tts-id option?

We have several voices for any language. Every voice has own identification. You can specify TTS identification with this option to get your audio file in different voices.
This option is optional and TTS service selects default voice for the given language.

# How to get voice identifications?

We added new REST get method [http://api.azreco.az/voices?api_id=YOUR_API_ID&api_token=YOUR_API_TOKEN](http://api.azreco.az/voices?api_id=YOUR_API_ID&api_token=YOUR_API_TOKEN). 
You can call this method in any browser. We also added this into the [Synthesizer](https://github.com/azreco/azreco-tts-csharp/blob/master/Client/Synthesizer.cs) class as a method. The result is JSON array of voice informations. For example:
```json
[   
   {
      "id":TTS_ID,
      "ttsName":"VOICE_NAME",
      "ttsLanguage":"SHORT_LANGUAGE",
      "ttsGender":"GENDER"
   }
]
```


# How to get user id and token?

To get user id and API token, send a request to info@azreco.az.

To confirm your request, we will ask you some details about your purpose in using API.
