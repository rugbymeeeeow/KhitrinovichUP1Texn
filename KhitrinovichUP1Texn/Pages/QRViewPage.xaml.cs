using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QRCoder;  

namespace KhitrinovichUP1Texn.Pages
{
    /// <summary>
    /// Логика взаимодействия для QRViewPage.xaml
    /// </summary>
    public partial class QRViewPage : Page
    {
            public QRViewPage()
            {
                InitializeComponent();
                GenerateQR();
            }

            private void GenerateQR()
            {
                string inputText = "https://docs.google.com/forms/d/19E2kBn8zLh1MSZPRGbxnnTzVThDKCvvpOCjW9pffcck/edit";

                if (string.IsNullOrWhiteSpace(inputText))
                {
                    MessageBox.Show("Введите текст для генерации QR-кода", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    // Генерация QR-кода с помощью QRCoder
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);

                    // Получаем графическое изображение QR-кода
                    using (var qrCodeBitmap = qrCode.GetGraphic(10))
                    using (var memory = new MemoryStream())
                    {
                        // Сохраняем изображение в поток памяти
                        qrCodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                        memory.Position = 0;

                        // Создаем BitmapImage из потока памяти
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memory;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        // Устанавливаем изображение в элемент Image
                        QRCodeImage.Source = bitmapImage;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при генерации QR-кода: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
    }
    
}
