using RecKevinMob.Classes;
using RecKevinMob.ViewsModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecKevinMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalheView : ContentPage
    {

        public Noticias Noticias { get; set; }
        public DetalheViewModel ViewModel { get; set; }
        public DetalheView(Noticias noticias)
        {
            InitializeComponent();
            Noticias = noticias;
            this.ViewModel = new DetalheViewModel(noticias);
            this.BindingContext = ViewModel;
        }

        

        protected override void OnAppearing()
        {
            //base.OnAppearing();
            //MessagingCenter.Subscribe<Restaurante>(this, "Proximo",
            //(msg) =>
            //{
            //    Navigation.PushAsync(new AgendamentoView(msg));
            //});
            base.OnAppearing();
            photoImage.Source = Noticias.PhotoFullPath;
            photoImage.HeightRequest = 200f;
            photoImage.WidthRequest = 200f;
        }

    }
}