using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Xml.Linq;

public class ImageResizing
    {
    //public const string BMP= ".bmp";

    private string ImagesExtensionsPath { get; } = $"{Environment.CurrentDirectory}\\Images Extensions.xml";
    public int Percentage { get; private set; } = 0;
    public int Length { get; private set; } = 0;
    public float TotalFilesSize { get; private set; } = 0;
    private float LoadPercentage { get; set; } = 0;
    private ImagesExtensions ImageExtension { get; } = new ImagesExtensions();


    public List<string> ImagesFiles { get; } = new List<string>();
    public List<int> Width { get; set; } = new List<int>();
    public List<int> Height { get; set; } = new List<int>();
    public List<float> Resolution { get; set; } = new List<float>();
    public List<float> FileSize { get; } = new List<float>();
    public List<Image> Images { get; } = new List<Image>();

    public ImageResizing(List<string> imagesFiles)
    {
        ImagesFiles = imagesFiles;
        Length = ImagesFiles.Count();    
    }

        
    //load the images
    public bool LoadImages(int i)
    {
        try
        {
            string file = ImagesFiles[i];
            TotalFilesSize = GetDirectorySize(ImagesFiles.ToArray());

            using (Stream BitmapStream = System.IO.File.Open(file, System.IO.FileMode.Open))
            {
                FileSize.Add(new FileInfo(file).Length);
                Images.Add(Image.FromStream(BitmapStream));
                float fileSizeInPercentage = (FileSize[i] / TotalFilesSize) * 100;
                LoadPercentage += fileSizeInPercentage;
                Percentage = (int)unchecked(Math.Round(LoadPercentage));
            }
            return true;
        }
        catch (ArgumentNullException ex)
        {
            if (ex.ParamName == "path") throw new ImageResizingException("Please Select the path to store the images", ex);
            else throw new ImageResizingException("Failed to load the images due to null parameters", ex);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ImageResizingException("Out of range!!. Please select fewer images", ex);
        }
        catch (Exception)
        {
            throw;
        }
    }

    //save the images
    public bool SaveImages(int i, string saveFolderName, string width, string height, string resolution)
    {
        try
        {
            string extension = ImagesFiles[i].Split('.').Last();
            ImageFormat format;

            if (i == 0) LoadPercentage = 0;

            if (Width.Count == ImagesFiles.Count)
            {
                Width[i] = Int32.Parse(GetValueOrDefault(width, Images[i].Width.ToString()));
                Height[i] = Int32.Parse(GetValueOrDefault(height, Images[i].Height.ToString()));
                Resolution[i] = Int32.Parse(GetValueOrDefault(resolution, Images[i].HorizontalResolution.ToString()));
            }
            else
            {
                Width.Add(Int32.Parse(GetValueOrDefault(width, Images[i].Width.ToString())));
                Height.Add(Int32.Parse(GetValueOrDefault(height, Images[i].Height.ToString())));
                Resolution.Add(Int32.Parse(GetValueOrDefault(resolution, Images[i].HorizontalResolution.ToString())));
            }


            Bitmap newImage = Resizing(Images[i], Width[i], Height[i], Resolution[i]);

            var type = (ImageExtension.GetExtensionType(extension.ToUpper())).ToUpper();

            if (type == "JPEG")
                format = ImageFormat.Jpeg;
            else if (type == "PNG")
                format = ImageFormat.Png;
            else if (type == "BMP")
                format = ImageFormat.Bmp;
            else if (type == "EMF")
                format = ImageFormat.Emf;
            else if (type == "EXIF")
                format = ImageFormat.Exif;
            else if (type == "TIFF")
                format = ImageFormat.Tiff;
            else if (type == "WMF")
                format = ImageFormat.Wmf;
            else if (type == "GIF")
                format = ImageFormat.Gif;
            else if (type == "MEMORYBMP")
                format = ImageFormat.MemoryBmp;
            else if (type == "ICON")
                format = ImageFormat.Icon;
            else
                throw new ImageResizingException($"Cannot resize the images with {extension} extension");

            if (!string.IsNullOrEmpty(saveFolderName))
                newImage.Save(saveFolderName + "\\" + ImagesFiles[i].Split('\\').Last(), format);
            else
                throw new ImageResizingException("Please Select the path to store the images", new Exception("Please Select the path to store the images"));

            float fileSizeInPercentage = (FileSize[i] / TotalFilesSize) * 100;
            LoadPercentage += fileSizeInPercentage;
            Percentage = (int)unchecked(Math.Round(LoadPercentage));

            return true;
        }
        catch (ExtensionsException ex)
        {
            throw new ImageResizingException(ex.Message, ex.InnerException);
        }
        catch (ArgumentNullException ex)
        {
            throw new ImageResizingException("Failed to save the images due to null parameters", ex);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ImageResizingException("Please select fewer images", ex);
        }
        catch (Exception)
        {
            throw;
        }
        
    }
    private T GetValueOrDefault<T>(T value, T defaultValue) => String.IsNullOrEmpty(value.ToString()) ? defaultValue : value;


    private Bitmap Resizing(Image image, int width, int height, float resolution)
    {

        try
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(resolution, resolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    graphics.Save();
                }
            }

            return destImage;
        }
        catch (ArgumentNullException ex)
        {
            throw new ImageResizingException("Failed to resize the images due to null parameters", ex);
        }
        catch (Exception ex)
        {
            throw new ImageResizingException("Failed to resize the images", ex);
        }
    }

    //Get the total size of the images files
    private long GetDirectorySize(string[] files) =>
        (
    from file in files
    select new FileInfo(file).Length).Sum();

    




}

