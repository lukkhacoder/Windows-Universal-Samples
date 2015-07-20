using System;
using System.Diagnostics;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EncryptStaticData
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void EncryptAndSave(object sender, RoutedEventArgs e)
        {
            //Encrypt the text
            DataProtectionProvider dataProtectionProvider = new DataProtectionProvider("Local=user");
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(textToEncrypt.Text, BinaryStringEncoding.Utf8);
            IBuffer protectedBuffer = await dataProtectionProvider.ProtectAsync(buffer);

            //Persist encrypted text
            StorageFile storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(encryptFilename.Text,
                CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteBufferAsync(storageFile, protectedBuffer);

            viewFileButton.IsEnabled = true;
            ClearTextBlock.Text = String.Empty;
        }

        private async void ViewFile(object sender, RoutedEventArgs e)
        {
            StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(encryptFilename.Text)
                as StorageFile;
            if (storageFile != null)
                await Launcher.LaunchFileAsync(storageFile);
        }

        private async void ReadAndDecrypt(object sender, RoutedEventArgs e)
        {
            StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(encryptFilename.Text)
                as StorageFile;

            if (storageFile != null)
            {
                IBuffer protectedBuffer = await FileIO.ReadBufferAsync(storageFile);
                DataProtectionProvider dataProtectionProvider = new DataProtectionProvider("Local=user");
                IBuffer clearTextBuffer = await dataProtectionProvider.UnprotectAsync(protectedBuffer);
                string clearText = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, clearTextBuffer);
                ClearTextBlock.Text = clearText;
            }
        }

        private async void OpenFolder(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);

        }
    }
}
