using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Windows.UI.Popups;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI; //Title Bar Color
using Windows.UI.ViewManagement;//Title Bar Color
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;

//Drag and Drop
using Windows.UI.Core;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sub_Cleaner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StorageFile _file = null;
        public MainPage()
        {
            this.InitializeComponent();
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SetTitleBarBackground();

            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += Back_Button;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested -= Back_Button;
        }

        private void Back_Button(object sender, BackRequestedEventArgs e)
        {
            //if (Frame.CanGoBack()) Frame.GoBack();
            Frame.Navigate(typeof(MainPage));
        }

        private static void SetTitleBarBackground()
        {
            // Get the instance of the Title Bar
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            // Set the color of the Title Bar content
            titleBar.BackgroundColor = Colors.DarkSlateGray;
            titleBar.ForegroundColor = Colors.Black;

            // Set the color of the Title Bar buttons
            titleBar.ButtonBackgroundColor = Colors.Teal;
            titleBar.ButtonForegroundColor = Colors.Black;
        }

        //DragandDrop not working in UWP
        /*private void DropArea_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Link;
            if (e.DragUIOverride != null)
            {
                e.DragUIOverride.Caption = "You are dragging a Sub";
                e.DragUIOverride.IsCaptionVisible = true;
                e.DragUIOverride.IsContentVisible = true;
            }
            DropArea.Background = new SolidColorBrush(Color.FromArgb(255, 168, 168, 168));
        }

        private async void DropArea_Drop(object sender, DragEventArgs e)
        {
            DropArea.Background = new SolidColorBrush(Colors.Transparent);
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Any())
                {
                    //var storeFile = items[0] as StorageFile;
                    // var bitmapImage = new BitmapImage();
                    //bitmapImage.SetSource(await storeFile.OpenAsync(FileAccessMode.Read));
                    // dragedImage.Source = bitmapImage;
                    DropOut.Visibility = Visibility.Collapsed;
                    DropFull.Visibility = Visibility.Visible;
                    Arow.Visibility = Visibility.Visible;
                    //OldSubSV.Visibility = Visibility.Visible;
                    var files = await e.DataView.GetStorageItemsAsync();
                    var file = files.First() as StorageFile;

                    if (file != null) _file = file;
                }
            }
            
        }

        private void DropArea_DragLeave(object sender, DragEventArgs e)
        {
            DropArea.Background = new SolidColorBrush(Colors.Transparent);
        }*/

        private void DispatcherTimer_Tick(object sender, object e)
        {
            if (fv.SelectedIndex == 14)
            {
                fv.SelectedIndex = 0;
            }
            else
            {
                fv.SelectedIndex++;
            }


        }

        private async void Clean_Sab(object sender, TappedRoutedEventArgs e)
        {
            Arow.Visibility = Visibility.Collapsed;
            DropFull.Visibility = Visibility.Collapsed;
            DropOut.Visibility = Visibility.Visible;
            
            var result = 0; //Anonimus Error
            await Task.Run(async () =>
            {
                if (_file != null)
                {
                   // string path = _file.Path;
                    int i = 2, cunt_readText = 0, newSub = 0, Num_of_New_Sub = 1;
                    // Open the file to read from.
                    //var readText = File.ReadAllLines(path);
                    List<string> readText = new List<string>();
                    using (var str = new StreamReader((await _file.OpenStreamForReadAsync()), Encoding.UTF8))
                    {
                        while (!str.EndOfStream)
                            readText.Add(str.ReadLine());

                        str.Dispose();

                    }
                    var megethos_readText = readText.Count;
                    var CorectSub = new string[megethos_readText/2];
                    foreach (var s in readText)
                    {
                        var metritis_gia_na_vro_mexri_to_keno = cunt_readText;
                        var w = i.ToString();
                        if (s == w)
                        {
                            i += 2;
                            CorectSub[newSub] = Num_of_New_Sub.ToString();
                            Num_of_New_Sub++;
                            newSub++;
                            do
                            {
                                if (metritis_gia_na_vro_mexri_to_keno >= megethos_readText - 1) break;
                                metritis_gia_na_vro_mexri_to_keno++;
                                CorectSub[newSub] = readText[metritis_gia_na_vro_mexri_to_keno];
                                newSub++;
                            } while (readText[metritis_gia_na_vro_mexri_to_keno] != "");
                            if (newSub != (megethos_readText / 2))
                            {
                                CorectSub[newSub] = "";
                            }
                        }
                        cunt_readText++;
                    }
                    i = i - 2;
                    if (i != 0)
                    {

                        //File.WriteAllLines(path, CorectSub);
                        //await _file.DeleteAsync();
                        await FileIO.WriteTextAsync(_file, "");
                        using (var str = new StreamWriter((await _file.OpenStreamForWriteAsync()), Encoding.UTF8))
                        {
                            str.Flush();
                            foreach (var s in CorectSub)
                            {
                                await str.WriteLineAsync(s);
                            }
                            str.Dispose();
                        }
                        
                        result = 1; //Sacsses!
                    }

                }
                else
                {
                    result = 3;//Has not finde the file 
                }
            });
            //Result
            
                    if (result == 1)
                    {
                        // Random Algrithm
                        {
                            var rnd = new Random();
                            var SLogo = rnd.Next(1, 11);
                            UnS1.Visibility = Visibility.Collapsed; UnS2.Visibility = Visibility.Collapsed; UnS3.Visibility = Visibility.Collapsed;
                            S1.Visibility = Visibility.Collapsed; S2.Visibility = Visibility.Collapsed; S3.Visibility = Visibility.Collapsed; S4.Visibility = Visibility.Collapsed; S5.Visibility = Visibility.Collapsed; S6.Visibility = Visibility.Collapsed; S6.Visibility = Visibility.Collapsed; S7.Visibility = Visibility.Collapsed; S8.Visibility = Visibility.Collapsed; S9.Visibility = Visibility.Collapsed; S10.Visibility = Visibility.Collapsed;
                            Logo.Visibility = Visibility.Collapsed;
                            Arow.Visibility = Visibility.Collapsed;
                            switch (SLogo)
                            {
                                case 1:
                                    S1.Visibility = Visibility.Visible;
                                    break;
                                case 2:
                                    S2.Visibility = Visibility.Visible;
                                    break;
                                case 3:
                                    S3.Visibility = Visibility.Visible;
                                    break;
                                case 4:
                                    S4.Visibility = Visibility.Visible;
                                    break;
                                case 5:
                                    S5.Visibility = Visibility.Visible;
                                    break;
                                case 6:
                                    S6.Visibility = Visibility.Visible;
                                    break;
                                case 7:
                                    S7.Visibility = Visibility.Visible;
                                    break;
                                case 8:
                                    S8.Visibility = Visibility.Visible;
                                    break;
                                case 9:
                                    S9.Visibility = Visibility.Visible;
                                    break;
                                default:
                                    S10.Visibility = Visibility.Visible;
                                    break;
                            }
                            var myMessage = new MessageDialog("The Subtitle has been Cleaned! ", "Everything went good!");
                            await myMessage.ShowAsync();
                        }
                    }
                    else
                    {
                        // Random Algrithm
                        {
                            Random rnd = new Random();
                            var UnSLogo = rnd.Next(1, 4);
                            UnS1.Visibility = Visibility.Collapsed; UnS2.Visibility = Visibility.Collapsed; UnS3.Visibility = Visibility.Collapsed;
                            S1.Visibility = Visibility.Collapsed; S2.Visibility = Visibility.Collapsed; S3.Visibility = Visibility.Collapsed; S4.Visibility = Visibility.Collapsed; S5.Visibility = Visibility.Collapsed; S6.Visibility = Visibility.Collapsed; S6.Visibility = Visibility.Collapsed; S7.Visibility = Visibility.Collapsed; S8.Visibility = Visibility.Collapsed; S9.Visibility = Visibility.Collapsed; S10.Visibility = Visibility.Collapsed;
                            Logo.Visibility = Visibility.Collapsed;
                            Arow.Visibility = Visibility.Collapsed;
                            switch (UnSLogo)
                            {
                                case 1:
                                    UnS1.Visibility = Visibility.Visible;
                                    break;
                                case 2:
                                    UnS2.Visibility = Visibility.Visible;
                                    break;
                                default:
                                    UnS3.Visibility = Visibility.Visible;
                                    break;
                            }

                            if (result == 0)
                            {
                                var myMessage = new MessageDialog("The Subtitle has not been Cleaned!\nPlease press the question mark button to see how to use the app and try again.  ", "Hmm.. It seems that there's something wrong here!");
                                await myMessage.ShowAsync();
                            }
                            else
                            {
                                var myMessage = new MessageDialog("The Subtitle has not been Cleaned!\nThe file was not found.", "Hmm.. It seems that there's something wrong here!");
                                await myMessage.ShowAsync();
                            }
                        }
                    }
                
            

        }

        private async void Sub_Finder(object sender, TappedRoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            //openPicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            openPicker.FileTypeFilter.Add(".srt");

            var file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                Arow.Visibility = Visibility.Visible;
                //OldSubSV.Visibility = Visibility.Visible;
                DropOut.Visibility = Visibility.Collapsed;
                DropFull.Visibility = Visibility.Visible;
                /*var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read); using
                (var reader = new StreamReader(stream.AsStream()))
                {
                    OldSub.Text = reader.ReadToEnd();
                }*/
                _file = file;
            }
        }

        private void Goto_info(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(info_Page));
        }

        private async void EmailMe(object sender, RoutedEventArgs e)
        {
            var emailMessage = new EmailMessage();
            emailMessage.Subject = "SubCleaner (UWP) - Feedback";
            emailMessage.To.Add(new EmailRecipient("psmakos@hotmail.com", "Panagiotis-Stephanos Makos"));
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        private void About(object sender, RoutedEventArgs e)
        {
           // AboutDialog about = new AboutDialog();
           // await about.ShowAsync();
        }

        private void Rate(object sender, RoutedEventArgs e)
        {
            //Windows.System.Launcher.LaunchUriAsync(new Uri(""));
        }
    }
}
