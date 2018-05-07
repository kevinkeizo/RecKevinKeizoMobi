using RecKevinMob.Classes;
using RecKevinMob.ViewsModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecKevinMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListagemView : ContentPage
    {
       public ListagemViewModel ViewModel { get; set; }
        public ListagemView ()
		{
			InitializeComponent ();
            this.ViewModel = new ListagemViewModel();
            this.BindingContext = ViewModel;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.GetNoticias();

            MessagingCenter.Subscribe<Noticias>(this, "NoticiasSelecionado",
                (msg) =>
                {
                    Navigation.PushAsync(new DetalheView(msg));

                }

                );
            MessagingCenter.Subscribe<Exception>(this, "FalhaListagem",
                (msg) =>
                {
                    DisplayAlert(
                        "Erro",
                        msg.Message,
                        "Ok"
                            );
                }

                );
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Noticias>(this, "NoticiasSelecionado");
            MessagingCenter.Unsubscribe<Exception>(this, "FalhaListagem");
            
        }

    }
}
