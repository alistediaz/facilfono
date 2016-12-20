using MakeCall.Page;
using System;

using Xamarin.Forms;

namespace MakeCall
{
    public class HomePage : ContentPage
    {
        // Number of squares horizontally and vertically,
        //  but if you change it, some code will break.
        static readonly int NUMC = 3, NUMF = 4;
        static readonly string SALUDO = "Bienvenido!";

        // Array of XuzzleSquare views, and empty row & column.
        XuzzleSquare[,] squares = new XuzzleSquare[NUMF, NUMC];

        StackLayout stackLayout;
        AbsoluteLayout absoluteLayout;
        Label timeLabel;
        bool isBusy;

        public HomePage()
        {

            // AbsoluteLayout to host the squares.
            absoluteLayout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,              
            };

            // Create XuzzleSquare's for all the rows and columns.
            string text = "123456789?0C";
            int index = 0;
            Point p = new Point(1,1);

            for (int row = 0; row < NUMF; row++)
            {
                for (int col = 0; col < NUMC; col++)
                {
                    // Instantiate XuzzleSquare.
                    XuzzleSquare square = new XuzzleSquare(text[index], index)
                    {
                        Row = row,
                        Col = col
                    };
                    square.SetLabelFont(60);
                    // Add tap recognition
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
                    {
                        Command = new Command(OnSquareTapped),
                        CommandParameter = square
                    };
                    square.GestureRecognizers.Add(tapGestureRecognizer);

                    // Add it to the array and the AbsoluteLayout.
                    squares[row, col] = square;
                    p.X = col * 80;
                    p.Y = row * 100;
                    absoluteLayout.Children.Add(square, p);
                    index++;
                }
            }


            // Label to display elapsed time.
            timeLabel = new Label
            {
                Text = SALUDO,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Put everything in a StackLayout.
            stackLayout = new StackLayout
            {
                Children = {
                    new StackLayout {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children = {
                            timeLabel
                        }
                    },
                    absoluteLayout
                }
            };

            // And set that to the content of the page.
            this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            this.Content = stackLayout;
        
        }

        void OnSquareTapped(object parameter)
        {
            if (isBusy)
                return;

            isBusy = true;
            XuzzleSquare tappedSquare = (XuzzleSquare)parameter;

            if (timeLabel.Text.Length > 0)
            {
                if (timeLabel.Text[0] == 'B') { timeLabel.Text = ""; }
            }

            if (tappedSquare.normText == "C")
            {
               if (timeLabel.Text.Length > 0) timeLabel.Text = "";
            }else if (tappedSquare.normText == "?")
            {
                DisplayAlert("Ayuda", "Digite los 9 números del teléfono al cual desea llamar, ej.: 2 2690 4000 ó 9 1234 5678 \n Presione C para borrar.", "Aceptar");
            }
            else
            {

                timeLabel.Text += tappedSquare.normText;

                if (timeLabel.Text.Length == 9)
                {
                    try
                    {
                        DependencyService.Get<IPhoneCall>().MakeQuickCall(timeLabel.Text);
                        //timeLabel.Text = SALUDO;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            isBusy = false;

        }
    }
}
