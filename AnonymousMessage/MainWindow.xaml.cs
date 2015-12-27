using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace AnonymousMessage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// You know what it is
        /// Errythang I do
        /// I do it big
        /// </summary>
        public SpeechSynthesizer SpeechSynthesizer { get; set; }

        public MainWindow()
        {
            SpeechSynthesizer = new SpeechSynthesizer();
            InitializeComponent();
        }

        private void btnSpeak_Click(object sender, RoutedEventArgs e)
        {
            new Thread(text =>
            {
                SpeechSynthesizer.SetOutputToDefaultAudioDevice();
                SpeechSynthesizer.SpeakAsync((string)text);
            }).Start(tbMessage.Text);
            tbMessage.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Wave Files|*.wav"
            };
            if (sfd.ShowDialog() == true)
            {
                new Thread(text =>
               {
                   SpeechSynthesizer.SetOutputToWaveFile(sfd.FileName);
                   SpeechSynthesizer.SpeakAsync((string) text);
                   Process.Start(sfd.FileName);
                   MessageBox.Show("Recording successful.");
               }).Start(tbMessage.Text);
            }
            tbMessage.Focus();
        }
    }
}
