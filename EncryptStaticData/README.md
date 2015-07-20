#How to encrypt and decrypt static sensitive data in Universal Windows Platform apps

This sample shows how to use the *DataProtectionProvider* API to encrypt and decrypt static (i.e. not streaming) information. For more information check the post titled [How to encrypt and decrypt static sensitive data in Universal Windows Platform apps](http://lukkhacoder.com/2015/07/13/how-to-encrypt-and-decrypt-static-sensitive-data-in-universal-windows-platform-apps/)

Here is what the application looks like:
![Application screen shot](https://github.com/lukkhacoder/Windows-Universal-Samples/blob/master/EncryptStaticData/EncryptStaticData.png "Application view")

Interesting methods to look at are the `EncryptAndSave` and the `ReadAndDecrypt` methods in the code behind of `MainPage.xaml`.

