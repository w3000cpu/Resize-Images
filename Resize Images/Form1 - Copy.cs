using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        Image[] images;
        int i, j;
        string saveFolderName;
        string[] loadImages;
        long[] fileSize;
        long totalFilesSize;
        public Form1()
        {
            InitializeComponent();

        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

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
                }
            }

            return destImage;
        }
        public Image stringToImage(string inputString)
        {
            string converted = inputString.Replace('-', '+');
            converted = converted.Replace('_', '/');
            byte[] imageBytes = Convert.FromBase64String(converted);
            MemoryStream ms = new MemoryStream(imageBytes);

            Image image = Image.FromStream(ms, true, true);

            return image;
        }
        public static long GetDirectorySize(string[] p)
        {
            // 1.
            // Get array of all file names.
            //string[] a = Directory.GetFiles();

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in p)
            {
                // 3.
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            // 4.
            // Return total size
            return b;
        }
        

        private void loadBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*.GIF;*.EMF;*.EXIF;*.TIFF;*.WMF)|*.BMP;*.JPG;*.PNG;*.GIF;*.EMF;*.EXIF;*.TIFF;*.WMF";
            DialogResult result = openFileDialog1.ShowDialog();
            fileSize = new long[openFileDialog1.SafeFileNames.Length];
            totalFilesSize = 0;
            i = 0;
            j = 0;
            if (result == DialogResult.OK) // Test result.
            {
                
                totalFilesSize = GetDirectorySize(openFileDialog1.FileNames)/1024;

                i = 0;
                loadImages = new string[openFileDialog1.SafeFileNames.Length];
                images = new Image[openFileDialog1.SafeFileNames.Length];
               
                foreach (string file in openFileDialog1.FileNames)
                {
                    using (Stream BitmapStream = System.IO.File.Open(file, System.IO.FileMode.Open))
                    {
                        fileSize[i] = new FileInfo(openFileDialog1.FileNames[i]).Length/1024;
                        loadImages[i] = openFileDialog1.SafeFileNames[i];
                        images[i] = Image.FromStream(BitmapStream);
                        progressBar1.Value = 50;
                    }
                    i++;
                }

            }

        }


        private void confirmBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string extension;
                ImageFormat format;
                int width;
                int height;
                for (int i = 0; i < openFileDialog1.SafeFileNames.Length; i++)
                {
                    width = images[i].Width;
                    height = images[i].Height; 
                
                    format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    extension = loadImages[i].Split('.')[loadImages[i].Split('.').Length - 1];
                    if (widthTxtBox.Text != string.Empty) width = Int32.Parse(widthTxtBox.Text);
                    if (heightTxtBox.Text != string.Empty) height = Int32.Parse(heightTxtBox.Text);
                    Bitmap newImage = ResizeImage(images[i], width, height);
                    if (extension.ToLower() == "jpg" || extension.ToLower() == "jpeg")
                        format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    else if (extension.ToLower() == "png")
                        format = System.Drawing.Imaging.ImageFormat.Png;
                    else if (extension.ToLower() == "bmp")
                        format = System.Drawing.Imaging.ImageFormat.Bmp;
                    else if (extension.ToLower() == "emf")
                        format = System.Drawing.Imaging.ImageFormat.Emf;
                    else if (extension.ToLower() == "exif")
                        format = System.Drawing.Imaging.ImageFormat.Exif;
                    else if (extension.ToLower() == "tiff")
                        format = System.Drawing.Imaging.ImageFormat.Tiff;
                    else if (extension.ToLower() == "wmf")
                        format = System.Drawing.Imaging.ImageFormat.Wmf;
                    else
                        MessageBox.Show("Please Select the path to store the images");

                    if (saveFolderName != null)
                    {
                        newImage.Save(saveFolderName + "\\" + loadImages[i], format);
                    }
                    else
                    {
                        MessageBox.Show("Please Select the path to store the images");
                        return;
                    }
                }

                MessageBox.Show("Images resized");

            }
            catch (Exception)
            {

                MessageBox.Show("Please Select the images");
            }
            
           
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if( result == DialogResult.OK )
            {
                saveFolderName = folderBrowserDialog1.SelectedPath;

            }
        }

        private void widthTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(widthTxtBox.Text, "  ^ [0-9]"))
            {
                widthTxtBox.Text = "";
            }
        }

        private void heightTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(heightTxtBox.Text, "  ^ [0-9]"))
            {
                heightTxtBox.Text = "";
            }
        }

        private void widthTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void heightTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

       

        
    }
}
