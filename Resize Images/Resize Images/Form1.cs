using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ResizeImagesApplication
{
    //public enum LoggerType
    //{
    //    Event,
    //    File
    //}

    //public enum LogLevel
    //{
    //    Info,
    //    Warning,
    //    Error,
    //    Fatal
    //}
    //public class LoggingCreator
    //{
    //    private static ILogger _logger;
    //    public static ILogger CreateLogger(string source, LoggerType loggerType)
    //    {
    //        switch (loggerType)
    //        {
    //            case LoggerType.Event:
    //                _logger = new EventLogger(source);
    //                break;
    //            case LoggerType.File:
    //                _logger = new FileLogger(source);
    //                break;
    //            default:
    //                break;
    //        }
    //        return _logger;
    //    }
    //    public static void ExceptionalLogging(Exception ex, LogLevel logLevel)
    //    {
    //        var message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
    //        switch (logLevel)
    //        {
    //            case LogLevel.Warning:
    //                _logger.LogWarning(message);
    //                break;
    //            case LogLevel.Error:
    //                _logger.LogError(message);
    //                break;
    //            case LogLevel.Fatal:
    //                _logger.LogFatal(message);
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    public void LoggingInfo(string message) => _logger.LogInfo(message);
    //}



    public partial class Form1 : Form
    {
        private string saveFolderName;
        private FilesStatus? status;
        private bool loadImageFlag, saveImageFlag;
        private DialogResult result;
        private ImageResizing imagesResizing;
        private readonly ILogger eventLogger;
        private readonly ILogger fileLogger;
        private const string expression = @"^(0*)$";

        public Form1()
        {
            InitializeComponent();
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker1_DoWork);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            Label.CheckForIllegalCrossThreadCalls = false;
            eventLogger = new EventLogger("Resize Images.exe");
            fileLogger = new FileLogger($"{Environment.CurrentDirectory}\\Logs.log");
           // eventLogger = LoggingCreator.CreateLogger("Resize Images.exe",LoggerType.Event);
           // fileLogger = LoggingCreator.CreateLogger($"{Environment.CurrentDirectory}\\Logs.txt", LoggerType.Event);
            
            
            
            
           ResetValues();
        }
        
        private void WidthTxtBox_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(widthTxtBox.Text, expression))
            {
                widthTxtBox.Text = String.Empty;
            }
            TextBoxWarning(false);


        }

        private void HeightTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(heightTxtBox.Text, expression))
            {
                heightTxtBox.Text = String.Empty;
            }
            TextBoxWarning(false);
        }
        private void ResolutionTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(resolutionTxtBox.Text, expression))
            {
                resolutionTxtBox.Text = String.Empty;
            }
            TextBoxWarning(false);
        }

        private void WidthTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void HeightTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }
        private void ResolutionTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }
        private void LoadBtn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                backgroundWorker1.CancelAsync();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                backgroundWorker1.CancelAsync();            
        }

        private void LoadBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                backgroundWorker1.CancelAsync();
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.FileName = String.Empty;
                status = FilesStatus.Loading;
                ResetValues();
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.Filter = SetImagesDialogFilter();

                result = openFileDialog1.ShowDialog();

                if (backgroundWorker1.IsBusy != true)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (ImageResizingException ex)
            {
                ErrorMessage(ex.Message);

                LoggingError(new List<ILogger>() { fileLogger, eventLogger }, ex);
            }
            catch (ExtensionsException ex)
            {
                ErrorMessage(ex.Message);

                LoggingError(new List<ILogger>() { fileLogger, eventLogger }, ex);
            }
            catch (Exception ex)
            {
                ErrorMessage("Unexpected Error");

                LoggingError(new List<ILogger>() { fileLogger, eventLogger }, ex);
            }            
        }

       

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            try
            {         
                if (progressBar1.Value < 100)
                {
                    if (status == FilesStatus.Loading || status == FilesStatus.Saving)
                    {
                        MessageBox.Show("Please wait...");
                        return;
                    }
                }

                if (string.IsNullOrEmpty(widthTxtBox.Text) && string.IsNullOrEmpty(heightTxtBox.Text) && string.IsNullOrEmpty(resolutionTxtBox.Text))
                {
                    TextBoxWarning(true);
                    ErrorMessage("Please fill at least one of the fields");
                    return;
                }

                ResetValues();
                
                status = FilesStatus.Saving;
                
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage("Please Select the images ");
                status = FilesStatus.Error;

                LoggingError(new List<ILogger>() { fileLogger, eventLogger }, ex);
            }
        }

        private  void TextBoxWarning(bool enabled)
        {
            if (enabled)
            {
                widthBorder.Visible = true;
                heightBorder.Visible = true;
                resolutionBorder.Visible = true;
            }
            else 
            {
                widthBorder.Visible = false;
                heightBorder.Visible = false;
                resolutionBorder.Visible = false;
            }
        }
        private void TextBoxWarning(Label border, bool enabled) 
        {
            if (enabled) 
                border.Visible = true;
            else 
                border.Visible = false;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                saveFolderName = folderBrowserDialog1.SelectedPath;
                saveImagesPath.Text = saveFolderName;
                saveImagesPath.ForeColor = Color.MidnightBlue;
            }
        }


        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                if (backgroundWorker1.CancellationPending)
                {
                    //CANCEL
                    e.Cancel = true;
                }
                else
                {
                    switch (status)
                    {
                        //Load images
                        case FilesStatus.Loading:
                            if (result == DialogResult.OK) // Test result.
                            {
                                int i;
                                //string[] filePaths = Directory.GetFiles(@"C:\Users\w3000cpu\Desktop\test\", "*.jpg", SearchOption.TopDirectoryOnly);
                                //filesNames
                                imagesResizing = new ImageResizing(openFileDialog1.FileNames.ToList<string>()); 

                                fileLogger.LogInfo("Start loading the image/s");
                                for (i = 0; i < imagesResizing.Length; i++)
                                {
                                    loadImageFlag = imagesResizing.LoadImages(i);
                                    backgroundWorker1.ReportProgress(imagesResizing.Percentage);
                                }                                
                                fileLogger.LogInfo($"{i} image/s selected");
                                selectedImagesLabel.Text = $"{i} image/s selected";
                                selectedImagesLabel.ForeColor = Color.MidnightBlue;
                            }
                            else
                                backgroundWorker1.CancelAsync();
                            break;

                         
                        //Save images
                        case FilesStatus.Saving:
                            if (imagesResizing?.Length == null)
                                throw new ImageResizingException("Click \"Load\" and select the image/s", new Exception("Click \"Load\" and select the image/s"));

                            fileLogger.LogInfo("Start resizing and saving the image/s");
                            for (int i = 0; i < imagesResizing.Length; i++)
                            {

                                string width = widthTxtBox.Text;
                                string height = heightTxtBox.Text;
                                string resolution = resolutionTxtBox.Text;
                             
                                saveImageFlag = imagesResizing.SaveImages(i, saveFolderName, width, height, resolution, resolution);
                                backgroundWorker1.ReportProgress(imagesResizing.Percentage);
                                
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (backgroundWorker1.CancellationPending)
                {
                    //CANCEL
                    e.Cancel = true;
                    status = null;
                }
            }
            catch (ImageResizingException ex)
            {
                ErrorMessage(ex.Message);
                e.Cancel = true;
                status = FilesStatus.Error;
                LoggingError(new List<ILogger>() { fileLogger, eventLogger }, ex);
            }
            catch (Exception ex)
            {
                 ErrorMessage("Unexpected Error");
                e.Cancel = true;
                status = FilesStatus.Error;
                LoggingError(new List<ILogger>() {fileLogger, eventLogger }, ex);
            }

        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            percentageLabel.Text = $"{e.ProgressPercentage}%";
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                ResetValues();
            }
            else
            {
                while (progressBar1.Value != 100)
                {
                    progressBar1.Value++;

                    percentageLabel.Text = $"{progressBar1.Value}%";
                }

                if (status == FilesStatus.Loading && loadImageFlag)
                {
                    MessageBox.Show("Images loaded successfully");
                    fileLogger.LogInfo("Images loaded successfully");

                }
                else if (status == FilesStatus.Saving && saveImageFlag)
                {
                    MessageBox.Show("Images saved successfully");
                    fileLogger.LogInfo("Images saved successfully");
                    Process.Start(saveFolderName);
                }
                else if (status == FilesStatus.Loading && !loadImageFlag)
                {
                    ErrorMessage("Failed to load the image/s");
                    fileLogger.LogError("Failed to load the image/s");
                    status = FilesStatus.Error;
                }
                else if (status == FilesStatus.Saving && !saveImageFlag)
                {
                    ErrorMessage("Failed to save the image/s");
                    LoggingError(fileLogger, new ImageResizingException("Falied to save the image/s"));
                    status = FilesStatus.Error;
                    //fileLogger.LogError();
                }
                else
                {
                    ErrorMessage("Click \"Load\" and select the image/s");
                    status = FilesStatus.Error;
                }
            }
        }
        private void ResetValues()
        {
            progressBar1.Value = 0;
            percentageLabel.Text = $"{progressBar1.Value}%";
            loadImageFlag = saveImageFlag = false;
        }

        private string SetImagesDialogFilter()
        {
            ImagesExtensions ImageExtension = new ImagesExtensions();
            string format = String.Empty;
            string description = String.Empty;
            var types = ImageExtension.GetExtensionTypes();
            List<XElement> extensions = new List<XElement>();

            types.ForEach(
                type => {
                    description = ImageExtension.GetDescription(type.Value);
                    extensions = ImageExtension.GetAllExtensions(type.Value);
                    format += SubFormat(description, extensions);
                });

            extensions = ImageExtension.GetAllExtensions();
            description = "All Images Files";

            format += SubFormat(description, extensions);
            return format = format.Remove(format.Length - 1);
        }

        private string SubFormat(string description, List<XElement> extensions)
        {
            if (description is null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            string format = $"{description}(";
            string formatSub = String.Empty;
            extensions.ForEach(
                extension =>
                {
                    formatSub += $"*.{extension.Value}";

                    if (extensions.IndexOf(extension) == extensions.Count - 1)
                        formatSub += $")|{formatSub}|";
                    else
                        formatSub += $";";
                });
            return format += formatSub;

        }

       private void ErrorMessage(string message) => this.Invoke((Func<DialogResult>)(() => MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error)));
       
        private void LoggingError(ILogger logger, Exception ex) 
        {

            var message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
            logger.LogError(message);

        }

        private void LoggingError(List<ILogger> logger, Exception ex)
        {

            var message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
            logger.ForEach(l => l.LogError(message));

        }        

    }
}
