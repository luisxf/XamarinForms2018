using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            btnBuscar.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO - Lógica do programa
            string cep = txtCep.Text.Trim();

            if (IsValidCEP(cep))
            {
                try { 
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        txtResultado.Text = string.Format("Endereço: {2} - {3} ({0}, {1})", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }
                }
                catch(Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
                }
            }
        }

        private bool IsValidCEP(string cep)
        {
            bool valido = true;
            int NovoCep = 0;

            if(cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 dígitos.", "OK");
                valido = false;
            }


            if (!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto apenas por números.", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
