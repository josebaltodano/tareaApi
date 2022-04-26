using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherConcurrencyApp.AppCore.Interfaces;
using WeatherConcurrencyApp.Infrastructure.OpenWeatherClient;
using WeatherConcurrentApp.Domain.Entities;

namespace WeatherConcurrencyApp
{
    public partial class FrmMain : Form
    {
        public IHttpOpenWeatherClientService httpOpenWeatherClient;
        public OpenWeather openWeather;
        List<OpenWeatherCities> cities;
        public FrmMain(IHttpOpenWeatherClientService httpOpenWeatherClient)
        {
            InitializeComponent();
            this.httpOpenWeatherClient = httpOpenWeatherClient;
            //cities = httpOpenWeatherClient.GetCities();
            cities = httpOpenWeatherClient.GetCities(Properties.Resources.city_list);
            //if (cities.Count != 0)
            //{
            //    MessageBox.Show("Test");
            //}
            comboBox1.DataSource = cities.Select(x => x.Name).ToList();

            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            getData(combData);
            comboBox1.AutoCompleteCustomSource = combData;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Run(Request).Wait();
                if(openWeather == null)
                {
                    throw new NullReferenceException("Fallo al obtener el objeto OpeWeather.");
                }
                string imageLocation = httpOpenWeatherClient.GetImage(openWeather);
                WeatherPanel weatherPanel = new WeatherPanel(openWeather, imageLocation);
                flpContent.Controls.Add(weatherPanel);
            }
            catch (Exception)
            {
                
            }
           
        }

        public async Task Request()
        {
            //openWeather = await httpOpenWeatherClient.GetWeatherByCityNameAsync(textBox1.Text);
            openWeather = await httpOpenWeatherClient.GetWeatherByCityNameAsync(comboBox1.SelectedValue.ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void getData(AutoCompleteStringCollection dataCollection)
        {
            foreach (OpenWeatherCities city in cities)
            {
                dataCollection.Add(city.Name);
            }
        }
    }
}
