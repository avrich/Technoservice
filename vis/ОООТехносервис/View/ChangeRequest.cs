using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ОООТехносервис.Classes;
using ОООТехносервис.Model;

namespace ОООТехносервис.View
{
    public partial class ChangeRequest : Form
    {
        Model.Request Request; // Глобальная для окна


        public ChangeRequest()
        {
            InitializeComponent();
        }

        public ChangeRequest(int id) // конструктор с параметром. Параметром является номер выбранной заявки
        {
            InitializeComponent();
            
           
           if(id>0)
            {
                Request = Helper.DBRequest.Request.Where(r=>r.Requestid== id).FirstOrDefault();
            }
            else
            {
                Request = null;
            }
           // MessageBox.Show(Request.RequestData.ToLongDateString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void ChangeRequest_Load(object sender, EventArgs e)
        {
            // Настройка всех комбобоксов
            //textBoxDate.Text = DateTime.Now.ToShortDateString();

            //textBoxTime.Text = "0";


            //List<Model.Device> devices = Helper.DBRequest.Device.ToList();
            //comboBoxDevice.DataSource = devices;
            comboBoxDevice.DataSource = Helper.DBRequest.Device.ToList();
            comboBoxDevice.DisplayMember = "DeviceName";
            comboBoxDevice.ValueMember =  "Deviceid";

            comboBoxProblem.DataSource = Helper.DBRequest.Defect.ToList();
            comboBoxProblem.DisplayMember = "DefectName";
            comboBoxProblem.ValueMember = "Defectid";

            comboBoxClient.DataSource = Helper.DBRequest.User.Where(u=>u.UserRoleid == 1).ToList();
            comboBoxClient.DisplayMember = "UserFullName";
            comboBoxClient.ValueMember = "Userid";

            comboBoxStatus.DataSource = Helper.DBRequest.Status.ToList();
            comboBoxStatus.DisplayMember = "StatusName";
            comboBoxStatus.ValueMember = "Statusid";
            comboBoxStatus.SelectedValue = 1;

            if (Request == null)
            {
                textBoxDate.Text = DateTime.Now.ToShortDateString();
                comboBoxStatus.SelectedValue = 1;
                comboBoxPhase.SelectedValue = 3;
                textBoxTime.Text = "0";

            }
            else
            {
                comboBoxDevice.SelectedValue = Request.RequestDeviceid;
                comboBoxProblem.SelectedValue = Request.RequestDefectid;
                comboBoxClient.SelectedValue = Request.RequestClientid;
                comboBoxMaster.SelectedValue = Request.RequestMasterid;
                comboBoxPriority.SelectedValue = Request.RequestPrioryid;
                comboBoxStatus.SelectedValue = Request.RequestStatusid;
                comboBoxPhase.SelectedValue = Request.RequestPhaseid;
                textBoxDate.Text = Request.RequestData.ToShortDateString();
                textBoxTime.Text = Request.RequestTime.ToString();


            }

            comboBoxMaster.DataSource = Helper.DBRequest.User.Where(m => m.UserRoleid == 2).ToList();
            comboBoxMaster.DisplayMember = "UserFullName";
            comboBoxMaster.ValueMember = "Userid";


            comboBoxPriority.DataSource = Helper.DBRequest.Priority.ToList();
            comboBoxPriority.DisplayMember = "PriorityName";
            comboBoxPriority.ValueMember = "Priorityid";

            comboBoxPhase.DataSource = Helper.DBRequest.Phase.ToList();
            comboBoxPhase.DisplayMember = "PhaseName";
            comboBoxPhase.ValueMember = "Phaseid";
            comboBoxPhase.SelectedValue = 3;

         


            // Взависимости от роли пользователя включил элементы  
            if (Helper.user.UserRoleid == 3)
            {
                comboBoxMaster.Enabled = true;
                //Включаем элементы доступные оператору в режиме Добавления 
                if (Request == null)
                {
                    comboBoxDevice.Enabled = comboBoxProblem.Enabled = comboBoxClient.Enabled = true;
                    textBoxDescription.Enabled = comboBoxPriority.Enabled = true;
                }
            }
            if (Helper.user.UserRoleid == 2)    //Оператор только в режиме Редактирования
            {
                //Может менять только время обслуживания заявки, ее этап и комментарий
                textBoxTime.Enabled = comboBoxPhase.Enabled = textBoxCom.Enabled = true;
            }

        }
        // Фиксировать в БД
        private void buttonFixed_Click(object sender, EventArgs e)
        {
            // Добавление
            if (Request == null)
            {
                Request = new Request(); //1
                Request.RequestData = DateTime.Parse(this.textBoxDate.Text);
                Request.RequestDeviceid = (int)comboBoxDevice.SelectedValue;
                Request.RequestDefectid = (int)comboBoxProblem.SelectedValue;
                Request.RequestClientid = (int)comboBoxClient.SelectedValue;
                Request.RequestDescription = textBoxDescription.Text;
                Request.RequestStatusid = (int)comboBoxStatus.SelectedValue;
                Request.RequestMasterid = (int)comboBoxMaster.SelectedValue;
                Request.RequestTime = int.Parse(textBoxTime.Text);
                Request.RequestPrioryid = (int)comboBoxPriority.SelectedValue;
                Request.RequestPhaseid = (int)comboBoxPhase.SelectedValue;
                Request.RequestComment = textBoxCom.Text; // 3 
                Helper.DBRequest.Request.Add(Request);
                //4
                try
                {
                    Helper.DBRequest.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены");
                }
                catch(Exception) 
                {
                    MessageBox.Show("Данные не работают");
                    return;
                }
                this.Close();
            }
            else{ //редактирование
                Request = new Request(); //1
                Request.RequestData = DateTime.Parse(this.textBoxDate.Text);
                Request.RequestDeviceid = (int)comboBoxDevice.SelectedValue;
                Request.RequestDefectid = (int)comboBoxProblem.SelectedValue;
                Request.RequestClientid = (int)comboBoxClient.SelectedValue;
                Request.RequestDescription = textBoxDescription.Text;
                Request.RequestStatusid = (int)comboBoxStatus.SelectedValue;
                Request.RequestMasterid = (int)comboBoxMaster.SelectedValue;
                Request.RequestTime = int.Parse(textBoxTime.Text);
                Request.RequestPrioryid = (int)comboBoxPriority.SelectedValue;
                Request.RequestPhaseid = (int)comboBoxPhase.SelectedValue;
                Request.RequestComment = textBoxCom.Text; // 3 
                //4
                try
                {
                    Helper.DBRequest.SaveChanges();
                    MessageBox.Show("Данные успешно отредактированы");
                }
                catch (Exception)
                {
                    MessageBox.Show("Данные не отредактированы");
                    return;
                }
                this.Close();

            }

        }
    }
}
