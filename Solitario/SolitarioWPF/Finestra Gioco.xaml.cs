﻿using SolitarioCroce;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Solitario_A_Croce_WPF
{
    /// <summary>
    /// Logica di interazione per Finestra_Gioco.xaml
    /// </summary>
    public partial class Finestra_Gioco : Window
    {
        Gioco _giocoSolitario;
        
        bool _applicaSwitch;
        Button _bottoneConCuiFareSwitch;
        Carta _cartaSelezionataDalPozzo;



        public Finestra_Gioco()
        {
            InitializeComponent();
            
            //mescolo il mazzo e inizializzo il gioco.
            Mazzo mazzo = new Mazzo();
            mazzo.MescolaMazzo();

            _giocoSolitario = new Gioco(mazzo);

            InizializzaCarteNellaPartita();
            _bottoneConCuiFareSwitch = new Button();
            

        }


        private void btnMazzo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _giocoSolitario.PescaCarta();
                _bottoneConCuiFareSwitch.Name = "btnPozzo";


                Carta carta;
                carta = _giocoSolitario.Pozzo[_giocoSolitario.Pozzo.Count - 1];
                string valCarta = carta.ValoreCarta.ToString();
                string semCarta = carta.SemeCarta.ToString();


                //animazione
                Storyboard animazione = (Storyboard)FindResource("Storyboard_DAMazzo_APozzo");
                animazione.Begin();
                Img_DaMazzo_APozzo.Source= new BitmapImage(new Uri($"carte/{valCarta}_{semCarta}.jpg", UriKind.Relative));
                // 
                Storyboard_Completed();



            }
            catch
            {
                MessageBox.Show("Il mazzo è finito non puoi pescare altre carte!","Mazzo Terminato",MessageBoxButton.OK,MessageBoxImage.Warning);
                btnMazzo.IsEnabled= false;
                btnMazzo.Visibility = Visibility.Collapsed;
                rettangoloMazzo.Visibility = Visibility.Collapsed;
            }
        }

        private void Storyboard_Completed()
        {
            _giocoSolitario.PescaCarta();
            _bottoneConCuiFareSwitch.Name = "btnPozzo";


            Carta carta;
            carta = _giocoSolitario.Pozzo[_giocoSolitario.Pozzo.Count - 1];
            string valCarta = carta.ValoreCarta.ToString();
            string semCarta = carta.SemeCarta.ToString();

            Thread.Sleep(2000);
            Img_btnPozzo.Source = null;
            Img_btnPozzo.Source = new BitmapImage(new Uri($"carte/{valCarta}_{semCarta}.jpg", UriKind.Relative));
        }
    
       

        private void InizializzaCarteNellaPartita()
        {
            List<Mazzetto> listaCroci = new List<Mazzetto>();
            listaCroci = _giocoSolitario.Croci.ToList();

            Carta CartaMazzetto1;
            
            CartaMazzetto1 = listaCroci[0].VisualizzaPrimaCarta;
            string valCartaMazz1 = CartaMazzetto1.ValoreCarta.ToString();
            string semCartaMazz1 = CartaMazzetto1.SemeCarta.ToString();
            Img_btnMazzetto1.Source = new BitmapImage(new Uri($"carte/{valCartaMazz1}_{semCartaMazz1}.jpg", UriKind.Relative));

            Carta CartaMazzetto2;
            
            CartaMazzetto2 = listaCroci[1].VisualizzaPrimaCarta;
            string valCartaMazz2 = CartaMazzetto2.ValoreCarta.ToString();
            string semCartaMazz2 = CartaMazzetto2.SemeCarta.ToString();
            Img_btnMazzetto2.Source = new BitmapImage(new Uri($"carte/{valCartaMazz2}_{semCartaMazz2}.jpg", UriKind.Relative));

            Carta CartaMazzetto3;
            
            CartaMazzetto3 = listaCroci[2].VisualizzaPrimaCarta;
            string valCartaMazz3 = CartaMazzetto3.ValoreCarta.ToString();
            string semCartaMazz3 = CartaMazzetto3.SemeCarta.ToString();
            Img_btnMazzetto3.Source = new BitmapImage(new Uri($"carte/{valCartaMazz3}_{semCartaMazz3}.jpg", UriKind.Relative));

            Carta CartaMazzetto4;
            
            CartaMazzetto4 = listaCroci[3].VisualizzaPrimaCarta;
            string valCartaMazz4 = CartaMazzetto4.ValoreCarta.ToString();
            string semCartaMazz4 = CartaMazzetto4.SemeCarta.ToString();
            Img_btnMazzetto4.Source = new BitmapImage(new Uri($"carte/{valCartaMazz4}_{semCartaMazz4}.jpg", UriKind.Relative));

            Carta CartaMazzetto5;
            
            CartaMazzetto5 = listaCroci[4].VisualizzaPrimaCarta;
            string valCartaMazz5 = CartaMazzetto5.ValoreCarta.ToString();
            string semCartaMazz5 = CartaMazzetto5.SemeCarta.ToString();
            Img_btnMazzetto5.Source = new BitmapImage(new Uri($"carte/{valCartaMazz5}_{semCartaMazz5}.jpg", UriKind.Relative));
        }

        private void btnPozzo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _cartaSelezionataDalPozzo = _giocoSolitario.Pozzo[_giocoSolitario.Pozzo.Count-1];
                _applicaSwitch = true;
            }
            catch
            {
                MessageBox.Show("Non ci sono carte nel pozzo!", "Pozzo Vuoto", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        
        private bool _primoBottoneSelezionato = false;
        private Button _primoBottone;

        private void btnMazzetto_Click(object sender, RoutedEventArgs e)
        {
            Button bottoneCliccato = (Button)sender;
            string nomeBottone = bottoneCliccato.Name;

            // Se è stato già selezionato un  bottone
            if (_primoBottoneSelezionato)
            {
                string idMazzettoDestinazione = TrovaIdMazzettoDaNomeBottone(nomeBottone);

                try
                {
                    // Sposta la carta solo se ho selezionato un posto diverso da qwuello di partenza.
                    if (idMazzettoDestinazione != null && idMazzettoDestinazione != TrovaIdMazzettoDaNomeBottone(_primoBottone.Name))
                    {
                        _giocoSolitario.SpostaCarte(_cartaSelezionataDalPozzo, idMazzettoDestinazione);



                        //aggiornare la SOURCE DELLA CARTAAAA QUII DENTRO 
                    }
                    else
                    {
                        MessageBox.Show("Seleziona un mazzetto diverso per lo spostamento della carta.", "Avviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante lo scambio della carta: {ex.Message}", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Resetta lo stato della selezione
                _primoBottoneSelezionato = false;
                _primoBottone = null;
            }
            else  // Se è il primo bottone selezionato, memorizza il bottone e imposta il flag a true
            {
                _primoBottone = bottoneCliccato;
                _primoBottoneSelezionato = true;

                string nome = _primoBottone.Name;
                string id = TrovaIdMazzettoDaNomeBottone(nome);
                Mazzetto[] croci = _giocoSolitario.Croci;
                bool trovato = false;
                foreach (Mazzetto mazzetto in croci)
                {
                    if (mazzetto.Id == id)
                    {
                        _cartaSelezionataDalPozzo = mazzetto.VisualizzaPrimaCarta;
                        trovato = true;
                        break;
                    }
                }
                if (!trovato)
                {
                    Mazzetto[] basi = _giocoSolitario.Basi;
                    foreach (Mazzetto mazzetto in basi)
                    {
                        if (mazzetto.Id == id)
                        {
                            _cartaSelezionataDalPozzo = mazzetto.VisualizzaPrimaCarta;
                            trovato = true;
                            break;
                        }
                    }
                    if (!trovato)
                    {
                        _cartaSelezionataDalPozzo = _giocoSolitario.Pozzo[_giocoSolitario.Pozzo.Count - 1];
                    }
                }


            }
        }

        private string TrovaIdMazzettoDaNomeBottone(string nomeBottone)
        {
            switch (nomeBottone)
            {
                case "btnMazzetto1": return "C1";
                case "btnMazzetto2": return "C2";
                case "btnMazzetto3": return "C3";
                case "btnMazzetto4": return "C4";
                case "btnMazzetto5": return "C5";
                default: return null;
            }
        }

        private void btnMazzetto_Click1(object sender, RoutedEventArgs e)
        {
            if (_applicaSwitch)
            {
                //caso in cui metto la carta dal pozzo a uno dei mazzetti.

                Button bottoneCliccato = (Button)sender;
                string nomeBottoneCliccato= bottoneCliccato.Name;
                string[] arrayInfoBottone = nomeBottoneCliccato.Split("btn");
                string nomeBottone = arrayInfoBottone[1]; 

                //scopro quale è il nome del mazzetto di cui prendre la carta.
                
                

                Mazzetto[] listaCroci = _giocoSolitario.Croci;

                if (nomeBottone == "Mazzetto1")
                {
                    _giocoSolitario.SpostaCarte(_giocoSolitario.Pozzo.Last(), "C1");
                }
                else if (nomeBottone == "Mazzetto2")
                {
                    _giocoSolitario.SpostaCarte(_giocoSolitario.Pozzo.Last(), "C2");
                }
                else if (nomeBottone == "Mazzetto3")
                {
                    _giocoSolitario.SpostaCarte(_giocoSolitario.Pozzo.Last(), "C3");
                }
                else if (nomeBottone == "Mazzetto4")
                {
                    _giocoSolitario.SpostaCarte(_giocoSolitario.Pozzo.Last(), "C4");
                }
                else if (nomeBottone == "Mazzetto5")
                {
                    _giocoSolitario.SpostaCarte(_giocoSolitario.Pozzo.Last(), "C5");
                }
            }
            else
            {
                //caso in cui metto la carta dal mazzetto a unaltro mazzetto.

            }

        }

        private void btnFinePartita_Click(object sender, RoutedEventArgs e)
        {
            _giocoSolitario.Resa();
        }
    }
}
