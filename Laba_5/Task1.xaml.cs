using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Laba_5
{
    /// <summary>
    /// Interaction logic for Task1.xaml
    /// </summary>
    public partial class Task1 : Window
    {
        private BettingViewModel vm = new BettingViewModel();
        private List<Horse> Horses = new();
        private Barrier barrier;
        private CancellationTokenSource raceTokenSource;
        private const int FinishLine = 1000;
        private bool raceFinished = false;
        private Random random = new();

        public Task1()
        {
            InitializeComponent();
            Horses = vm.Horses;
            foreach (var horse in Horses)
            {
                if (horse.Color is SolidColorBrush brush)
                {
                    var color = brush.Color;
                    horse.AnimationFrames = GetHorseAnimation(color);
                }
            }

            DataContext = vm;
        }
        private void IncreaseBet_Click(object sender, RoutedEventArgs e) => vm.IncreaseBet();
        private void DecreaseBet_Click(object sender, RoutedEventArgs e) => vm.DecreaseBet();
        private void NextHorse_Click(object sender, RoutedEventArgs e) => vm.NextHorse();
        private void PreviousHorse_Click(object sender, RoutedEventArgs e) => vm.PreviousHorse();

        private void Bet_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Balance < vm.BetAmount)
            {
                MessageBox.Show("Not enough money");
                return;
            }
            vm.Balance -= vm.BetAmount;
            foreach (var horse in Horses)
            {
                horse.Reset();
                horse.Speed = random.Next(5, 10);// створимо цей метод нижче
            }

            StartRace();
        }


        private void StartRace()
        {
            raceFinished = false;
            raceTokenSource = new CancellationTokenSource();
            barrier = new Barrier(Horses.Count, _ => Dispatcher.Invoke(DrawHorses));

            foreach (var horse in Horses)
            {
                horse.StartMoving(
                    barrier,
                    FinishLine,
                    () => Dispatcher.Invoke(DrawHorses),
                    raceTokenSource.Token,
                    OnHorseFinish);
            }
        }

        private void OnHorseFinish(Horse winner)
        {
            if (raceFinished) return;
            raceFinished = true;
            raceTokenSource.Cancel();

            Dispatcher.Invoke(() =>
            {
                MessageBox.Show($"🏁 {winner.Name} wins!\nTime: {winner.Time.TotalSeconds:F2} sec", "Race Result");

                bool win = winner.Name == vm.SelectedHorseText;
                double coeff = winner.Coefficient;

                if (win)
                {
                    int payout = (int)(vm.BetAmount * coeff);
                    vm.Balance += payout;
                    MessageBox.Show($"You won! +{payout}$");
                }
                else
                {
                    MessageBox.Show($"You lost! -{vm.BetAmount}$");
                }

                UpdateHorseCoefficients();
                dataGrid.Items.Refresh();
                vm.NotifyBalance();
            });
        }

        private void UpdateHorseCoefficients()
        {
            var sorted = Horses.OrderBy(h => h.Time).ToList();
            int n = Horses.Count;
            for (int i = 0; i < n; i++)
            {
                sorted[i].Coefficient = 1.5 + (n - i - 1) * 0.5;
            }
        }

        private void DrawHorses()
        {
            for (int i = canvas.Children.Count - 1; i >= 1; i--)
                canvas.Children.RemoveAt(i);

            var ranked = Horses.OrderByDescending(h => h.TrackX).ToList();
            for (int i = 0; i < ranked.Count; i++)
                ranked[i].Position = i + 1;

            for (int i = 0; i < Horses.Count; i++)
            {
                var horse = Horses[i];
                var image = new Image
                {
                    Source = horse.CurrentFrame,
                    Width = 80,
                    Height = 80,
                };
                Canvas.SetLeft(image, horse.TrackX);
                Canvas.SetTop(image, 100 + i * 50);
                canvas.Children.Add(image);
            }
            dataGrid.Items.Refresh();
        }

        public List<ImageSource> GetHorseAnimation(Color color)
        {
            var bitmaps = ReadImageList(@"C:\\Users\\ADMIN\\OneDrive\\Desktop\\OOP\\Laba_5\\Images\\Horses");
            var masks = ReadImageList(@"C:\\Users\\ADMIN\\OneDrive\\Desktop\\OOP\\Laba_5\\Images\\HorsesMask");
            return bitmaps.Select((img, i) => GetImageWithColor(img, masks[i], color)).ToList();
        }

        private List<BitmapImage> ReadImageList(string folderPath)
        {
            return Directory.GetFiles(folderPath, "*.png")
                            .OrderBy(f => f)
                            .Select(f => new BitmapImage(new Uri(f, UriKind.Absolute)))
                            .ToList();
        }

        private ImageSource GetImageWithColor(BitmapImage image, BitmapImage mask, Color tintColor)
        {
            int width = image.PixelWidth, height = image.PixelHeight;
            var imageBmp = new WriteableBitmap(image);
            var maskBmp = new WriteableBitmap(mask);
            var output = new WriteableBitmap(width, height, image.DpiX, image.DpiY, PixelFormats.Bgra32, null);

            int stride = width * 4;
            byte[] imagePixels = new byte[height * stride];
            byte[] maskPixels = new byte[height * stride];
            byte[] resultPixels = new byte[height * stride];

            imageBmp.CopyPixels(imagePixels, stride, 0);
            maskBmp.CopyPixels(maskPixels, stride, 0);

            for (int i = 0; i < imagePixels.Length; i += 4)
            {
                double alphaFactor = maskPixels[i + 3] / 255.0;
                resultPixels[i] = (byte)(imagePixels[i] * (1 - alphaFactor) + tintColor.B * alphaFactor);
                resultPixels[i + 1] = (byte)(imagePixels[i + 1] * (1 - alphaFactor) + tintColor.G * alphaFactor);
                resultPixels[i + 2] = (byte)(imagePixels[i + 2] * (1 - alphaFactor) + tintColor.R * alphaFactor);
                resultPixels[i + 3] = imagePixels[i + 3];
            }

            output.WritePixels(new Int32Rect(0, 0, width, height), resultPixels, stride, 0);
            return output;
        }
    }

  

    public class BettingViewModel : INotifyPropertyChanged
    {
        public int Balance { get; set; } = 250;
        public int BetAmount { get; set; } = 20;
        private static Random random = new();
        public List<Horse> Horses { get; set; } = new()
    {
        new Horse("Anton", RandomBrush(), random.Next(5, 10)),
        new Horse("Dima", RandomBrush(), random.Next(5, 10)),
        new Horse("Ivan", RandomBrush(), random.Next(5, 10)),
        new Horse("Artem", RandomBrush(), random.Next(5, 10)),
        new Horse("Sasha", RandomBrush(), random.Next(5, 10)),
        new Horse("Anna", RandomBrush(), random.Next(5, 10)),
    };

        private int horseIndex = 0;

        public string BetAmountText => $"{BetAmount}$";
        public string BalanceText => $"Balance: {Balance}$";
        public string SelectedHorseText => Horses[horseIndex].Name;

        public void IncreaseBet() => SetBet(BetAmount + 10);
        public void DecreaseBet() => SetBet(Math.Max(10, BetAmount - 10));
        public void NextHorse() => SetHorse((horseIndex + 1) % Horses.Count);
        public void PreviousHorse() => SetHorse((horseIndex - 1 + Horses.Count) % Horses.Count);

        private void SetBet(int value) { BetAmount = value; OnPropertyChanged(nameof(BetAmountText)); }
        private void SetHorse(int index) { horseIndex = index; OnPropertyChanged(nameof(SelectedHorseText)); }

        public void NotifyBalance()
        {
            OnPropertyChanged(nameof(BalanceText));
        }

        private static SolidColorBrush RandomBrush()
        {
            return new SolidColorBrush(Color.FromArgb(255,
                (byte)random.Next(0, 255),
                (byte)random.Next(0, 255),
                (byte)random.Next(0, 255)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }





}
