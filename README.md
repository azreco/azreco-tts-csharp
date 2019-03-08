# AzReco Text To Speech API C# example
Example project in c# .Net to help you integrate with our text-to-speech API.

This is an example c# .Net project for uploading text file and saving the audio into a .wav file.

# Supporting languages
AZERBAIJANI (az-AZ)

TURKISH  (tr-TR)

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

client.exe -t text\\example-tr.txt -l tr-TR -i api_user_id -k api_token -o example-tr.wav  

In .Net Core environment:

dotnet client.dll -t text\\example-tr.txt -l tr-TR -i api_user_id -k api_token -o example-tr.wav

In this example the application uploads 'example-tr.txt', synthesizes speech using our tr-TR text-to-speech and saves the resulting audio as 'example-tr.wav' when the synthesizing process finished.


# How to get user id and token?

To get user id and API token, send a request to info@azreco.az.

To confirm your request, we will ask you some details about your purpose in using API.
