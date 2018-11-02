using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ScheduleXamarinAndroid.Controls
{
    public class ScheduleControl : ContentView
    {
        DateTime dtinicio;
        DateTime dtfinal;
        public DateTime principaldate;
        StackLayout MainLayout;
        StackLayout DiaActual;
        Label labelWeeks;
        Grid gridhours;
        Color colorHeader = Color.FromHex("#EEEEF2"); //Color.FromHex("#2A579A");
        Color cText = Color.FromHex("#8F8F91");
        Color CBlack = Color.FromHex("#000000");
        Color cSelected = Color.FromHex("#FFFFFF");
        Color cButton = Color.FromHex("#DBDBE3");
        public Grid gridminutes;
        ScrollView scrollhours;
        CultureInfo ci;
        Grid gridday = new Grid();
        Button botonback;
        Button botonnext;

        public ScheduleControl(DateTime now )
        {
            principaldate = now;
            ci = new CultureInfo("es-US");

            MainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 0, 0, 0),
                Margin = new Thickness(0, 0, 0, 0),
                Spacing = 0,
                BackgroundColor = colorHeader
            };

            generateDay(principaldate);
            var stackweek = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = colorHeader,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            labelWeeks = new Label();
            labelWeeks.Text = String.Format("{0} {1} - {2} {3}", dtinicio.ToString("MMM"), dtinicio.Day.ToString(), dtfinal.ToString("MMM"), dtfinal.Day.ToString());
            labelWeeks.FontSize = 20;
            labelWeeks.HorizontalOptions = LayoutOptions.Center;
            labelWeeks.HorizontalTextAlignment = TextAlignment.Center;
            labelWeeks.VerticalTextAlignment = TextAlignment.Center;
            labelWeeks.TextColor = CBlack;

            botonback = new Button
            {
                Text = "<",
                FontSize = 15,
                BorderRadius = 200,
                WidthRequest = 40,
                HeightRequest = 40,
                BackgroundColor = cButton,
                BorderWidth = 2,
                BorderColor = Color.FromHex("#CCCCD7"),
                //Image = "afft"

            };
            botonnext = new Button
            {
                Text = ">",
                FontSize = 15,
                BorderRadius = 200,
                WidthRequest = 40,
                HeightRequest = 40,
                BackgroundColor = cButton,
                BorderWidth = 2,
                BorderColor = Color.FromHex("#CCCCD7"),
               // Image = "next"
            };
            DiaActual = new StackLayout();
            DiaActual.BackgroundColor = Color.FromHex("#FFFFFF");
            var labelDiaActual = new Label
            {
                Text = string.Format("HOY {0}, {1} {2}", PrimeraLetraMayúscula(ci.DateTimeFormat.GetDayName(principaldate.DayOfWeek)), ci.DateTimeFormat.GetMonthName(principaldate.Month), principaldate.Day).ToUpper(),
                FontSize = 20,
                Margin = new Thickness(10, 0, 0, 0),
                TextColor = Color.FromHex("#539DE7")
            };
            DiaActual.Children.Add(labelDiaActual);
            botonnext.Clicked += NextDate;
            botonback.Clicked += BackDate;
            stackweek.Children.Add(botonback);
            stackweek.Children.Add(labelWeeks);
            stackweek.Children.Add(botonnext);
            MainLayout.Children.Add(stackweek);
            MainLayout.Children.Add(gridday);
            MainLayout.Children.Add(DiaActual);
            MainLayout.Children.Add(GenerateHoursDay());
            Content = MainLayout;
        }

        void NextDate(object sender, EventArgs e)
        {
            showDays(dtinicio.AddDays(8));
        }

        void BackDate(object sender, EventArgs e)
        {
            showDays(dtfinal.AddDays(-8));
        }

        private void generateDay(DateTime now)
        {
            try
            {
                gridday = new Grid();
                gridday.RowDefinitions.Add(new RowDefinition { Height = 65 });
                gridday.BackgroundColor = colorHeader;
                gridday.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridday.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridday.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridday.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridday.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridday.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridday.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridday.ColumnSpacing = 0;
                gridday.RowSpacing = 0;
                if (now.DayOfWeek == DayOfWeek.Sunday)
                { }
                else if (now.DayOfWeek == DayOfWeek.Monday)
                {
                    now = now.AddDays(-1);
                }
                else if (now.DayOfWeek == DayOfWeek.Tuesday)
                {
                    now = now.AddDays(-2);
                }
                else if (now.DayOfWeek == DayOfWeek.Wednesday)
                {
                    now = now.AddDays(-3);
                }
                else if (now.DayOfWeek == DayOfWeek.Thursday)
                {
                    now = now.AddDays(-4);
                }
                else if (now.DayOfWeek == DayOfWeek.Friday)
                {
                    now = now.AddDays(-5);
                }
                else if (now.DayOfWeek == DayOfWeek.Saturday)
                {
                    now = now.AddDays(-6);
                }
                this.dtinicio = now;
                this.dtfinal = now.AddDays(6);
                int i = 0;
                gridday.Children.Clear();
                while (now.Date <= dtfinal.Date)
                {
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += async (s, e) => {
                        try
                        {
                            var griddays = (Grid)MainLayout.Children[1];
                            ((StackLayout)griddays.Children[0]).BackgroundColor = colorHeader;
                            ((Label)((StackLayout)griddays.Children[0]).Children[1]).TextColor = cText;
                            ((Label)((StackLayout)griddays.Children[0]).Children[2]).TextColor = cText;
                            ((StackLayout)griddays.Children[1]).BackgroundColor = colorHeader;
                            ((Label)((StackLayout)griddays.Children[1]).Children[1]).TextColor = cText;
                            ((Label)((StackLayout)griddays.Children[1]).Children[2]).TextColor = cText;
                            ((StackLayout)griddays.Children[2]).BackgroundColor = colorHeader;
                            ((Label)((StackLayout)griddays.Children[2]).Children[1]).TextColor = cText;
                            ((Label)((StackLayout)griddays.Children[2]).Children[2]).TextColor = cText;
                            ((StackLayout)griddays.Children[3]).BackgroundColor = colorHeader;
                            ((Label)((StackLayout)griddays.Children[3]).Children[1]).TextColor = cText;
                            ((Label)((StackLayout)griddays.Children[3]).Children[2]).TextColor = cText;
                            ((StackLayout)griddays.Children[4]).BackgroundColor = colorHeader;
                            ((Label)((StackLayout)griddays.Children[4]).Children[1]).TextColor = cText;
                            ((Label)((StackLayout)griddays.Children[4]).Children[2]).TextColor = cText;
                            ((StackLayout)griddays.Children[5]).BackgroundColor = colorHeader;
                            ((Label)((StackLayout)griddays.Children[5]).Children[1]).TextColor = cText;
                            ((Label)((StackLayout)griddays.Children[5]).Children[2]).TextColor = cText;
                            ((StackLayout)griddays.Children[6]).BackgroundColor = colorHeader;
                            ((Label)((StackLayout)griddays.Children[6]).Children[1]).TextColor = cText;
                            ((Label)((StackLayout)griddays.Children[6]).Children[2]).TextColor = cText;
                            var stackl = (StackLayout)s;
                            stackl.BackgroundColor = cSelected;
                            ((Label)stackl.Children[1]).TextColor = CBlack;
                            ((Label)stackl.Children[2]).TextColor = CBlack;
                            ((Label)stackl.Children[3]).TextColor = CBlack;
                            var day = ((Label)stackl.Children[2]).Text;
                            var mes = ((Label)stackl.Children[4]).Text;
                            var annio = ((Label)stackl.Children[5]).Text;
                            var strdt = String.Format("{0:00}/{1:00}/{2}", int.Parse(day), int.Parse(mes), annio);
                            DateTime dt = DateTime.ParseExact(strdt, "dd/MM/yyyy", ci);
                            string hoy = "";
                            principaldate = dt;
                            if (dt.Day == DateTime.Now.Day && dt.Month == DateTime.Now.Month && dt.Year == DateTime.Now.Year)
                            {
                                hoy = "HOY";
                            }
                            ((Label)DiaActual.Children[0]).Text = string.Format("{0} {1}, {2} {3}", hoy, PrimeraLetraMayúscula(ci.DateTimeFormat.GetDayName(dt.DayOfWeek)), ci.DateTimeFormat.GetMonthName(dt.Month), dt.Day).ToUpper();
                            gridminutes.Children.Clear();

                        }
                        catch (Exception ex) { }

                    };
                    var stack = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical
                    };
                    stack.GestureRecognizers.Add(tapGestureRecognizer);
                    BoxView boxview = new BoxView();
                    boxview.BackgroundColor = colorHeader;
                    boxview.HeightRequest = 4;
                    boxview.VerticalOptions = LayoutOptions.End;
                    boxview.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stack.Children.Add(boxview);
                    stack.Spacing = 0;
                    var labeldia = new Label();
                    var labelnumdia = new Label();
                    var labelannio = new Label();
                    var labelmes = new Label();
                    labeldia.VerticalTextAlignment = TextAlignment.Start;
                    labeldia.HorizontalTextAlignment = TextAlignment.Center;
                    labeldia.Text = PrimeraLetraMayúscula(ci.DateTimeFormat.GetDayName(now.DayOfWeek).Substring(0, 3));
                    labelnumdia.Text = now.Day.ToString();
                    labelannio.Text = now.Year.ToString();
                    labelmes.Text = now.Month.ToString();
                    labelnumdia.VerticalTextAlignment = TextAlignment.Center;
                    labelnumdia.HorizontalTextAlignment = TextAlignment.Center;
                    labeldia.TextColor = cText;
                    labelnumdia.TextColor = cText;
                    labelannio.TextColor = colorHeader;
                    labelmes.TextColor = colorHeader;
                    labeldia.FontSize = 15;
                    labelnumdia.FontSize = 15; ;
                    labelnumdia.Margin = new Thickness(0, 0, 0, 0);
                    if (now.Date == DateTime.Now.Date)
                    {
                        stack.BackgroundColor = cSelected;
                        labeldia.TextColor = CBlack;
                        labelnumdia.TextColor = CBlack;
                    }
                    var labelnumjob = new Label();
                    labelnumjob.FontAttributes = FontAttributes.Bold;
                    labelnumjob.VerticalTextAlignment = TextAlignment.Start;
                    labelnumjob.HorizontalTextAlignment = TextAlignment.Center;
                    labelnumjob.VerticalOptions = LayoutOptions.Start;
                    labelnumjob.Margin = new Thickness(0, 0, 0, 0);
                    labelnumjob.FontSize = 15;
                    stack.Children.Add(labeldia);
                    stack.Children.Add(labelnumdia);
                    stack.Children.Add(labelnumjob);
                    stack.Children.Add(labelmes);
                    stack.Children.Add(labelannio);
                    try
                    {
                        gridday.Children.Add(stack, i, 0);
                    }
                    catch { }
                    i++;
                    now = now.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                gridday = new Grid();
            }
        }

        private async void showDays(DateTime now)
        {
            try
            {
                generateDay(now);
                MainLayout.Children[1] = gridday;
                labelWeeks.Text = String.Format("{0} {1} - {2} {3}", this.dtinicio.ToString("MMM"), this.dtinicio.Day.ToString(), this.dtfinal.ToString("MMM"), this.dtfinal.Day.ToString());
                labelWeeks.TextColor = CBlack;
            }
            catch (Exception ex)
            {
            }
        }

        private ScrollView GenerateHoursDay()
        {
            InitGridHours();
            scrollhours = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                Margin = new Thickness(0, 0, 0, 0),
                Padding = new Thickness(0, 0, 0, 0),
            };
            var stackhours = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(0, 0, 0, 0),
                Padding = new Thickness(0, 0, 0, 0)
            };
            gridhours.Children.Add(GenerateMinutesDay());
            stackhours.Children.Add(gridhours);
            scrollhours.Content = stackhours;
            return scrollhours;
        }

        private void InitGridHours()
        {
            if (gridhours == null)
            {
                gridhours = new Grid();
                gridhours.BackgroundColor = Color.FromRgb(255, 255, 255);
                gridhours.ColumnDefinitions.Add(new ColumnDefinition { Width = 80 });
                gridhours.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridhours.HorizontalOptions = LayoutOptions.Center;
                for (var i = 0; i < 24; i++)
                {
                    gridhours.RowDefinitions.Add(new RowDefinition { Height = 100 });
                }
                gridhours.ColumnSpacing = 0;
                gridhours.RowSpacing = 0;
                gridhours.Margin = new Thickness(0, 0, 0, 0);
                gridhours.Padding = new Thickness(0, 0, 0, 0);
            }

            for (var i = 0; i < 24; i++)
            {
                gridhours.Children.Add(GenerateBorderGrid(i, 0, 1, 6, false, "#C2C3C9"));
            }

            for (var i = 0; i < 24; i++)
            {
                var labelHour = new Label();
                if (i < 12)
                {
                    labelHour.Text = "" + i + " AM";
                }
                else
                {
                    if (i == 12)
                    {
                        labelHour.Text = "12 PM";
                    }
                    else
                    {
                        labelHour.Text = (i - 12) + " PM";
                    }
                }

                labelHour.HorizontalTextAlignment = TextAlignment.End;
                labelHour.VerticalTextAlignment = TextAlignment.Start;
                labelHour.Margin = new Thickness(0, 0, 8, 0);
                gridhours.Children.Add(labelHour, 0, i);
            }
        }

        public Grid GenerateMinutesDay()
        {
            gridminutes = new Grid();
            gridminutes.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            for (int i = 0; i < 288; i++)
            {
                gridminutes.RowDefinitions.Add(new RowDefinition { Height = 8.3333 });
            }
            gridminutes.RowSpacing = 0;
            gridminutes.ColumnSpacing = 0;
            gridminutes.WidthRequest = gridhours.Width;
            Grid.SetRow((BindableObject)gridminutes, 0);
            Grid.SetRowSpan((BindableObject)gridminutes, 24);
            Grid.SetColumn((BindableObject)gridminutes, 1);
            Grid.SetColumnSpan((BindableObject)gridminutes, 5);

            return gridminutes;
        }

        public BoxView GenerateBorderGrid(int row, int col, int rowspan, int colspan, bool vertical = true, string color = "#007ACC")
        {
            BoxView boxview = new BoxView();
            boxview.BackgroundColor = Color.FromHex(color);

            if (vertical)
            {
                boxview.WidthRequest = 1;
                boxview.VerticalOptions = LayoutOptions.FillAndExpand;
                boxview.HorizontalOptions = LayoutOptions.End;
            }
            else
            {
                boxview.VerticalOptions = LayoutOptions.End;
                boxview.HorizontalOptions = LayoutOptions.FillAndExpand;
                boxview.HeightRequest = 1;
            }

            Grid.SetRow((BindableObject)boxview, row);
            Grid.SetRowSpan((BindableObject)boxview, rowspan);
            Grid.SetColumn((BindableObject)boxview, col);
            Grid.SetColumnSpan((BindableObject)boxview, colspan);

            return boxview;
        }

        public StackLayout GenerateRowSpanandColSpan(List<Models.Job> id, string texto, string textohoras, int row, int col, int rowspan, int colspan, Color color, bool vertical = true, bool tipe = false)
        {
            var labelHoras = new Label
            {
                Text = textohoras,
                TextColor = Color.FromHex("#000000"),
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 8,
                IsVisible = false
            };
            var labelEvent = new Label
            {
                Text = texto,
                TextColor = Color.FromHex("#FFFFFF"),
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                Margin = new Thickness(5, 5, 5, 5)
            };
            var tapGestureRecognizer = new TapGestureRecognizer();
            var stackevent = new StackLayout
            {
                BackgroundColor = color,
                Margin = new Thickness(1, 1, 1, 1),
                Padding = new Thickness(0, 0, 0, 0)
            };
            var listdetails = new ListView()
            {
                ItemsSource = id,
                IsVisible = false
            };
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                var stacktemp = (StackLayout)s;
                List<Models.Job> idjob = (List<Models.Job>)((ListView)stacktemp.Children[2]).ItemsSource;
            };
            stackevent.GestureRecognizers.Add(tapGestureRecognizer);
            if (vertical)
            {
                if (!tipe)
                {
                    stackevent.Orientation = StackOrientation.Vertical;
                    stackevent.VerticalOptions = LayoutOptions.FillAndExpand;
                    stackevent.HorizontalOptions = LayoutOptions.FillAndExpand;
                }
                else
                {
                    stackevent.Orientation = StackOrientation.Vertical;
                    stackevent.VerticalOptions = LayoutOptions.End;
                    stackevent.HorizontalOptions = LayoutOptions.FillAndExpand;
                }
            }
            else
            {
                stackevent.Orientation = StackOrientation.Vertical;
                stackevent.HorizontalOptions = LayoutOptions.FillAndExpand;
                stackevent.VerticalOptions = LayoutOptions.FillAndExpand;
            }
            stackevent.Children.Add(labelHoras);
            stackevent.Children.Add(labelEvent);
            stackevent.Children.Add(listdetails);
            Grid.SetRow((BindableObject)stackevent, row);
            Grid.SetRowSpan((BindableObject)stackevent, rowspan);
            Grid.SetColumn((BindableObject)stackevent, col);
            Grid.SetColumnSpan((BindableObject)stackevent, colspan);
            return stackevent;
        }

        public string PrimeraLetraMayúscula(string palabra)
        {
            string cadena, primera;
            cadena = "";
            primera = palabra[0].ToString();//Aqui guardo la 1ra letra 

            primera = primera.ToUpper();//Covierto a mayúscula 

            for (int i = 1; i < palabra.Length; i++) //en este ciclo se coge la palabra menos la 1ra letra 
            {
                cadena = cadena + palabra[i];
            }

            cadena = primera + cadena;

            return cadena;
        }
    }
}
