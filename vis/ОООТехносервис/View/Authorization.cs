using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ОООТехносервис.Model;
using ОООТехносервис.Classes; // Для сокращения, чтобы не писать постоянно Classes

namespace ОООТехносервис
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            try
            {
                Classes.Helper.DBRequest = new Model.ConnectDBRequest();
                MessageBox.Show("Подключение к БД успешно");
            }

            catch{
                MessageBox.Show("Не удается подключится к БД");
                Environment.Exit(-1);
            }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       
        //Вход в систему по логину и паролю
        private void buttonInput_Click(object sender, EventArgs e)
        {
      

            //Получили данные от пользователя из интерфейса
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;


            ////List<Model.User> users = Classes.Helper.DBRequest.User.ToList();
            /////Один пользователь
            ////Model.User user = null;
            ////// Список отфильтрованных пользователей
            ////////2 Способ
            ////List<Model.User> usersWhere = users.Where(u => u.UserLogin == login && u.UserPassword == password).ToList();
            //////Первый из отфильтрованных
            ////user = usersWhere.FirstOrDefault();


            ////Перебор всех пользователей
            //// 1 Способ
            ////foreach (Model.User u in users)
            ////{
            ////    if (u.UserLogin ==login && u.UserPassword == password)
            ////    {
            ////        user = u;
            ////        break;

            ////    }
            ////}
            //// 3 Способ
            Helper.user = Classes.Helper.DBRequest.User.Where(u => u.UserLogin == login && u.UserPassword == password).FirstOrDefault();

            if (Helper.user != null)
            {
                MessageBox.Show("Нашли. Вы вошли с ролью " + Helper.user.Role.RoleName);
                // обьект окна 
                View.ListRequest listRequest = new View.ListRequest();       // создали дополнительное окно
                this.Hide(); // Временно скрыли окно авторизации this- ссылка на окно, в котором мы находимся
                listRequest.ShowDialog(); // открыть окно списка
                this.Show(); // Показать окно авторизации после закрытия

            }
            else
            {
                MessageBox.Show("Не нашли");
            }



        }
    }
}
