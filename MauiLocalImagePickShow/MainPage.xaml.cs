namespace MauiLocalImagePickShow;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await PickImageAsync();
    }

    private async Task PickImageAsync()
    {
        try
        {
            var fileResult = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "PLease select an image",
                FileTypes = FilePickerFileType.Images
            });
            if (fileResult is not null)
            {
                var stream = await fileResult.OpenReadAsync();
                var uploadedImagePath = await UploadLocalAsync(fileResult.FileName, stream);
                img.Source = uploadedImagePath;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error in picking image", ex.Message, "Ok");
        }
    }

    private async Task<string> UploadLocalAsync(string fileName, Stream stream)
    {
        var localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        using var fs = new FileStream(localPath, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fs);

        return localPath;
    }
}

