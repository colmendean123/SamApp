using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sam
{
    public partial class MainPage : ContentPage
    {
        private static Database db;
        private static List<ImageSource> images;
        private static List<MediaSource> media;
        public static Database Database
        {
            get
            {
                if (db == null)
                {
                    db = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                }
                return db;
            }
        }
        public MainPage()
        {
            InitializeComponent();
            displayAll();
        }

        private async void displayAll()
        {
            var imageList = await Database.GetPeopleAsync();
            images = new List<ImageSource>();
            foreach (Music m in imageList)
            {
                images.Add(ImageSource.FromStream(() => BytesToStream(m.get())));
            }
            collectionView.ItemsSource = images;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,

                PickerTitle = "Pick an image"
            });
            if (result != null)
            {

                foreach (var image in result)
                {
                    var stream = await image.OpenReadAsync();
                    byte[] mybytearray = GetImageBytes(stream);
                    await Database.SavePersonAsync(new Music
                    {
                        myData = mybytearray
                    });

                    //resultImage.Source = ImageSource.FromStream(() => BytesToStream(mybytearray));
                    //resultImage.Source = ImageSource.FromStream(() => stream);
                    //imageList.Add(ImageSource.FromStream(() => BytesToStream(mybytearray)));
                }
                var imageList = await Database.GetPeopleAsync();
                images = new List<ImageSource>();
                foreach (Music m in imageList)
                {
                    images.Add(ImageSource.FromStream(() => BytesToStream(m.get())));
                }
                collectionView.ItemsSource = images;
                //collectionView.ItemsSource = BytesToStream(await Database.GetPeopleAsync());
                //collectionView.ItemsSource = imageList;
            }
        }

        private static byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public async void Button_Clicked_1(object sender, EventArgs e)
        {
            var result = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Videos,

                PickerTitle = "Pick a Video"
            });
            if (result != null)
            {

                foreach (var image in result)
                {
                    var stream = await image.OpenReadAsync();
                    byte[] mybytearray = GetImageBytes(stream);
                    await Database.SavePersonAsync(new Music
                    {
                        myData = mybytearray
                    });
                }
                var imageList = await Database.GetPeopleAsync();
                media = new List<MediaSource>();
                foreach (Music m in imageList)
                {
                    //convert to random access stream
                    images.Add(ImageSource.FromStream(() => BytesToStream(m.get())));
                }
                collectionView.ItemsSource = images;
                //collectionView.ItemsSource = BytesToStream(await Database.GetPeopleAsync());
                //collectionView.ItemsSource = imageList;
                //Add videos
            }
        }
            }
}
