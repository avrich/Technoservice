using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ОООТехносервис.Classes;

namespace ОООТехносервис.View
{
    public partial class ListRequest : Form
    {
        public ListRequest()
        {
            InitializeComponent();

        }

        
        // Метод отображения заявок с учетом фильтрации
        public void ShowRequest()
        {

            // Получить все заявки из БД
            List<Model.Request> list = Classes.Helper.DBRequest.Request.ToList();
            // Анализ рали
            switch (Helper.user.UserRoleid) 
            { 
                case 1:
                        list = list.Where(cl => cl.RequestClientid == Helper.user.Userid).ToList();
              
                    break;
                case 2:
                    list = list.Where(m => m.RequestMasterid == Helper.user.Userid).ToList();
                    buttonAddRequest.Visible = true;
                    break;
                case 3:
                    buttonAddRequest.Visible = buttonEdit.Visible = true;
                    break;
                case 4:
                    buttonReport.Visible = true;
                    break;
            }
            //Фильтрация по заявкам для заказчика
            if (Helper.user.UserRoleid == 1) // Заказчик (Клиент)
            {
                list = list.Where(cl=>cl.RequestClientid==Helper.user.Userid).ToList();
            }
            


            // Фильтровать по статусу
            int numberStatus = (int)comboBoxStatus.SelectedIndex;
            if (numberStatus > 0 )
            {
                list = list.Where (r=>r.RequestStatusid == numberStatus).ToList();
            }


            // Поиск по номеру
            if (textBoxNumber.Text!= "")
            {
                list = list.Where(r => r.Requestid.ToString().Contains(textBoxNumber.Text)).ToList();
            }


            this.dataGridViewRequest.Rows.Clear();
            int i = 0; //Счетчик строк

            foreach (Model.Request request in list)
            {
                this.dataGridViewRequest.Rows.Add();
                dataGridViewRequest.Rows[i].Cells[0].Value = request.Requestid;
                dataGridViewRequest.Rows[i].Cells[1].Value = request.RequestData;
                dataGridViewRequest.Rows[i].Cells[2].Value = request.Device.DeviceName;
                dataGridViewRequest.Rows[i].Cells[3].Value = request.User.UserFullName;
                dataGridViewRequest.Rows[i].Cells[4].Value = request.User1.UserFullName;
                dataGridViewRequest.Rows[i].Cells[5].Value = request.Status.StatusName;
                dataGridViewRequest.Rows[i].Cells[6].Value = request.Phase.PhaseName;
                i++;
            }



            //// Отобразить список заявок в сетке
            //dataGridViewRequest.DataSource = list;
        }


        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Отобразить список заявок при загрузки окна
        private void ListRequest_Load(object sender, EventArgs e)
        {
            // заполняем статусами из БД 
            List<Model.Status> statuses = Classes.Helper.DBRequest.Status.ToList();
            Model.Status status = new Model.Status();
            status.Statusid = 0;
            status.StatusName = "Все статусы";
            //Добавляем новый статус к статусам из бд
            statuses.Insert(0, status);

            // пЕРЕНОСИМ НОВЫЙ СТАТУС В COMBOBOX
            comboBoxStatus.DataSource = statuses;

            //Какое поле может видеть пользователь
            comboBoxStatus.DisplayMember = "StatusName";
            comboBoxStatus.ValueMember = "Statusid";
            comboBoxStatus.SelectedIndex = 0;

            // Обращение к методу Отобразить список
            ShowRequest();

        }


        // Выбор статуса для фильтрации
        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowRequest();
        }
        /// <summary>
        ///     Ввод номера заявки для поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {
            ShowRequest();
        }

        // Добавить заявку в роли оператор
        private void buttonAddRequest_Click(object sender, EventArgs e)
        {
            View.ChangeRequest changeRequest = new View.ChangeRequest(0);
            this.Hide();
            changeRequest.ShowDialog();
            this.Show();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        { 
            // Проверили, что есть выбранная заявка
            if (dataGridViewRequest.SelectedRows.Count > 0)
            {// Номер выбранной заявки
                int id = (int)dataGridViewRequest.CurrentRow.Cells[0].Value;
                // Открыли окно и передали в него номер выбранной заявки
                //MessageBox.Show("Номер" + dataGridViewRequest.CurrentRow.Cells[0].Value);
                View.ChangeRequest changeRequest = new View.ChangeRequest();
                this.Hide();
                changeRequest.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Не выбрана заявка для редактирования");
            }


            
        }

       

        private void ListRequest_Activated(object sender, EventArgs e)
        {

            ShowRequest();
        }
    }
}
