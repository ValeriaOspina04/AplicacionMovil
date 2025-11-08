using DBSqlite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DBSqlite
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LlenarDatos();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Usuario Est = new Usuario
                {
                    Nombre = txtNombre.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Edad = int.Parse(txtEdad.Text),
                };

                await App.SQLiteDB.GuardarUsuarioAsync(Est);
                txtNombre.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtEdad.Text = "";
                await DisplayAlert("Registro", "Se guardó de manera exitosa el usuario", "ok");
                LlenarDatos();

            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos ", "OK");
            }
        }

        private async void ListUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Usuario)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtIdUsuario.IsVisible = true;
            btnActualizar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IdUsuario.ToString()))
            {
                var Usuario = await App.SQLiteDB.GetUsuarioByIdAsync(obj.IdUsuario);
                if (Usuario != null)
                {
                    txtIdUsuario.Text = Usuario.IdUsuario.ToString();
                    txtNombre.Text = Usuario.Nombre;
                    txtEmail.Text = Usuario.Email;
                    txtPassword.Text = Usuario.Password;
                    txtEdad.Text = Usuario.Edad.ToString();
                }
            }

        }
        public async void LlenarDatos()
        {
            var UsuarioList = await App.SQLiteDB.GetUsuarioAsync();
            if (UsuarioList != null)
            {
                ListUsuarios.ItemsSource = UsuarioList;
            }
        }


        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }

            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;


        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdUsuario.Text))
            {
                Usuario Est = new Usuario
                {
                    IdUsuario = Convert.ToInt32(txtIdUsuario.Text),
                    Nombre = txtNombre.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Edad = Convert.ToInt32(txtEdad.Text),
                };

                await App.SQLiteDB.GuardarUsuarioAsync(Est);
                await DisplayAlert("Registro", "Se registró de manera exitosa el usuario", "ok");
                txtIdUsuario.Text = "";
                txtNombre.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtEdad.Text = "";

                btnRegistrar.IsVisible = true;
                txtIdUsuario.IsVisible = false;
                btnActualizar.IsVisible = false;
                await DisplayAlert("Actualización", "Se actualizó de manera exitosa el usuario", "ok");
                LlenarDatos();
            }
            else
            {
                await DisplayAlert("Advertencia", "Seleccionar un usuario ", "OK");
            }
        }
    }
}
