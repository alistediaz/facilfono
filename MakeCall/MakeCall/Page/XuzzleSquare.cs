using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MakeCall.Page
{
    class XuzzleSquare : ContentView
    {
        Label label;
        public string normText;

        public XuzzleSquare(char normChar, int index)
        {
            this.Index = index;
            this.normText = normChar.ToString();

            // A Frame surrounding two Labels.
            label = new Label
            {
                Text = " " + this.normText + " ",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            this.Padding = new Thickness(3);
            this.Content = new Frame
            {
                OutlineColor = Color.Accent,
                Padding = new Thickness(5, 5, 5, 0),
                Content = new StackLayout
                {
                    Spacing = 0,
                    Children = {
                        label
                    }
                }
            };

            // Don't let touch pass us by.
            this.BackgroundColor = Color.Transparent;
        }

        // Retain current Row and Col position.
        public int Index { private set; get; }

        public int Row { set; get; }

        public int Col { set; get; }

        public void SetLabelFont(double fontSize)/*, FontAttributes attributes)*/
        {
            label.FontSize = fontSize;
            //label.FontAttributes = attributes;
        }
    }
}
