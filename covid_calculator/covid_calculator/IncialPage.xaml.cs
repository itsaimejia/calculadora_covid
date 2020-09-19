using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace covid_calculator
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IncialPage : ContentPage
	{
		static double factores = 0;
		static double factorSexo = 0;
		static double factorEdad = 0;
		static double factorPeso = 0;

		static double vHiper = 0;
		static double vDiabetes = 0;
		static double vPulmonar = 0;
		static double vRenal = 0;
		static double vSupresion = 0;

		int banderaSexo = 0;
		int banderaEdad = 0;
		int banderaPeso = 0;
		int banderaHiper = 0;
		int banderaDiabetes = 0;
		int banderaPulmon = 0;
		int banderaRenal = 0;
		int banderaSupresion = 0;


		bool esHombre = false;

		ImageButton hombre1, hombre2, hombre3, hombre4;
		ImageButton mujer1, mujer2, mujer3, mujer4;
		
		public IncialPage()
		{
			InitializeComponent();

			for (int i = 20; i < 100; i++)
			{
				pickerEdad.Items.Add(i.ToString());
			}

			stackPeso.IsEnabled = false;

			LimpiarBotonesPeso();

			SumaFactores();

			// sexo
			cbHombre.CheckedChanged += FactorSexo;
			cbMujer.CheckedChanged += FactorSexo;

			//edad
			pickerEdad.SelectedIndexChanged += FactorEdad;

			//peso
			
			

			//padecimientos
			hiper.CheckedChanged += PadecimientosCheck;
			diabetes.CheckedChanged += PadecimientosCheck;
			pulmon.CheckedChanged += PadecimientosCheck;
			renal.CheckedChanged += PadecimientosCheck;
			supresion.CheckedChanged += PadecimientosCheck;
		}

		




		#region Eventos Factores

		//factor sexo
		void FactorSexo(object sender, CheckedChangedEventArgs e)
		{
			Reset();
			var factor = 0.0;
			if (cbHombre.IsChecked == true)
			{
				cbMujer.IsChecked = false;
				cbHombre.IsChecked = true;
				factor = 1.811368;
				esHombre = true;
				banderaSexo = 1;
				BotonesPeso();
				factorSexo = Math.Log(factor);
				SumaFactores();
			}

			if (cbMujer.IsChecked == true)
			{
				cbHombre.IsChecked = false;
				cbMujer.IsChecked = true;
				esHombre = false;
				BotonesPeso();
			}

			
			
		}
		//factor edad
		private void FactorEdad(object sender, EventArgs e)
		{
			if (pickerEdad.SelectedIndex >= 0)
			{
				var factor = 1.082408;
				var logFactor = Math.Log(factor);
				var edad = double.Parse(pickerEdad.Items[pickerEdad.SelectedIndex]);
				banderaEdad = (esHombre) ? ((edad > 45) ? 1 : 0) : (edad > 53) ? 1 : 0;
				factorEdad = logFactor * edad;
				SumaFactores();
				stackPeso.IsEnabled = true;
			}
			
		}

		//factor peso
		private void FactorPeso(object sender, EventArgs e)
		{
			var factor = 1.282446;
			factorPeso = Math.Log(factor);
			banderaPeso = 1;
			SumaFactores();
		}

		//factor padecimientos
		private void PadecimientosCheck(object sender, CheckedChangedEventArgs e)
		{
			if (hiper.IsChecked)
			{
				banderaHiper = 1;
				vHiper = Math.Log(1.159017);
			}
			else
			{
				banderaHiper = 0;
			}
			if (diabetes.IsChecked)
			{
				banderaDiabetes = 1;
				vDiabetes = Math.Log(1.904794);
			}
			else
			{
				banderaDiabetes = 0;
			}
			if (pulmon.IsChecked)
			{
				banderaPulmon = 1;
				vPulmonar = Math.Log(1.904794);
			}
			else
			{
				banderaPulmon = 0;
			}
			if (renal.IsChecked)
			{
				banderaRenal = 1;
				vRenal = Math.Log(1.904794);
			}
			else
			{
				banderaRenal = 0;
			}
			if (supresion.IsChecked)
			{
				banderaSupresion = 1;
				vSupresion = Math.Log(1.904794);
			}
			else
			{
				banderaSupresion = 0;
			}
			SumaFactores();
		}
		#endregion

		private void SumaFactores()
		{
			var sumaTotalFactores = Math.Log(0.0125288) +
				factorSexo +
				factorEdad +
				factorPeso +
				vHiper +
				vPulmonar +
				vDiabetes +
				vRenal +
				vSupresion;

			var xMenosUno = -1 * sumaTotalFactores;
			var expo = Math.Exp(xMenosUno);
			var expMasUno = 1 + expo;
			var unoEntre = 1 / expMasUno;
			var porCien = unoEntre * 100;
			var probabilidad = Math.Round(porCien);

			var factores = banderaSexo +
				banderaEdad +
				banderaPeso +
				banderaHiper +
				banderaPulmon +
				banderaDiabetes +
				banderaRenal +
				banderaSupresion;

			lblPorcentaje.Text = $"Probabilidad: {probabilidad}\n" +
				$"Número de factores: {factores}";
		}

		

		#region Elementos dinamicos
		private void BotonesPeso()
		{

			if (esHombre)
			{
				LimpiarBotonesPeso();
				hombre1 = new ImageButton
				{
					BackgroundColor = Color.White,
					HeightRequest = 100,
					Source = "hombre1.png"

				};
				stackPeso.Children.Add(hombre1);

				hombre2 = new ImageButton
				{
					BackgroundColor = Color.White,
					HeightRequest = 100,
					Source = "hombre2.png"

				};
				stackPeso.Children.Add(hombre2);

				hombre3 = new ImageButton
				{
					BackgroundColor = Color.White,
					HeightRequest = 100,
					Source = "hombre3.png"


				};
				hombre3.Clicked += FactorPeso;
				stackPeso.Children.Add(hombre3);

				hombre4 = new ImageButton
				{
					BackgroundColor = Color.White,
					HeightRequest = 100,
					Source = "hombre4.png"

				};
				hombre4.Clicked += FactorPeso;
				stackPeso.Children.Add(hombre4);
			}
			else
			{
				LimpiarBotonesPeso();
				mujer1 = new ImageButton
				{
					BackgroundColor = Color.White,
					HeightRequest = 100,
					Source = "mujer1.png"

				};
				stackPeso.Children.Add(mujer1);

				mujer2 = new ImageButton
				{
					BackgroundColor = Color.White,
					HeightRequest = 100,
					Source = "mujer2.png"

				};
				stackPeso.Children.Add(mujer2);

				mujer3 = new ImageButton
				{
					BackgroundColor = Color.White,
					HeightRequest = 100,
					Source = "mujer3.png"

				};
				mujer3.Clicked += FactorPeso;
				stackPeso.Children.Add(mujer3);

				mujer4 = new ImageButton
				{
					BackgroundColor = Color.White,
					HeightRequest = 100,
					Source = "mujer4.png"

				};
				mujer4.Clicked += FactorPeso;
				stackPeso.Children.Add(mujer4);
			}
		}


		#endregion


		#region Actualizacion interfaz
		private void Reset()
		{

			pickerEdad.SelectedIndex = -1;
			banderaSexo = 0;
			banderaEdad = 0;
			banderaPeso = 0;
			banderaHiper = 0;
			banderaDiabetes = 0;
			banderaPulmon = 0;
			banderaRenal = 0;
			banderaSupresion = 0;

		}
		private void LimpiarBotonesPeso()
		{
			if (stackPeso.Children.Count > 0)
			{
				foreach (var btn in stackPeso.Children.ToList())
				{
					stackPeso.Children.Remove(btn);
				}
			}
		} 
		#endregion
	}

}