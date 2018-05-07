using Newtonsoft.Json;
using RecKevinMob.Classes;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecKevinMob.ViewsModels
{
    public class ListagemViewModel : BaseViewModel
    {
        public ObservableCollection<Noticias> Noticias { get; set; }
        const string URL_GET_NOTICIAS = "http://kevinkeizo.somee.com/api/news";



        bool _aguarde;
        Noticias _noticiasSelecionando;

        public Noticias NoticiasSelecionado
        {
            get
            {
                return _noticiasSelecionando;
            }
            set
            {
                _noticiasSelecionando = value;
                if (value != null)
                    MessagingCenter.Send(_noticiasSelecionando, "NoticiasSelecionado");
            }
        }

        public async Task GetNoticias()
        {
            Aguarde = true;
            try
            {
                HttpClient cliente = new HttpClient();
                var resultado = await cliente.GetStringAsync(URL_GET_NOTICIAS);
                var restaurantesJson = JsonConvert.DeserializeObject<NoticiasJson[]>(resultado);

                Noticias.Clear();
                foreach (var restaurantes in restaurantesJson)
                {
                    Noticias.Add(new Noticias
                    {
                        NewsId = restaurantes.newsid,
                        Title = restaurantes.title,
                        Author = restaurantes.author,
                        ResumeText = restaurantes.resumetext,
                        PostDate = restaurantes.postdate,
                        Photo = restaurantes.photo.ToString(),
                        Text = restaurantes.text
                    });
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send<Exception>(ex, "FalhaListagem");
            }
            Aguarde = false;

        }

        public bool Aguarde
        {
            get { return _aguarde; }
            set
            {
                _aguarde = value;
                OnPropertyChanged();
            }
        }
        public ListagemViewModel()
        {
            Noticias = new ObservableCollection<Noticias>();
        }

    }

    public class NoticiasJson
    {
        public string author { get; set; }
        public string resumetext { get; set; }
        public string postdate { get; set; }
        public string photo { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public int newsid { get; set; }
    }

}
