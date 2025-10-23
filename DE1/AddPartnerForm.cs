using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE1
{
    public partial class AddPartnerForm : Form
    {
        private chimDBEntities _dbContext;

        public AddPartnerForm(chimDBEntities dbContext)
        {
            InitializeComponent();

            _dbContext = dbContext;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var newPartner = new Partners();

                newPartner.Тип_партнера = textBox1.Text;
                newPartner.Наименование_партнера = textBox2.Text;
                newPartner.Руководитель = textBox3.Text;
                newPartner.Электронная_почта_партнера = textBox4.Text;
                newPartner.Телефон_партнера = textBox5.Text;
                newPartner.Юридический_адрес_партнера = textBox8.Text;
                newPartner.ИНН = textBox7.Text;

                if (int.TryParse(textBox6.Text, out int rating))
                {
                    newPartner.Рейтинг = rating;
                }

                _dbContext.Partners.Add(newPartner);
                _dbContext.SaveChanges();

                MessageBox.Show("Новый партнер успешно создан!", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
