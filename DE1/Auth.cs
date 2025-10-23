using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE1
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword == storedPassword;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string passw = textBox2.Text;

            using (var db = new chimDBEntities())
            {
                var user = db.Пользователи.FirstOrDefault(u => u.Логин == login);

                if (user != null && VerifyPassword(passw, user.Пароль))
                {
                    var role = db.Роли.FirstOrDefault(r => r.IdRole == user.IdRole)?.Наименование;

                    Form1 form1 = new Form1(user.Логин, role);
                    form1.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
